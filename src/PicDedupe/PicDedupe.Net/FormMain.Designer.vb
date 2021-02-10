<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
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
        Me.PathPicker1 = New PicDedupe.Net.PathPicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.FolderListView1 = New PicDedupe.Net.FolderListView()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PathPicker1
        '
        Me.PathPicker1.DialogTitel = Nothing
        Me.PathPicker1.Location = New System.Drawing.Point(11, 12)
        Me.PathPicker1.Name = "PathPicker1"
        Me.PathPicker1.Size = New System.Drawing.Size(1039, 33)
        Me.PathPicker1.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(5, 73)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.FolderListView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1044, 551)
        Me.SplitContainer1.SplitterDistance = 348
        Me.SplitContainer1.TabIndex = 2
        '
        'FolderListView1
        '
        Me.FolderListView1.FullRowSelect = True
        Me.FolderListView1.HideSelection = False
        Me.FolderListView1.Location = New System.Drawing.Point(24, 25)
        Me.FolderListView1.Name = "FolderListView1"
        Me.FolderListView1.Size = New System.Drawing.Size(989, 284)
        Me.FolderListView1.TabIndex = 0
        Me.FolderListView1.UseCompatibleStateImageBehavior = False
        Me.FolderListView1.View = System.Windows.Forms.View.Details
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1062, 650)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.PathPicker1)
        Me.Name = "FormMain"
        Me.Text = "PicDedupe"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PathPicker1 As PathPicker
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents FolderListView1 As FolderListView
End Class
