# Filename Randomizer

A lightweight Windows utility that renames all files in a selected folder to random 32-character alphanumeric strings (`a`–`z`, `0`–`9`), preserving file extensions.

## Features

- Randomizes filenames in a selected folder to base36 strings (e.g. `k3f7x2q1...4e9z.jpg`)
- **Pin Folder** — lock a frequently-used folder so it survives across sessions
- **Last-used memory** — automatically restores the last folder you acted on
- **Windows context menu integration** — right-click any folder and select "Randomize Filenames" to launch with that folder pre-filled
- Optional: mark renamed files as hidden after renaming
- Clean uninstall via Add/Remove Programs

## Requirements

- Windows 10 or 11
- .NET Framework 4.8 (included with Windows 10/11)

## Installation

Download `FilenameRandomizer_Setup.exe` from [Releases](../../releases) and run it. Accept the UAC prompt. That's it.

> **Note:** Windows SmartScreen may show a warning on first run because the installer is not code-signed. Click "More info" → "Run anyway." The source code is here if you want to verify it yourself.

## Usage

**From the context menu (recommended):**
Right-click any folder in Windows Explorer → "Randomize Filenames" → the app opens with that folder pre-filled → click Rename.

**Standalone:**
Launch from Start Menu or `Program Files\Filename Randomizer\`. Browse to a folder, click Rename.

**Pin Folder:**
Navigate to a folder you use repeatedly, click "Pin Folder." The app will always reopen to that folder regardless of what else you rename. Click "Unpin" to return to normal last-used tracking.

## Warning

Renaming is **immediate and irreversible.** There is no undo. The original filenames are gone. Do not run this on folders containing files whose names matter unless you are sure.

## Building from Source

1. Open `Filename Randomizer.sln` in Visual Studio (Community edition is free)
2. Target framework: .NET Framework 4.8
3. Build → Rebuild Solution
4. Output: `bin\Release\Filename Randomizer.exe`

To build the installer, open `FilenameRandomizer.iss` in [Inno Setup](https://jrsoftware.org/isinfo.php) and update the `Source:` path in the `[Files]` section to match your local build output, then Build → Compile.

## Version History

**v1.2.0** — Expanded the character pool from hex (`0`–`9`, `a`–`f`; 16 symbols) to lowercase alphanumeric (`0`–`9`, `a`–`z`; 36 symbols). Name space grows from 16³² ≈ 3.4×10³⁸ to 36³² ≈ 6.3×10⁴⁹ at the same 32-char length. Uppercase excluded because NTFS filenames are case-insensitive.

**v1.1.1** — Fixed off-by-one error in name generation (was producing 33-char names); fixed excluded character (`9` was never selected due to incorrect upper bound); command-line argument support for context menu integration

**v1.1.0** — Replaced broken IniFile dependency with native My.Settings; added Pin Folder feature; last-used path now saves on successful rename rather than on close; fixed dead code path in protected-file message; retargeted to .NET Framework 4.8

**v1.0.1** — Original release by xdpirate (2012)

## License

MIT
