[Setup]
AppName=Filename Randomizer
AppVersion=1.2.0
AppPublisher=xdpirate
DefaultDirName={autopf}\Filename Randomizer
DefaultGroupName=Filename Randomizer
OutputBaseFilename=FilenameRandomizer_Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
; Require admin so we can write to HKLM and Program Files
PrivilegesRequired=admin

[Files]
; Point this at your compiled exe - adjust path if needed
Source: "Filename Randomizer\bin\Release\Filename Randomizer.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Filename Randomizer"; Filename: "{app}\Filename Randomizer.exe"
Name: "{group}\Uninstall Filename Randomizer"; Filename: "{uninstallexe}"

[Registry]
; Add right-click context menu entry for folders
Root: HKLM; Subkey: "SOFTWARE\Classes\Directory\shell\RandomizeFilenames"; ValueType: string; ValueName: ""; ValueData: "Randomize Filenames"; Flags: uninsdeletekey
Root: HKLM; Subkey: "SOFTWARE\Classes\Directory\shell\RandomizeFilenames\command"; ValueType: string; ValueName: ""; ValueData: """{app}\Filename Randomizer.exe"" ""%1"""; Flags: uninsdeletekey

[Code]
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then
  begin
    RegDeleteKeyIncludingSubkeys(HKLM, 'SOFTWARE\Classes\Directory\shell\RandomizeFilenames');
  end;
end;
