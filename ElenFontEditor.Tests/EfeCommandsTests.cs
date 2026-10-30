using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ElenFontEditor.Forms;
using ElenFontEditor.XML;

namespace ElenFontEditor.Tests;

/// <summary>
/// Prikazy hlavneho okna a ich skratky v config.xml.
/// </summary>
[TestClass]
public class EfeCommandsTests
{
    [TestMethod]
    public void All_ObsahujeSpatAZnovaBezDuplicit()
    {
        var ids = EfeCommands.All.Select(c => c.Id).ToList();
        CollectionAssert.AreEquivalent(new[] { "New", "Open", "Save", "Undo", "Redo", "FontManager", "RenameFile", "AppSettings", "InfoApp", "CheckUpdates", "ShowLogs" }, ids);

        var shortcuts = EfeCommands.All.Where(c => c.DefaultKeys != Keys.None).Select(c => c.DefaultKeys).ToList();
        Assert.HasCount(shortcuts.Count, shortcuts.Distinct().ToList());
    }

    [TestMethod]
    public void Config_StarySuborNastaveni_SpatAZnovaMajuPredvoleneSkratky()
    {
        const string xml = """
            <CONFIG>
              <Shortcuts>
                <Save sc="CtrlShiftS" />
              </Shortcuts>
            </CONFIG>
            """;

        using var reader = new StringReader(xml);
        var config = (ElenFontEditorConfig)new XmlSerializer(typeof(ElenFontEditorConfig)).Deserialize(XmlReader.Create(reader))!;

        Assert.AreEqual((Keys)Shortcut.CtrlShiftS, config.Shortcuts.Get(EfeCommands.Save));
        Assert.AreEqual((Keys)Shortcut.CtrlZ, config.Shortcuts.Get(EfeCommands.Undo));
        Assert.AreEqual((Keys)Shortcut.CtrlY, config.Shortcuts.Get(EfeCommands.Redo));
    }
}
