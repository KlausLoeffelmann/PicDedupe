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
        Me.folderSplitter = New System.Windows.Forms.SplitContainer()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TotalFileSize = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TotalFileCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ElapsedTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.CurrentTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.fileCrawlerFolderListView = New PicDedupe.Controls.FolderListView()
        Me.fileCrawlerPathPicker = New PicDedupe.Controls.PathPicker()
        CType(Me.folderSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.folderSplitter.Panel1.SuspendLayout()
        Me.folderSplitter.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'folderSplitter
        '
        Me.folderSplitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.folderSplitter.Location = New System.Drawing.Point(4, 47)
        Me.folderSplitter.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.folderSplitter.Name = "folderSplitter"
        Me.folderSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'folderSplitter.Panel1
        '
        Me.folderSplitter.Panel1.Controls.Add(Me.fileCrawlerFolderListView)
        Me.folderSplitter.Size = New System.Drawing.Size(799, 368)
        Me.folderSplitter.SplitterDistance = 231
        Me.folderSplitter.SplitterWidth = 2
        Me.folderSplitter.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TotalFileSize, Me.TotalFileCount, Me.ElapsedTime, Me.CurrentTime})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 429)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(812, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TotalFileSize
        '
        Me.TotalFileSize.Name = "TotalFileSize"
        Me.TotalFileSize.Size = New System.Drawing.Size(240, 17)
        Me.TotalFileSize.Spring = True
        Me.TotalFileSize.Text = "Total file size: - - -"
        '
        'TotalFileCount
        '
        Me.TotalFileCount.Name = "TotalFileCount"
        Me.TotalFileCount.Size = New System.Drawing.Size(240, 17)
        Me.TotalFileCount.Spring = True
        Me.TotalFileCount.Text = "Total file count: - - -"
        '
        'ElapsedTime
        '
        Me.ElapsedTime.Name = "ElapsedTime"
        Me.ElapsedTime.Size = New System.Drawing.Size(240, 17)
        Me.ElapsedTime.Spring = True
        Me.ElapsedTime.Text = "Ellapsed Time: not started."
        '
        'CurrentTime
        '
        Me.CurrentTime.Name = "CurrentTime"
        Me.CurrentTime.Size = New System.Drawing.Size(79, 17)
        Me.CurrentTime.Text = "Current Time:"
        '
        'fileCrawlerFolderListView
        '
        Me.fileCrawlerFolderListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fileCrawlerFolderListView.FullRowSelect = True
        Me.fileCrawlerFolderListView.HideSelection = False
        Me.fileCrawlerFolderListView.Location = New System.Drawing.Point(0, 0)
        Me.fileCrawlerFolderListView.Margin = New System.Windows.Forms.Padding(2)
        Me.fileCrawlerFolderListView.Name = "fileCrawlerFolderListView"
        Me.fileCrawlerFolderListView.Size = New System.Drawing.Size(799, 231)
        Me.fileCrawlerFolderListView.TabIndex = 0
        Me.fileCrawlerFolderListView.UseCompatibleStateImageBehavior = False
        Me.fileCrawlerFolderListView.View = System.Windows.Forms.View.Details
        '
        'fileCrawlerPathPicker
        '
        Me.fileCrawlerPathPicker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fileCrawlerPathPicker.DialogTitel = Nothing
        Me.fileCrawlerPathPicker.Location = New System.Drawing.Point(4, 8)
        Me.fileCrawlerPathPicker.Margin = New System.Windows.Forms.Padding(2)
        Me.fileCrawlerPathPicker.Name = "fileCrawlerPathPicker"
        Me.fileCrawlerPathPicker.Path = Nothing
        Me.fileCrawlerPathPicker.Size = New System.Drawing.Size(797, 27)
        Me.fileCrawlerPathPicker.TabIndex = 1
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 451)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.folderSplitter)
        Me.Controls.Add(Me.fileCrawlerPathPicker)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
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
    Friend WithEvents CurrentTime As ToolStripStatusLabel
End Class
