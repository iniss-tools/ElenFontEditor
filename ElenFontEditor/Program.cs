using System;
using System.Windows.Forms;
using ElenFontEditor.Forms;
using ToolsCore;
using ToolsCore.Forms;
using ToolsCore.Tools;
using ToolsCore.XML;

namespace ElenFontEditor;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        GlobSettings.LinkUpdater = "http://iniss.6f.sk/gvdeditor-updater/update.txt";
        AppInit.Initialization(out GlobData.Config, out GlobData.Styles, out GlobData.UsingStyle);

        if (GlobData.Config.DebugModeGUI != DebugMode.AppCrash)
        {
            try
            {
                Application.Run(new FMain());
            }
            catch (Exception exception)
            {
                Log.Exception(exception);
                FError.ShowError(GlobData.Config.DebugModeGUI == DebugMode.OnlyMessage ? exception.Message : exception.ToString());
            }
        }
        else
        {
            Application.Run(new FMain());
        }

        Log.Info("Program sa ukončuje\r\n");
    }
}