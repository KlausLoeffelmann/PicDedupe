﻿Imports System.Text

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMainOld
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
        Me.components = New System.ComponentModel.Container()
        Me.fileCrawlerPathPicker = New PicDedupe.Controls.PathPicker()
        Me.folderSplitter = New System.Windows.Forms.SplitContainer()
        Me.fileCrawlerFolderListView = New PicDedupe.Controls.FileEntryListView()
        Me.doubletsTreeView = New PicDedupe.Controls.FileEntryTreeView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TotalFileSize = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TotalFileCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ElapsedTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ItemsPerSecondProcessed = New System.Windows.Forms.ToolStripStatusLabel()
        Me.CurrentTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.chkUseNetEnumerator = New System.Windows.Forms.CheckBox()
        CType(Me.folderSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.folderSplitter.Panel1.SuspendLayout()
        Me.folderSplitter.Panel2.SuspendLayout()
        Me.folderSplitter.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'fileCrawlerPathPicker
        '
        Me.fileCrawlerPathPicker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fileCrawlerPathPicker.BrowserPath = Nothing
        Me.fileCrawlerPathPicker.DialogTitel = Nothing
        Me.fileCrawlerPathPicker.Location = New System.Drawing.Point(5, 12)
        Me.fileCrawlerPathPicker.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.fileCrawlerPathPicker.Name = "fileCrawlerPathPicker"
        Me.fileCrawlerPathPicker.Path = Nothing
        Me.fileCrawlerPathPicker.Size = New System.Drawing.Size(825, 38)
        Me.fileCrawlerPathPicker.TabIndex = 1
        '
        'folderSplitter
        '
        Me.folderSplitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.folderSplitter.Location = New System.Drawing.Point(5, 74)
        Me.folderSplitter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.folderSplitter.Name = "folderSplitter"
        Me.folderSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'folderSplitter.Panel1
        '
        Me.folderSplitter.Panel1.Controls.Add(Me.fileCrawlerFolderListView)
        '
        'folderSplitter.Panel2
        '
        Me.folderSplitter.Panel2.Controls.Add(Me.doubletsTreeView)
        Me.folderSplitter.Size = New System.Drawing.Size(1038, 534)
        Me.folderSplitter.SplitterDistance = 333
        Me.folderSplitter.SplitterWidth = 2
        Me.folderSplitter.TabIndex = 2
        '
        'fileCrawlerFolderListView
        '
        Me.fileCrawlerFolderListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fileCrawlerFolderListView.FullRowSelect = True
        Me.fileCrawlerFolderListView.HideSelection = False
        Me.fileCrawlerFolderListView.Location = New System.Drawing.Point(0, 0)
        Me.fileCrawlerFolderListView.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.fileCrawlerFolderListView.Name = "fileCrawlerFolderListView"
        Me.fileCrawlerFolderListView.Size = New System.Drawing.Size(1038, 333)
        Me.fileCrawlerFolderListView.TabIndex = 0
        Me.fileCrawlerFolderListView.UseCompatibleStateImageBehavior = False
        Me.fileCrawlerFolderListView.View = System.Windows.Forms.View.Details
        '
        'doubletsTreeView
        '
        Me.doubletsTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.doubletsTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.doubletsTreeView.Location = New System.Drawing.Point(0, 0)
        Me.doubletsTreeView.Margin = New System.Windows.Forms.Padding(2)
        Me.doubletsTreeView.Name = "doubletsTreeView"
        Me.doubletsTreeView.Size = New System.Drawing.Size(1038, 199)
        Me.doubletsTreeView.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TotalFileSize, Me.TotalFileCount, Me.ElapsedTime, Me.ItemsPerSecondProcessed, Me.CurrentTime})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 624)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1054, 26)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TotalFileSize
        '
        Me.TotalFileSize.Name = "TotalFileSize"
        Me.TotalFileSize.Size = New System.Drawing.Size(207, 20)
        Me.TotalFileSize.Spring = True
        Me.TotalFileSize.Text = "Total file size: - - -"
        '
        'TotalFileCount
        '
        Me.TotalFileCount.Name = "TotalFileCount"
        Me.TotalFileCount.Size = New System.Drawing.Size(207, 20)
        Me.TotalFileCount.Spring = True
        Me.TotalFileCount.Text = "Total file count: - - -"
        '
        'ElapsedTime
        '
        Me.ElapsedTime.Name = "ElapsedTime"
        Me.ElapsedTime.Size = New System.Drawing.Size(207, 20)
        Me.ElapsedTime.Spring = True
        Me.ElapsedTime.Text = "Ellapsed Time: not started."
        '
        'ItemsPerSecondProcessed
        '
        Me.ItemsPerSecondProcessed.Name = "ItemsPerSecondProcessed"
        Me.ItemsPerSecondProcessed.Size = New System.Drawing.Size(207, 20)
        Me.ItemsPerSecondProcessed.Spring = True
        Me.ItemsPerSecondProcessed.Text = "Items per Second:"
        '
        'CurrentTime
        '
        Me.CurrentTime.Name = "CurrentTime"
        Me.CurrentTime.Size = New System.Drawing.Size(207, 20)
        Me.CurrentTime.Spring = True
        Me.CurrentTime.Text = "CurrentTime"
        '
        'chkUseNetEnumerator
        '
        Me.chkUseNetEnumerator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkUseNetEnumerator.AutoSize = True
        Me.chkUseNetEnumerator.Location = New System.Drawing.Point(844, 20)
        Me.chkUseNetEnumerator.Margin = New System.Windows.Forms.Padding(2)
        Me.chkUseNetEnumerator.Name = "chkUseNetEnumerator"
        Me.chkUseNetEnumerator.Size = New System.Drawing.Size(195, 24)
        Me.chkUseNetEnumerator.TabIndex = 4
        Me.chkUseNetEnumerator.Text = "Use .NET file enumerator"
        Me.chkUseNetEnumerator.UseVisualStyleBackColor = True
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1054, 650)
        Me.Controls.Add(Me.fileCrawlerPathPicker)
        Me.Controls.Add(Me.chkUseNetEnumerator)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.folderSplitter)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FormMain"
        Me.Text = "PicDedupe - .NET 5"
        Me.folderSplitter.Panel1.ResumeLayout(False)
        Me.folderSplitter.Panel2.ResumeLayout(False)
        CType(Me.folderSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.folderSplitter.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents fileCrawlerPathPicker As PicDedupe.Controls.PathPicker
    Friend WithEvents folderSplitter As SplitContainer
    Friend WithEvents fileCrawlerFolderListView As PicDedupe.Controls.FileEntryListView
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TotalFileSize As ToolStripStatusLabel
    Friend WithEvents TotalFileCount As ToolStripStatusLabel
    Friend WithEvents ElapsedTime As ToolStripStatusLabel
    Friend WithEvents CurrentTime As ToolStripStatusLabel
    Friend WithEvents ItemsPerSecondProcessed As ToolStripStatusLabel
    Friend WithEvents chkUseNetEnumerator As CheckBox
    Friend WithEvents doubletsTreeView As Controls.FileEntryTreeView
End Class
