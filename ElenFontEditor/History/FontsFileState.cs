using ElenFontEditor.Entities;

namespace ElenFontEditor.History;

/// <summary>
/// Stav otvoreneho suboru pisiem na kroky spat - vsetko, co uklada Ctrl+S: nazov a cesta suboru, pisma a ich znaky.
/// Hodnoty su kopie, dalsie upravy suboru stav nezmenia. Nezmenene znaky zdiela s predchadzajucim stavom, takze krok
/// spat si pamata len zmenene znaky.
/// </summary>
internal sealed class FontsFileState
{
    private readonly FontsFile _file;
    private readonly string _name;
    private readonly string _pathName;
    private readonly FontState[] _fonts;

    private FontsFileState(FontsFile file, FontState[] fonts)
    {
        _file = file;
        _name = file.Name;
        _pathName = file.PathName;
        _fonts = fonts;
    }

    /// <summary>
    /// Zapamata stav suboru <paramref name="file" />. Znaky rovnake ako v stave <paramref name="previous" /> na tom istom
    /// mieste sa nekopiruju znova - stavy ich zdielaju.
    /// </summary>
    public static FontsFileState Capture(FontsFile file, FontsFileState? previous = null)
    {
        var fonts = new FontState[file.Fonts.Count];
        for (var i = 0; i < fonts.Length; i++)
            fonts[i] = FontState.Capture(file.Fonts[i], previous?._fonts.ElementAtOrDefault(i));
        return new FontsFileState(file, fonts);
    }

    /// <summary>
    /// Vrati subor do zapamataneho stavu. Pisma a znaky su nove objekty, subor a jeho zoznam pisiem ostavaju tie iste
    /// (vazby okna na zoznam pisiem ostanu platne).
    /// </summary>
    public void Restore()
    {
        _file.Name = _name;
        _file.PathName = _pathName;

        var fonts = _file.Fonts;
        var raise = fonts.RaiseListChangedEvents;
        fonts.RaiseListChangedEvents = false;
        try
        {
            fonts.Clear();
            foreach (var font in _fonts)
                fonts.Add(font.Create());
        }
        finally
        {
            fonts.RaiseListChangedEvents = raise;
        }

        if (raise)
            fonts.ResetBindings();
    }

    /// <summary>Rovnaky stav - zmena, ktora nic nezmenila, nie je krok.</summary>
    public bool SameAs(FontsFileState other) =>
        _name == other._name && _pathName == other._pathName && _fonts.Length == other._fonts.Length &&
        _fonts.Zip(other._fonts).All(pair => pair.First.SameAs(pair.Second));

    private sealed class FontState
    {
        private readonly string _name;
        private readonly int _maxWidth;
        private readonly int _height;
        private readonly bool _isProportional;
        private readonly SymbolState[] _symbols;

        private FontState(TableFont font, SymbolState[] symbols)
        {
            _name = font.Name;
            _maxWidth = font.MaxWidth;
            _height = font.Height;
            _isProportional = font.IsProportional;
            _symbols = symbols;
        }

        public static FontState Capture(TableFont font, FontState? previous)
        {
            var symbols = new SymbolState[font.Symbols.Length];
            for (var i = 0; i < symbols.Length; i++)
            {
                var symbol = font.Symbols[i];
                symbols[i] = previous?._symbols.ElementAtOrDefault(i) is { } same && same.Matches(symbol) ? same : SymbolState.Capture(symbol);
            }

            return new FontState(font, symbols);
        }

        public TableFont Create()
        {
            var font = new TableFont
            {
                Name = _name,
                MaxWidth = _maxWidth,
                Height = _height,
                IsProportional = _isProportional
            };
            for (var i = 0; i < _symbols.Length; i++)
                font.Symbols[i] = _symbols[i].Create(i, font);
            return font;
        }

        public bool SameAs(FontState other) =>
            ReferenceEquals(this, other) ||
            (_name == other._name && _maxWidth == other._maxWidth && _height == other._height && _isProportional == other._isProportional &&
             _symbols.Length == other._symbols.Length && _symbols.Zip(other._symbols).All(pair => pair.First.SameAs(pair.Second)));
    }

    private sealed class SymbolState
    {
        private readonly int _width;
        private readonly int _rows;
        private readonly int _columns;

        /// <summary>Bajty riadkov po maticiach (riadok * pocet matic + matica), ako v subore.</summary>
        private readonly byte[] _data;

        private SymbolState(FontSymbol symbol, byte[] data)
        {
            _width = symbol.Width;
            _rows = symbol.Rows;
            _columns = symbol.Columns;
            _data = data;
        }

        public static SymbolState Capture(FontSymbol symbol)
        {
            var data = new byte[symbol.Rows * symbol.Columns];
            for (var r = 0; r < symbol.Rows; r++)
                for (var c = 0; c < symbol.Columns; c++)
                    data[r * symbol.Columns + c] = symbol[r, c];
            return new SymbolState(symbol, data);
        }

        /// <summary>Znak <paramref name="symbol" /> ma prave tento stav.</summary>
        public bool Matches(FontSymbol symbol)
        {
            if (symbol.Width != _width || symbol.Rows != _rows || symbol.Columns != _columns)
                return false;

            for (var r = 0; r < _rows; r++)
                for (var c = 0; c < _columns; c++)
                    if (symbol[r, c] != _data[r * _columns + c])
                        return false;
            return true;
        }

        public FontSymbol Create(int index, TableFont font)
        {
            var symbol = new FontSymbol(index, _width, _rows, _columns, font);
            for (var r = 0; r < _rows; r++)
                for (var c = 0; c < _columns; c++)
                    symbol[r, c] = _data[r * _columns + c];
            return symbol;
        }

        public bool SameAs(SymbolState other) =>
            ReferenceEquals(this, other) ||
            (_width == other._width && _rows == other._rows && _columns == other._columns && _data.AsSpan().SequenceEqual(other._data));
    }
}
