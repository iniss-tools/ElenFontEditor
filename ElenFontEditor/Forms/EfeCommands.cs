using ElenFontEditor.Properties;
using ToolsCore.Commands;

namespace ElenFontEditor.Forms;

/// <summary>
/// Prikazy hlavneho okna s predvolenymi klavesovymi skratkami. Identifikator je nazov prvku skratky v config.xml -
/// nesmie sa menit, inak by sa stratili skratky nastavene pouzivatelom.
/// </summary>
internal static class EfeCommands
{
    public static CommandInfo New => new("New", Resources.Cmd_New, Shortcut.CtrlShiftS);
    public static CommandInfo Open => new("Open", Resources.Cmd_Open, Shortcut.CtrlO);
    public static CommandInfo Save => new("Save", Resources.Cmd_Save, Shortcut.CtrlS);
    public static CommandInfo FontManager => new("FontManager", Resources.Cmd_FontManager, Shortcut.F1);
    public static CommandInfo RenameFile => new("RenameFile", Resources.Cmd_RenameFile, Shortcut.F2);
    public static CommandInfo AppSettings => new("AppSettings", Resources.Cmd_AppSettings, Shortcut.CtrlShiftN);

    // bez nastavitelnej skratky
    public static CommandInfo InfoApp => new(nameof(InfoApp), nameof(InfoApp), Shortcut.None);

    /// <summary>
    /// Prikazy, ktorym sa v nastaveniach da zmenit skratka.
    /// </summary>
    public static IReadOnlyList<CommandInfo> All => [New, Open, Save, FontManager, RenameFile, AppSettings];
}
