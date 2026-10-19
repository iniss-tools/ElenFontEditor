using ElenFontEditor.Entities;

namespace ElenFontEditor.History;

/// <summary>
/// Kroky spat a znova nad otvorenym suborom pisiem: po kazdej dokoncenej uprave sa zapamata stav suboru
/// (<see cref="Commit" />), Spat a Znova sa medzi zapamatanymi stavmi len posuvaju. Najviac <see cref="Capacity" />
/// krokov; nova uprava po kroku spat zahodi kroky znova.
/// </summary>
internal sealed class FontsFileHistory
{
    /// <summary>Kolko krokov spat sa pamata.</summary>
    public const int Capacity = 100;

    private readonly FontsFile _file;
    private readonly List<(string Label, FontsFileState State)> _states = [];
    private int _position;
    private int _savedPosition;

    /// <summary>Zacne historiu so sucasnym stavom suboru; <paramref name="saved" /> = sucasny stav je ulozeny.</summary>
    public FontsFileHistory(FontsFile file, bool saved)
    {
        _file = file;
        _states.Add(("", FontsFileState.Capture(file)));
        _savedPosition = saved ? 0 : -1;
    }

    public bool CanUndo => _position > 0;

    public bool CanRedo => _position < _states.Count - 1;

    /// <summary>Popis kroku, ktory vrati Spat; null = nie je.</summary>
    public string? UndoLabel => CanUndo ? _states[_position].Label : null;

    /// <summary>Popis kroku, ktory zopakuje Znova; null = nie je.</summary>
    public string? RedoLabel => CanRedo ? _states[_position + 1].Label : null;

    /// <summary>Popisy krokov spat od posledneho.</summary>
    public IReadOnlyList<string> UndoLabels => Enumerable.Range(1, _position).Select(i => _states[_position + 1 - i].Label).ToList();

    /// <summary>Popisy krokov znova od najblizsieho.</summary>
    public IReadOnlyList<string> RedoLabels => _states.Skip(_position + 1).Select(s => s.Label).ToList();

    /// <summary>Subor je v stave, v akom sa naposledy ulozil.</summary>
    public bool IsSaved => _position == _savedPosition;

    /// <summary>Kolko krokov spat sa pamata teraz.</summary>
    public int Count => _position;

    /// <summary>
    /// Zapamata stav po dokoncenej uprave <paramref name="label" />. Ak sa subor v skutocnosti nezmenil, krok nevznikne.
    /// </summary>
    /// <returns><see langword="true" />, ak vznikol krok.</returns>
    public bool Commit(string label)
    {
        var state = FontsFileState.Capture(_file, _states[_position].State);
        if (state.SameAs(_states[_position].State))
            return false;

        // nova uprava po kroku spat - kroky znova (a s nimi aj ulozeny stav, ak bol medzi nimi) zaniknu
        _states.RemoveRange(_position + 1, _states.Count - _position - 1);
        if (_savedPosition > _position)
            _savedPosition = -1;

        _states.Add((label, state));
        _position++;
        if (_states.Count > Capacity + 1)
        {
            _states.RemoveAt(0);
            _position--;
            _savedPosition = _savedPosition > 0 ? _savedPosition - 1 : -1;
        }

        return true;
    }

    /// <summary>Vrati <paramref name="steps" /> krokov spat.</summary>
    public void Undo(int steps = 1)
    {
        if (steps < 1 || steps > _position)
            throw new ArgumentOutOfRangeException(nameof(steps));
        _position -= steps;
        _states[_position].State.Restore();
    }

    /// <summary>Zopakuje <paramref name="steps" /> krokov znova.</summary>
    public void Redo(int steps = 1)
    {
        if (steps < 1 || _position + steps > _states.Count - 1)
            throw new ArgumentOutOfRangeException(nameof(steps));
        _position += steps;
        _states[_position].State.Restore();
    }

    /// <summary>Sucasny stav sa ulozil.</summary>
    public void MarkSaved() => _savedPosition = _position;
}
