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
        Me.SuspendLayout()
        '
        'directoryNameTextBox
        '
        Me.directoryNameTextBox.Location = New System.Drawing.Point(12, 12)
        Me.directoryNameTextBox.Name = "directoryNameTextBox"
        Me.directoryNameTextBox.ReadOnly = True
        Me.directoryNameTextBox.Size = New System.Drawing.Size(151, 20)
        Me.directoryNameTextBox.TabIndex = 0
        '
        'browseButton
        '
        Me.browseButton.Location = New System.Drawing.Point(170, 11)
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
        'pinButton
        '
        Me.pinButton.Location = New System.Drawing.Point(170, 40)
        Me.pinButton.Name = "pinButton"
        Me.pinButton.Size = New System.Drawing.Size(75, 23)
        Me.pinButton.TabIndex = 2
        Me.pinButton.Text = "Pin Folder"
        Me.pinButton.UseVisualStyleBackColor = True
        '
        'renameButton
        '
        Me.renameButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renameButton.Location = New System.Drawing.Point(170, 69)
        Me.renameButton.Name = "renameButton"
        Me.renameButton.Size = New System.Drawing.Size(75, 23)
        Me.renameButton.TabIndex = 3
        Me.renameButton.Text = "&Rename!"
        Me.renameButton.UseVisualStyleBackColor = True
        '
        'hideFilesCheckBox
        '
        Me.hideFilesCheckBox.AutoSize = True
        Me.hideFilesCheckBox.Location = New System.Drawing.Point(13, 44)
        Me.hideFilesCheckBox.Name = "hideFilesCheckBox"
        Me.hideFilesCheckBox.Size = New System.Drawing.Size(120, 17)
        Me.hideFilesCheckBox.TabIndex = 4
        Me.hideFilesCheckBox.Text = "Mark files as &hidden"
        Me.hideFilesCheckBox.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(253, 101)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents directoryNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents browseButton As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents renameButton As System.Windows.Forms.Button
    Friend WithEvents hideFilesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents pinButton As System.Windows.Forms.Button

End Class
