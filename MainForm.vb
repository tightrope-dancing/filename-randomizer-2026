Imports System.IO

Public Class MainForm

    Public randomizer As New Random
    ' Lowercase a-z + 0-9 (base36). Uppercase is deliberately excluded: NTFS is case-insensitive,
    ' so "aB" and "Ab" are the same filename and mixed case would add no distinct names on Windows.
    Public characters As String() = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
                                     "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                     "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
    Private isPinned As Boolean = False

    Private Sub browseButton_Click(sender As System.Object, e As System.EventArgs) Handles browseButton.Click
        Dim response As DialogResult = FolderBrowserDialog1.ShowDialog()
        Select Case response
            Case Windows.Forms.DialogResult.OK
                directoryNameTextBox.Text = FolderBrowserDialog1.SelectedPath
        End Select
    End Sub

    Private Sub pinButton_Click(sender As System.Object, e As System.EventArgs) Handles pinButton.Click
        If isPinned Then
            isPinned = False
            pinButton.Text = "Pin Folder"
            browseButton.Enabled = True
        Else
            If directoryNameTextBox.Text = Nothing Or directoryNameTextBox.Text = "" Then
                MessageBox.Show("Please select a folder to pin.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                isPinned = True
                pinButton.Text = "Unpin"
                browseButton.Enabled = False
            End If
        End If
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
                        My.Computer.FileSystem.RenameFile(fileName, randomFileName(32) & extension)
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

                ' Write acted-on path immediately, unless pinned
                If Not isPinned Then
                    SaveSettings()
                End If
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

    Private Sub SaveSettings()
        My.Settings.LastPath = directoryNameTextBox.Text
        My.Settings.HiddenChecked = hideFilesCheckBox.Checked
        My.Settings.Pinned = isPinned
        My.Settings.PinnedPath = If(isPinned, directoryNameTextBox.Text, My.Settings.PinnedPath)
        My.Settings.Save()
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSettings()
    End Sub

    Private Sub MainForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim args As String() = Environment.GetCommandLineArgs()

        ' If launched with a folder path argument (e.g. from context menu), pre-fill it
        If args.Length > 1 AndAlso Directory.Exists(args(1)) Then
            directoryNameTextBox.Text = args(1)
            FolderBrowserDialog1.SelectedPath = args(1)
        Else
            ' Normal launch - restore saved settings
            If My.Settings.Pinned AndAlso My.Settings.PinnedPath <> Nothing AndAlso My.Settings.PinnedPath <> "" Then
                directoryNameTextBox.Text = My.Settings.PinnedPath
                FolderBrowserDialog1.SelectedPath = My.Settings.PinnedPath
                isPinned = True
                pinButton.Text = "Unpin"
                browseButton.Enabled = False
            ElseIf My.Settings.LastPath <> Nothing AndAlso My.Settings.LastPath <> "" Then
                directoryNameTextBox.Text = My.Settings.LastPath
                FolderBrowserDialog1.SelectedPath = My.Settings.LastPath
            End If
        End If

        hideFilesCheckBox.Checked = My.Settings.HiddenChecked
    End Sub

End Class
