using ElenFontEditor.Forms;
using ElenFontEditor.XML;
using ToolsCore;

namespace ElenFontEditor;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        GlobData.Session = AppInit.Initialization<ElenFontEditorConfig, ElenFontEditorStyle>();

        AppInit.Run(GlobData.Config, () => new FMain());
    }
}
