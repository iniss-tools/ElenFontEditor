using System.Globalization;
using System.Reflection;
using ElenFontEditor.Forms;
using ElenFontEditor.XML;
using Microsoft.Win32;
using ToolsCore;
using ToolsCore.Tools;

namespace ElenFontEditor.DocScreenshots;

/// <summary>
/// Generátor snímok okien ElenFontEditora do dokumentácie.
/// </summary>
/// <remarks>
/// Použitie: <c>ElenFontEditor.DocScreenshots [--out=priečinok] [--work=priečinok] [--only=text] [--theme=light|dark|both]</c>.
/// <list type="bullet">
/// <item><c>--out</c> – kam uložiť PNG; predvolene <c>iniss-tools-docs\static\img\elenfonteditor</c> vedľa repozitára.</item>
/// <item><c>--work</c> – kde zostaviť ukážkové súbory písiem; predvolene <c>C:\ELEN</c> (cesta je vidno v titulku).
/// Existujúci priečinok bez značky <c>.docshots</c> sa nezmaže.</item>
/// <item><c>--timeout</c> – po koľkých minútach sa harness ukončí, ak ho zablokuje modálne okno (predvolene 5).</item>
/// <item><c>--only</c> – len snímky, ktorých cesta obsahuje daný text (napr. <c>hlavne-okno</c>).</item>
/// </list>
/// Hodnoty sa zadávajú len v tvare <c>--názov=hodnota</c>: FMain pri štarte otvára posledný argument, ktorý
/// nezačína pomlčkou, ako súbor písiem. Program beží pod vlastným menom, takže konfiguráciu
/// (<c>%LocalAppData%\ElenFontEditor.DocScreenshots</c>) aj register má oddelené od ElenFontEditora – pri každom
/// spustení začína s predvolenými nastaveniami.
/// </remarks>
internal static class Program
{
    private const BindingFlags Any = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

    [STAThread]
    private static int Main(string[] args)
    {
        Options options;
        try
        {
            options = Options.Parse(args);
        }
        catch (ArgumentException e)
        {
            Console.Error.WriteLine(e.Message);
            return 1;
        }

        var log = new List<string>();

        // poistka: modálne okno (chyba, otázka) by harness zablokovalo navždy
        using var watchdog = new System.Threading.Timer(_ =>
        {
            Console.Error.WriteLine("Časový limit vypršal – pravdepodobne visí modálne okno. Posledné kroky:");
            foreach (var line in log.TakeLast(5)) Console.Error.WriteLine("  " + line);
            Console.Error.WriteLine("Otvorené okná: " + string.Join(" | ", WindowCapture.ProcessWindowTitles()));
            Environment.Exit(2);
        }, null, TimeSpan.FromMinutes(options.TimeoutMinutes), Timeout.InfiniteTimeSpan);

        try
        {
            InitApp();
            var files = DemoFonts.Build(options.WorkDir, log);
            // ponuka Nedávne - hlavný súbor ako naposledy otvorený
            for (var i = files.Length - 1; i >= 0; i--)
                AppRegistry.SetUsageOfProject(files[i]);
            using (var screen = Graphics.FromHwnd(IntPtr.Zero))
                log.Add($"DPI: {screen.DpiX} (snímky majú rozmery podľa škálovania obrazovky, pre docs 96 = 100 %)");

            var shots = 0;
            foreach (var theme in options.Themes)
            {
                SetTheme(theme);
                shots += new Shots(options, theme, log).Run(files[0]);
            }

            log.Add($"hotovo: {shots} snímok do {options.OutDir}");
            Flush(log);
            return 0;
        }
        catch (Exception e)
        {
            log.Add("CHYBA: " + (e is TargetInvocationException { InnerException: { } inner } ? inner : e));
            Flush(log);
            return 1;
        }
    }

    private static void Flush(List<string> log)
    {
        foreach (var line in log) Console.WriteLine(line);
    }

    /// <summary>
    /// Rovnaká inicializácia ako ElenFontEditor.Program.Main, s čistou konfiguráciou, registrom a slovenčinou.
    /// </summary>
    private static void InitApp()
    {
        if (Directory.Exists(AppPaths.DataDir))
            Directory.Delete(AppPaths.DataDir, true);

        // zoznam nedávnych súborov a posledný súbor – kľúč patrí harnessu (názov zostavy), nie ElenFontEditoru
        var name = Assembly.GetEntryAssembly()!.GetName().Name;
        Registry.CurrentUser.DeleteSubKeyTree($@"SOFTWARE\{name}", throwOnMissingSubKey: false);

        GlobData.Session = AppInit.Initialization<ElenFontEditorConfig, ElenFontEditorStyle>();

        // harness nebezi v Application.Run: modalne okno (ShowDialog) by pri skonceni svojej slucky odinstalovalo
        // synchronizacny kontext WinForms a BeginInvoke (krok spat po uprave) by sa uz nespracoval
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
        // pristup k prvkom z cudzieho vlakna ma skoncit vynimkou, nie zamrznutim
        Control.CheckForIllegalCrossThreadCalls = true;

        var culture = CultureInfo.CreateSpecificCulture("sk");
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    private static void SetTheme(string theme)
    {
        var style = theme == "dark" ? ElenFontEditorStyle.DefaultDarkStyle : ElenFontEditorStyle.DefaultLightStyle;
        GlobData.UsingStyle = style;
        AppInit.MsgBoxStyleInit(style, GlobData.Config);
    }

    /// <summary>
    /// Otvorí hlavné okno so súborom písiem rovnako ako Súbor → Nedávne.
    /// </summary>
    public static FMain OpenMain(string path)
    {
        var main = new FMain
        {
            StartPosition = FormStartPosition.Manual,
            Location = new Point(40, 40)
        };
        main.Show();
        Pump.Events();

        // okno sa otvára maximalizované - na snímke by malo šírku celej obrazovky
        main.WindowState = FormWindowState.Normal;
        main.Location = new Point(40, 40);
        main.Size = new Size(1100, 720);
        Pump.Events();

        typeof(FMain).GetMethod("OpenFile", Any, [typeof(string), typeof(bool)])!.Invoke(main, [path, false]);
        Pump.Events();
        return main;
    }

    internal sealed record Options(string OutDir, string WorkDir, string? Only, string[] Themes, int TimeoutMinutes)
    {
        public static Options Parse(string[] args)
        {
            var values = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                var eq = arg.IndexOf('=');
                if (!arg.StartsWith("--", StringComparison.Ordinal) || eq < 0)
                    throw new ArgumentException($"Neznámy argument '{arg}' – hodnoty sa zadávajú ako --názov=hodnota (napr. --only=hlavne-okno).");
                values[arg[..eq]] = arg[(eq + 1)..];
            }

            string? Arg(string name) => values.GetValueOrDefault(name);

            var theme = Arg("--theme") ?? "both";
            return new Options(
                Arg("--out") ?? DefaultOutDir(),
                Arg("--work") ?? @"C:\ELEN",
                Arg("--only"),
                theme == "both" ? ["light", "dark"] : [theme],
                int.Parse(Arg("--timeout") ?? "5", CultureInfo.InvariantCulture));
        }

        private static string DefaultOutDir()
        {
            // hľadá iniss-tools-docs v niektorom nadradenom priečinku (D:\INISSTools\iniss-tools-docs)
            for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
            {
                var docs = Path.Combine(dir.FullName, "iniss-tools-docs");
                if (Directory.Exists(docs))
                    return Path.Combine(docs, "static", "img", "elenfonteditor");
            }

            throw new DirectoryNotFoundException("Nenašiel sa priečinok iniss-tools-docs – zadaj --out=….");
        }
    }
}
