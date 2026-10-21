; Instalator ElenFontEditora. Spolocny obsah instalatorov je v ToolsCore\Installer\AppSetup.iss.
; Pred kompilaciou treba zostavit Release: dotnet build ElenFontEditor\ElenFontEditor.csproj -c Release

#define MyAppName "ElenFontEditor"
#define MyAppExeName "ElenFontEditor.exe"
#define MyAppDir SourcePath + "ElenFontEditor\bin\Release\net10.0-windows"
#define MyOutputBaseFilename "elenfonteditor-setup"

#include "..\ToolsCore\Installer\AppSetup.iss"
