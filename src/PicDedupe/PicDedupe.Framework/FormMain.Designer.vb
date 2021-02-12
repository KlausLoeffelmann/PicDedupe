<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.fileCrawlerPathPicker = New PicDedupe.Controls.PathPicker()
        Me.folderSplitter = New System.Windows.Forms.SplitContainer()
        Me.fileCrawlerFolderListView = New PicDedupe.Controls.FolderListView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TotalFileSize = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TotalFileCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ElapsedTime = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.folderSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.folderSplitter.Panel1.SuspendLayout()
        Me.folderSplitter.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'fileCrawlerPathPicker
        '
        Me.fileCrawlerPathPicker.DialogTitel = Nothing
        Me.fileCrawlerPathPicker.Location = New System.Drawing.Point(11, 10)
        Me.fileCrawlerPathPicker.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.fileCrawlerPathPicker.Name = "fileCrawlerPathPicker"
        Me.fileCrawlerPathPicker.Path = Nothing
        Me.fileCrawlerPathPicker.Size = New System.Drawing.Size(1038, 30)
        Me.fileCrawlerPathPicker.TabIndex = 1
        '
        'folderSplitter
        '
        Me.folderSplitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.folderSplitter.Location = New System.Drawing.Point(5, 58)
        Me.folderSplitter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.folderSplitter.Name = "folderSplitter"
        Me.folderSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'folderSplitter.Panel1
        '
        Me.folderSplitter.Panel1.Controls.Add(Me.fileCrawlerFolderListView)
        Me.folderSplitter.Size = New System.Drawing.Size(1077, 453)
        Me.folderSplitter.SplitterDistance = 285
        Me.folderSplitter.SplitterWidth = 3
        Me.folderSplitter.TabIndex = 2
        '
        'fileCrawlerFolderListView
        '
        Me.fileCrawlerFolderListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fileCrawlerFolderListView.FullRowSelect = True
        Me.fileCrawlerFolderListView.HideSelection = False
        Me.fileCrawlerFolderListView.Location = New System.Drawing.Point(7, 14)
        Me.fileCrawlerFolderListView.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.fileCrawlerFolderListView.Name = "fileCrawlerFolderListView"
        Me.fileCrawlerFolderListView.Size = New System.Drawing.Size(1067, 270)
        Me.fileCrawlerFolderListView.TabIndex = 0
        Me.fileCrawlerFolderListView.UseCompatibleStateImageBehavior = False
        Me.fileCrawlerFolderListView.View = System.Windows.Forms.View.Details
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TotalFileSize, Me.TotalFileCount, Me.ElapsedTime})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 529)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1095, 26)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TotalFileSize
        '
        Me.TotalFileSize.Name = "TotalFileSize"
        Me.TotalFileSize.Size = New System.Drawing.Size(360, 20)
        Me.TotalFileSize.Spring = True
        Me.TotalFileSize.Text = "Total file size: - - -"
        '
        'TotalFileCount
        '
        Me.TotalFileCount.Name = "TotalFileCount"
        Me.TotalFileCount.Size = New System.Drawing.Size(360, 20)
        Me.TotalFileCount.Spring = True
        Me.TotalFileCount.Text = "Total file count: - - -"
        '
        'ElapsedTime
        '
        Me.ElapsedTime.Name = "ElapsedTime"
        Me.ElapsedTime.Size = New System.Drawing.Size(360, 20)
        Me.ElapsedTime.Spring = True
        Me.ElapsedTime.Text = "Ellapsed Time: not started."
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1095, 555)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.folderSplitter)
        Me.Controls.Add(Me.fileCrawlerPathPicker)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FormMain"
        Me.Text = "PicDedupe"
        Me.folderSplitter.Panel1.ResumeLayout(False)
        CType(Me.folderSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.folderSplitter.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents fileCrawlerPathPicker As PicDedupe.Controls.PathPicker
    Friend WithEvents folderSplitter As SplitContainer
    Friend WithEvents fileCrawlerFolderListView As PicDedupe.Controls.FolderListView
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TotalFileSize As ToolStripStatusLabel
    Friend WithEvents TotalFileCount As ToolStripStatusLabel
    Friend WithEvents ElapsedTime As ToolStripStatusLabel
End Class
