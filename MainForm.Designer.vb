<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.directoryNameTextBox = New System.Windows.Forms.TextBox()
        Me.browseButton = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.renameButton = New System.Windows.Forms.Button()
        Me.hideFilesCheckBox = New System.Windows.Forms.CheckBox()
        Me.pinButton = New System.Windows.Forms.Button()
        Me.folderListLabel = New System.Windows.Forms.Label()
        Me.folderListBox = New System.Windows.Forms.ListBox()
        Me.lengthLabel = New System.Windows.Forms.Label()
        Me.lengthTrackBar = New System.Windows.Forms.TrackBar()
        CType(Me.lengthTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'directoryNameTextBox
        '
        Me.directoryNameTextBox.Location = New System.Drawing.Point(12, 12)
        Me.directoryNameTextBox.Name = "directoryNameTextBox"
        Me.directoryNameTextBox.ReadOnly = True
        Me.directoryNameTextBox.Size = New System.Drawing.Size(270, 20)
        Me.directoryNameTextBox.TabIndex = 0
        '
        'browseButton
        '
        Me.browseButton.Location = New System.Drawing.Point(290, 11)
        Me.browseButton.Name = "browseButton"
        Me.browseButton.Size = New System.Drawing.Size(75, 23)
        Me.browseButton.TabIndex = 1
        Me.browseButton.Text = "&Browse..."
        Me.browseButton.UseVisualStyleBackColor = True
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'folderListLabel
        '
        Me.folderListLabel.AutoSize = True
        Me.folderListLabel.Location = New System.Drawing.Point(10, 42)
        Me.folderListLabel.Name = "folderListLabel"
        Me.folderListLabel.Size = New System.Drawing.Size(80, 13)
        Me.folderListLabel.TabIndex = 2
        Me.folderListLabel.Text = "Recent &folders:"
        '
        'folderListBox
        '
        Me.folderListBox.FormattingEnabled = True
        Me.folderListBox.HorizontalScrollbar = True
        Me.folderListBox.Location = New System.Drawing.Point(12, 58)
        Me.folderListBox.Name = "folderListBox"
        Me.folderListBox.Size = New System.Drawing.Size(270, 56)
        Me.folderListBox.TabIndex = 3
        '
        'pinButton
        '
        Me.pinButton.Location = New System.Drawing.Point(290, 57)
        Me.pinButton.Name = "pinButton"
        Me.pinButton.Size = New System.Drawing.Size(75, 23)
        Me.pinButton.TabIndex = 4
        Me.pinButton.Text = "&Pin"
        Me.pinButton.UseVisualStyleBackColor = True
        '
        'hideFilesCheckBox
        '
        Me.hideFilesCheckBox.AutoSize = True
        Me.hideFilesCheckBox.Location = New System.Drawing.Point(13, 124)
        Me.hideFilesCheckBox.Name = "hideFilesCheckBox"
        Me.hideFilesCheckBox.Size = New System.Drawing.Size(120, 17)
        Me.hideFilesCheckBox.TabIndex = 5
        Me.hideFilesCheckBox.Text = "Mark files as &hidden"
        Me.hideFilesCheckBox.UseVisualStyleBackColor = True
        '
        'lengthLabel
        '
        Me.lengthLabel.AutoSize = True
        Me.lengthLabel.Location = New System.Drawing.Point(10, 150)
        Me.lengthLabel.Name = "lengthLabel"
        Me.lengthLabel.Size = New System.Drawing.Size(130, 13)
        Me.lengthLabel.TabIndex = 6
        Me.lengthLabel.Text = "Name &length: 32 characters"
        '
        'lengthTrackBar
        '
        Me.lengthTrackBar.LargeChange = 4
        Me.lengthTrackBar.Location = New System.Drawing.Point(12, 166)
        Me.lengthTrackBar.Maximum = 40
        Me.lengthTrackBar.Minimum = 16
        Me.lengthTrackBar.Name = "lengthTrackBar"
        Me.lengthTrackBar.Size = New System.Drawing.Size(270, 45)
        Me.lengthTrackBar.TabIndex = 7
        Me.lengthTrackBar.TickFrequency = 4
        Me.lengthTrackBar.Value = 32
        '
        'renameButton
        '
        Me.renameButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renameButton.Location = New System.Drawing.Point(290, 176)
        Me.renameButton.Name = "renameButton"
        Me.renameButton.Size = New System.Drawing.Size(75, 23)
        Me.renameButton.TabIndex = 8
        Me.renameButton.Text = "&Rename!"
        Me.renameButton.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(377, 212)
        Me.Controls.Add(Me.lengthTrackBar)
        Me.Controls.Add(Me.lengthLabel)
        Me.Controls.Add(Me.folderListBox)
        Me.Controls.Add(Me.folderListLabel)
        Me.Controls.Add(Me.pinButton)
        Me.Controls.Add(Me.hideFilesCheckBox)
        Me.Controls.Add(Me.renameButton)
        Me.Controls.Add(Me.browseButton)
        Me.Controls.Add(Me.directoryNameTextBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "Filename Randomizer"
        CType(Me.lengthTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents directoryNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents browseButton As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents renameButton As System.Windows.Forms.Button
    Friend WithEvents hideFilesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents pinButton As System.Windows.Forms.Button
    Friend WithEvents folderListLabel As System.Windows.Forms.Label
    Friend WithEvents folderListBox As System.Windows.Forms.ListBox
    Friend WithEvents lengthLabel As System.Windows.Forms.Label
    Friend WithEvents lengthTrackBar As System.Windows.Forms.TrackBar

End Class
