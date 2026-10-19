using ElenFontEditor.Entities;
using ElenFontEditor.Forms;
using ElenFontEditor.Properties;
using ElenFontEditor.Tools;
using ToolsCore.Iniss.Elen;

namespace ElenFontEditor.Tests;

/// <summary>
/// Kontrola noveho nazvu suboru pri premenovani.
/// </summary>
[TestClass]
public class RenameFileTests
{
    private DirectoryInfo _dir = null!;

    [TestInitialize]
    public void Init()
    {
        _dir = Directory.CreateTempSubdirectory("efe-rename-");
        File.WriteAllBytes(Path.Combine(_dir.FullName, "TEST.bin"), [0x61]);
        File.WriteAllBytes(Path.Combine(_dir.FullName, "INY.bin"), [0x61]);
    }

    [TestCleanup]
    public void Cleanup() => _dir.Delete(true);

    [TestMethod]
    public void NameError_SuborSNovymNazvomUzJe_Odmietne()
    {
        var file = Open("TEST");

        Assert.AreEqual(Resources.FMain_FileExists, FRenameFile.NameError(file, "INY", DiskPath("TEST")));
    }

    [TestMethod]
    public void NameError_NovySuborNaMiesteExistujuceho_Odmietne()
    {
        var file = Open("NOVY");

        Assert.AreEqual(Resources.FMain_FileExists, FRenameFile.NameError(file, "INY", diskPath: null));
    }

    [TestMethod]
    public void NameError_VolnyNazov_Povoli()
    {
        var file = Open("TEST");

        Assert.IsNull(FRenameFile.NameError(file, "NOVY", DiskPath("TEST")));
        Assert.AreEqual(Path.Combine(_dir.FullName, "NOVY.bin"), FRenameFile.NewPath(file, "NOVY"));
    }

    [TestMethod]
    public void NameError_PremenovanieSpatNaSuborNaDisku_Povoli()
    {
        // premenovany a este neulozeny subor - TEST.bin na disku je jeho povodny subor
        var file = Open("TEST");
        file.Name = "NOVY";
        file.PathName = Path.Combine(_dir.FullName, "NOVY.bin");

        Assert.IsNull(FRenameFile.NameError(file, "TEST", DiskPath("TEST")));
    }

    [TestMethod]
    [DataRow("", "RenameFile_NameEmpty")]
    [DataRow("TEST", "RenameFile_NameSame")]
    [DataRow("A:B", "RenameFile_InvalidChars")]
    [DataRow("A?", "RenameFile_InvalidChars")]
    [DataRow("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456", "FMain_FileName_Length")]
    public void NameError_NeplatnyNazov_Odmietne(string name, string resource)
    {
        var file = Open("TEST");

        Assert.AreEqual(Resources.ResourceManager.GetString(resource, Resources.Culture), FRenameFile.NameError(file, name, DiskPath("TEST")));
    }

    [TestMethod]
    public void NameError_ZmenaLenVelkostiPismen_Povoli()
    {
        var file = Open("TEST");

        Assert.IsNull(FRenameFile.NameError(file, "test", DiskPath("TEST")));
    }

    [TestMethod]
    public void Ulozenie_ZmenaLenVelkostiPismen_PremenujeSuborNaDisku()
    {
        var file = Open("TEST");
        file.Fonts.Add(TableFont.CreateEmpty("PISMO", 6, 10, false));
        file.Name = "test";
        file.PathName = FRenameFile.NewPath(file, "test")!;

        ElenBinParser.SaveFile(file);

        CollectionAssert.AreEquivalent(new[] { "INY.bin", "test.bin" }, Directory.GetFiles(_dir.FullName).Select(Path.GetFileName).ToArray());
        var saved = ElenFontFile.Load(file.PathName, strict: true);
        Assert.AreEqual("test", saved.Name);
        Assert.AreEqual("PISMO", saved.Fonts.Single().Name);
    }

    [TestMethod]
    public void Ulozenie_CestaSInymNazvomAkoHlavicka_SuborNepremenuje()
    {
        // subor otvoreny cestou s inou velkostou pismen - nazov na disku zodpoveda hlavicke
        var file = Open("TEST");
        file.PathName = DiskPath("test");

        ElenBinParser.SaveFile(file);

        CollectionAssert.AreEquivalent(new[] { "INY.bin", "TEST.bin" }, Directory.GetFiles(_dir.FullName).Select(Path.GetFileName).ToArray());
    }

    private FontsFile Open(string name) => new() { Name = name, PathName = DiskPath(name) };

    private string DiskPath(string name) => Path.Combine(_dir.FullName, name + ".bin");
}
