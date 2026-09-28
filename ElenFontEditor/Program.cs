using ElenFontEditor.Forms;
using ToolsCore;

namespace ElenFontEditor;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        AppInit.Initialization(out GlobData.Config, out GlobData.Styles, out GlobData.UsingStyle);

        AppInit.Run(GlobData.Config, () => new FMain());
    }
}
