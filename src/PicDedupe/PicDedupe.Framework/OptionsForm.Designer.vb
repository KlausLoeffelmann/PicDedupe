<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsForm
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
        Me._takeFilenameDifferencesIntoAccountCheckBox = New System.Windows.Forms.CheckBox()
        Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._takeFileOfCertainSizeIntoAccountCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._fileSizeToTakeIntoAccountNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me._okButton = New System.Windows.Forms.Button()
        Me._cancelButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me._newFileTypesTextBox = New System.Windows.Forms.TextBox()
        Me._addNewFileTypeCateogryButton = New System.Windows.Forms.Button()
        Me._fileTypesListBox = New System.Windows.Forms.ListBox()
        Me._deleteFileCategoryButton = New System.Windows.Forms.Button()
        CType(Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._fileSizeToTakeIntoAccountNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        '_takeFilenameDifferencesIntoAccountCheckBox
        '
        Me._takeFilenameDifferencesIntoAccountCheckBox.AutoSize = True
        Me._takeFilenameDifferencesIntoAccountCheckBox.Location = New System.Drawing.Point(6, 61)
        Me._takeFilenameDifferencesIntoAccountCheckBox.Name = "_takeFilenameDifferencesIntoAccountCheckBox"
        Me._takeFilenameDifferencesIntoAccountCheckBox.Size = New System.Drawing.Size(372, 21)
        Me._takeFilenameDifferencesIntoAccountCheckBox.TabIndex = 3
        Me._takeFilenameDifferencesIntoAccountCheckBox.Text = "Take file name differences into account for files > than"
        Me._takeFilenameDifferencesIntoAccountCheckBox.UseVisualStyleBackColor = True
        '
        '_filesizeForNameDifferencesToTakeIntoAccountNumericUpDown
        '
        Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown.Location = New System.Drawing.Point(384, 61)
        Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown.Name = "_filesizeForNameDifferencesToTakeIntoAccountNumericUpDown"
        Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown.Size = New System.Drawing.Size(94, 22)
        Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(484, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Megabytes"
        '
        '_takeFileOfCertainSizeIntoAccountCheckBox
        '
        Me._takeFileOfCertainSizeIntoAccountCheckBox.AutoSize = True
        Me._takeFileOfCertainSizeIntoAccountCheckBox.Location = New System.Drawing.Point(6, 34)
        Me._takeFileOfCertainSizeIntoAccountCheckBox.Name = "_takeFileOfCertainSizeIntoAccountCheckBox"
        Me._takeFileOfCertainSizeIntoAccountCheckBox.Size = New System.Drawing.Size(244, 21)
        Me._takeFileOfCertainSizeIntoAccountCheckBox.TabIndex = 0
        Me._takeFileOfCertainSizeIntoAccountCheckBox.Text = "Only take files into account > than"
        Me._takeFileOfCertainSizeIntoAccountCheckBox.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(355, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Megabytes"
        '
        '_fileSizeToTakeIntoAccountNumericUpDown
        '
        Me._fileSizeToTakeIntoAccountNumericUpDown.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me._fileSizeToTakeIntoAccountNumericUpDown.Location = New System.Drawing.Point(255, 34)
        Me._fileSizeToTakeIntoAccountNumericUpDown.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me._fileSizeToTakeIntoAccountNumericUpDown.Name = "_fileSizeToTakeIntoAccountNumericUpDown"
        Me._fileSizeToTakeIntoAccountNumericUpDown.Size = New System.Drawing.Size(94, 22)
        Me._fileSizeToTakeIntoAccountNumericUpDown.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me._takeFileOfCertainSizeIntoAccountCheckBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me._takeFilenameDifferencesIntoAccountCheckBox)
        Me.GroupBox1.Controls.Add(Me._fileSizeToTakeIntoAccountNumericUpDown)
        Me.GroupBox1.Controls.Add(Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(592, 122)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Doublet search options"
        '
        '_okButton
        '
        Me._okButton.Location = New System.Drawing.Point(396, 328)
        Me._okButton.Name = "_okButton"
        Me._okButton.Size = New System.Drawing.Size(100, 30)
        Me._okButton.TabIndex = 2
        Me._okButton.Text = "OK"
        Me._okButton.UseVisualStyleBackColor = True
        '
        '_cancelButton
        '
        Me._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me._cancelButton.Location = New System.Drawing.Point(504, 328)
        Me._cancelButton.Name = "_cancelButton"
        Me._cancelButton.Size = New System.Drawing.Size(100, 30)
        Me._cancelButton.TabIndex = 3
        Me._cancelButton.Text = "Cancel"
        Me._cancelButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me._deleteFileCategoryButton)
        Me.GroupBox2.Controls.Add(Me._fileTypesListBox)
        Me.GroupBox2.Controls.Add(Me._addNewFileTypeCateogryButton)
        Me.GroupBox2.Controls.Add(Me._newFileTypesTextBox)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 140)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(592, 180)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Manage File Types"
        '
        '_newFileTypesTextBox
        '
        Me._newFileTypesTextBox.Location = New System.Drawing.Point(6, 145)
        Me._newFileTypesTextBox.Name = "_newFileTypesTextBox"
        Me._newFileTypesTextBox.Size = New System.Drawing.Size(500, 22)
        Me._newFileTypesTextBox.TabIndex = 2
        '
        '_addNewFileTypeCateogryButton
        '
        Me._addNewFileTypeCateogryButton.Location = New System.Drawing.Point(512, 143)
        Me._addNewFileTypeCateogryButton.Name = "_addNewFileTypeCateogryButton"
        Me._addNewFileTypeCateogryButton.Size = New System.Drawing.Size(74, 26)
        Me._addNewFileTypeCateogryButton.TabIndex = 3
        Me._addNewFileTypeCateogryButton.Text = "Add"
        Me._addNewFileTypeCateogryButton.UseVisualStyleBackColor = True
        '
        '_fileTypesListBox
        '
        Me._fileTypesListBox.FormattingEnabled = True
        Me._fileTypesListBox.ItemHeight = 16
        Me._fileTypesListBox.Location = New System.Drawing.Point(6, 23)
        Me._fileTypesListBox.Name = "_fileTypesListBox"
        Me._fileTypesListBox.Size = New System.Drawing.Size(500, 100)
        Me._fileTypesListBox.TabIndex = 0
        '
        '_deleteFileCategoryButton
        '
        Me._deleteFileCategoryButton.Location = New System.Drawing.Point(512, 22)
        Me._deleteFileCategoryButton.Name = "_deleteFileCategoryButton"
        Me._deleteFileCategoryButton.Size = New System.Drawing.Size(74, 26)
        Me._deleteFileCategoryButton.TabIndex = 1
        Me._deleteFileCategoryButton.Text = "Delete"
        Me._deleteFileCategoryButton.UseVisualStyleBackColor = True
        '
        'OptionsForm
        '
        Me.AcceptButton = Me._okButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me._cancelButton
        Me.ClientSize = New System.Drawing.Size(621, 370)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me._cancelButton)
        Me.Controls.Add(Me._okButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "OptionsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        CType(Me._filesizeForNameDifferencesToTakeIntoAccountNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._fileSizeToTakeIntoAccountNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents _takeFilenameDifferencesIntoAccountCheckBox As CheckBox
    Friend WithEvents _filesizeForNameDifferencesToTakeIntoAccountNumericUpDown As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents _takeFileOfCertainSizeIntoAccountCheckBox As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents _fileSizeToTakeIntoAccountNumericUpDown As NumericUpDown
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents _okButton As Button
    Friend WithEvents _cancelButton As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents _deleteFileCategoryButton As Button
    Friend WithEvents _fileTypesListBox As ListBox
    Friend WithEvents _addNewFileTypeCateogryButton As Button
    Friend WithEvents _newFileTypesTextBox As TextBox
End Class
