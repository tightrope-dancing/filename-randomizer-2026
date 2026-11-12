Imports System.IO

Public Class MainForm

    Public randomizer As New Random
    ' Lowercase a-z + 0-9 (base36). Uppercase is deliberately excluded: NTFS is case-insensitive,
    ' so "aB" and "Ab" are the same filename and mixed case would add no distinct names on Windows.
    Public characters As String() = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
                                     "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                     "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}

    ' Bounds for the name-length slider (applied to lengthTrackBar on load).
    Private Const MinNameLength As Integer = 16
    Private Const MaxNameLength As Integer = 40
    Private Const DefaultNameLength As Integer = 32

    ' Folder history: one optional pinned folder that never ages out, plus the most recently
    ' renamed folders (newest first). The pinned folder is never duplicated in the recent list.
    Private Const MaxRecentFolders As Integer = 3
    Private Const PinnedPrefix As String = "[Pinned]  "
    ' Path separator for the RecentFolders setting; "|" cannot appear in a Windows path.
    Private Const RecentSeparator As String = "|"

    Private pinnedFolder As String = ""
    Private recentFolders As New List(Of String)
    ' Real paths behind each folderListBox row (rows carry display text such as the pinned prefix).
    Private folderListPaths As New List(Of String)
    Private suppressListSelection As Boolean = False

    ' Opening size of the Browse For Folder window, roughly a tall column; the user's own resize
    ' is remembered in My.Settings.BrowseDialogSize and used from then on.
    Private Shared ReadOnly DefaultBrowseDialogSize As New Drawing.Size(325, 810)

    Private Sub browseButton_Click(sender As System.Object, e As System.EventArgs) Handles browseButton.Click
        Dim dialogSize As Drawing.Size = My.Settings.BrowseDialogSize
        Dim response As DialogResult = FolderDialogSizer.ShowDialog(FolderBrowserDialog1, Me, dialogSize, DefaultBrowseDialogSize)
        My.Settings.BrowseDialogSize = dialogSize
        Select Case response
            Case Windows.Forms.DialogResult.OK
                directoryNameTextBox.Text = FolderBrowserDialog1.SelectedPath
        End Select
    End Sub

    Private Sub pinButton_Click(sender As System.Object, e As System.EventArgs) Handles pinButton.Click
        Dim current As String = directoryNameTextBox.Text

        If String.IsNullOrEmpty(current) Then
            MessageBox.Show("Please select a folder to pin.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If SamePath(current, pinnedFolder) Then
            ' Unpin: the folder drops back into the recent list as the newest entry.
            pinnedFolder = ""
            AddRecentFolder(current)
        Else
            ' Pin: a previously pinned folder is demoted to the recent list rather than lost.
            Dim previous As String = pinnedFolder
            pinnedFolder = NormalizePath(current)
            recentFolders.RemoveAll(Function(p) SamePath(p, pinnedFolder))
            If previous <> "" Then AddRecentFolder(previous)
        End If

        RefreshFolderList()
        SaveSettings()
    End Sub

    Private Sub folderListBox_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles folderListBox.SelectedIndexChanged
        If suppressListSelection Then Return
        Dim index As Integer = folderListBox.SelectedIndex
        If index < 0 OrElse index >= folderListPaths.Count Then Return
        directoryNameTextBox.Text = folderListPaths(index)
        FolderBrowserDialog1.SelectedPath = folderListPaths(index)
    End Sub

    Private Sub directoryNameTextBox_TextChanged(sender As Object, e As System.EventArgs) Handles directoryNameTextBox.TextChanged
        UpdatePinButton()
        SyncListSelection()
    End Sub

    Private Sub renameButton_Click(sender As System.Object, e As System.EventArgs) Handles renameButton.Click
        Try
            If directoryNameTextBox.Text = Nothing Or directoryNameTextBox.Text = "" Then
                MessageBox.Show("Please select a folder.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim fileNames As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetFiles(directoryNameTextBox.Text)
                Dim extension As String
                Dim protectedFileFound As Boolean = False

                For Each fileName As String In fileNames
                    If Path.GetFileName(fileName) = "Thumbs.db" Or Path.GetFileName(fileName) = "desktop.ini" Then
                        protectedFileFound = True
                    Else
                        extension = Path.GetExtension(fileName)
                        ' Regenerate on the (astronomically unlikely) chance the name is already taken,
                        ' so a collision can't abort the batch halfway through.
                        Dim newName As String
                        Do
                            newName = randomFileName(lengthTrackBar.Value) & extension
                        Loop While File.Exists(Path.Combine(Path.GetDirectoryName(fileName), newName))
                        My.Computer.FileSystem.RenameFile(fileName, newName)
                    End If
                Next

                If hideFilesCheckBox.Checked = True Then
                    Dim fileNames2 As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetFiles(directoryNameTextBox.Text)
                    For Each fileName As String In fileNames2
                        Dim fileInfo As New FileInfo(fileName)
                        fileInfo.Attributes = FileAttributes.Hidden
                    Next
                End If

                Dim printString As String = "Done!"
                If protectedFileFound Then
                    printString = "Done! One or more protected files were found (Thumbs.db/desktop.ini) and were left untouched."
                End If
                MessageBox.Show(printString, My.Application.Info.AssemblyName)

                ' Record the acted-on folder in the history and persist immediately
                AddRecentFolder(directoryNameTextBox.Text)
                RefreshFolderList()
                SaveSettings()
            End If
        Catch ex As Exception
            MessageBox.Show("Something horrible has happened!" & vbNewLine & vbNewLine & ex.Message, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function randomFileName(fileNameLength)
        Dim returnValue As String = Nothing

        For counter As Integer = 1 To fileNameLength Step 1
            returnValue &= characters(randomizer.Next(0, characters.Length))
        Next

        Return returnValue
    End Function

    ' ---- Folder history helpers ----

    Private Shared Function NormalizePath(folder As String) As String
        Dim full As String = Path.GetFullPath(folder)
        ' Strip a trailing separator, but keep drive roots ("C:\") intact.
        If full.Length > 3 Then full = full.TrimEnd(Path.DirectorySeparatorChar)
        Return full
    End Function

    Private Shared Function SamePath(a As String, b As String) As Boolean
        If String.IsNullOrEmpty(a) OrElse String.IsNullOrEmpty(b) Then Return False
        Try
            Return String.Equals(NormalizePath(a), NormalizePath(b), StringComparison.OrdinalIgnoreCase)
        Catch
            Return String.Equals(a, b, StringComparison.OrdinalIgnoreCase)
        End Try
    End Function

    ' Moves a folder to the front of the recent list, dropping the oldest beyond the limit.
    ' The pinned folder is never added; it already has a permanent slot.
    Private Sub AddRecentFolder(folder As String)
        If String.IsNullOrEmpty(folder) OrElse SamePath(folder, pinnedFolder) Then Return
        Dim normalized As String = NormalizePath(folder)
        recentFolders.RemoveAll(Function(p) SamePath(p, normalized))
        recentFolders.Insert(0, normalized)
        If recentFolders.Count > MaxRecentFolders Then
            recentFolders.RemoveRange(MaxRecentFolders, recentFolders.Count - MaxRecentFolders)
        End If
    End Sub

    Private Sub RefreshFolderList()
        suppressListSelection = True
        folderListBox.BeginUpdate()
        folderListBox.Items.Clear()
        folderListPaths.Clear()

        If pinnedFolder <> "" Then
            folderListBox.Items.Add(PinnedPrefix & pinnedFolder)
            folderListPaths.Add(pinnedFolder)
        End If
        For Each folder As String In recentFolders
            folderListBox.Items.Add(folder)
            folderListPaths.Add(folder)
        Next

        folderListBox.EndUpdate()
        suppressListSelection = False
        SyncListSelection()
        UpdatePinButton()
    End Sub

    ' Highlights the list row matching the folder in the text box, if there is one.
    Private Sub SyncListSelection()
        suppressListSelection = True
        Dim match As Integer = folderListPaths.FindIndex(Function(p) SamePath(p, directoryNameTextBox.Text))
        folderListBox.SelectedIndex = match
        suppressListSelection = False
    End Sub

    Private Sub UpdatePinButton()
        pinButton.Text = If(SamePath(directoryNameTextBox.Text, pinnedFolder), "Un&pin", "&Pin")
    End Sub

    ' ---- Settings ----

    Private Sub LoadFolderHistory()
        ' One-time migration from v1.2.0 and earlier: PinnedPath could hold a stale value while
        ' Pinned was False, and the single LastPath becomes the first recent folder.
        pinnedFolder = If(My.Settings.Pinned AndAlso Not String.IsNullOrEmpty(My.Settings.PinnedPath), My.Settings.PinnedPath, "")

        recentFolders.Clear()
        Dim stored As String = My.Settings.RecentFolders
        If String.IsNullOrEmpty(stored) AndAlso Not String.IsNullOrEmpty(My.Settings.LastPath) Then
            stored = My.Settings.LastPath
        End If
        If Not String.IsNullOrEmpty(stored) Then
            For Each folder As String In stored.Split(New String() {RecentSeparator}, StringSplitOptions.RemoveEmptyEntries)
                ' Recent folders that no longer exist are dropped; the pinned one is kept even if
                ' it is temporarily unavailable (e.g. an unplugged drive).
                If Directory.Exists(folder) Then AddRecentFolderAtEnd(folder)
            Next
        End If
    End Sub

    Private Sub AddRecentFolderAtEnd(folder As String)
        If SamePath(folder, pinnedFolder) Then Return
        If recentFolders.Exists(Function(p) SamePath(p, folder)) Then Return
        If recentFolders.Count >= MaxRecentFolders Then Return
        recentFolders.Add(NormalizePath(folder))
    End Sub

    Private Sub SaveSettings()
        My.Settings.RecentFolders = String.Join(RecentSeparator, recentFolders)
        My.Settings.LastPath = If(recentFolders.Count > 0, recentFolders(0), "")
        My.Settings.Pinned = (pinnedFolder <> "")
        My.Settings.PinnedPath = pinnedFolder
        My.Settings.HiddenChecked = hideFilesCheckBox.Checked
        My.Settings.NameLength = lengthTrackBar.Value
        My.Settings.Save()
    End Sub

    Private Sub lengthTrackBar_ValueChanged(sender As Object, e As System.EventArgs) Handles lengthTrackBar.ValueChanged
        lengthLabel.Text = "Name &length: " & lengthTrackBar.Value & " characters"
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSettings()
    End Sub

    Private Sub MainForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' User settings are stored per assembly version; carry them forward after an update.
        If My.Settings.UpgradeRequired Then
            My.Settings.Upgrade()
            My.Settings.UpgradeRequired = False
            My.Settings.Save()
        End If

        LoadFolderHistory()

        Dim args As String() = Environment.GetCommandLineArgs()
        Dim startFolder As String = ""

        If args.Length > 1 AndAlso Directory.Exists(args(1)) Then
            ' Launched with a folder path argument (e.g. from context menu)
            startFolder = args(1)
        ElseIf pinnedFolder <> "" Then
            startFolder = pinnedFolder
        ElseIf recentFolders.Count > 0 Then
            startFolder = recentFolders(0)
        End If

        If startFolder <> "" Then
            directoryNameTextBox.Text = startFolder
            FolderBrowserDialog1.SelectedPath = startFolder
        End If
        RefreshFolderList()

        hideFilesCheckBox.Checked = My.Settings.HiddenChecked

        ' Clamp in case the saved value predates the current slider range
        lengthTrackBar.Minimum = MinNameLength
        lengthTrackBar.Maximum = MaxNameLength
        Dim savedLength As Integer = My.Settings.NameLength
        If savedLength < MinNameLength OrElse savedLength > MaxNameLength Then savedLength = DefaultNameLength
        lengthTrackBar.Value = savedLength
        lengthTrackBar_ValueChanged(lengthTrackBar, EventArgs.Empty)
    End Sub

End Class
