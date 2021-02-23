<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PictureViewerForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PictureViewerForm))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbFitPage = New System.Windows.Forms.ToolStripButton()
        Me.tsbOriginalSize = New System.Windows.Forms.ToolStripButton()
        Me.PictureViewer1 = New PicDedupe.Controls.PictureViewer()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(36, 36)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbFitPage, Me.tsbOriginalSize})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(739, 45)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbFitPage
        '
        Me.tsbFitPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbFitPage.Font = New System.Drawing.Font("Segoe MDL2 Assets", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbFitPage.Image = CType(resources.GetObject("tsbFitPage.Image"), System.Drawing.Image)
        Me.tsbFitPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFitPage.Name = "tsbFitPage"
        Me.tsbFitPage.Size = New System.Drawing.Size(55, 40)
        Me.tsbFitPage.Text = ""
        Me.tsbFitPage.ToolTipText = "Fit Window"
        '
        'tsbOriginalSize
        '
        Me.tsbOriginalSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbOriginalSize.Font = New System.Drawing.Font("Segoe MDL2 Assets", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbOriginalSize.Image = CType(resources.GetObject("tsbOriginalSize.Image"), System.Drawing.Image)
        Me.tsbOriginalSize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOriginalSize.Name = "tsbOriginalSize"
        Me.tsbOriginalSize.Size = New System.Drawing.Size(55, 40)
        Me.tsbOriginalSize.Text = ""
        Me.tsbOriginalSize.ToolTipText = "Original Size"
        '
        'PictureViewer1
        '
        Me.PictureViewer1.AutoScroll = True
        Me.PictureViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureViewer1.Location = New System.Drawing.Point(0, 45)
        Me.PictureViewer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureViewer1.Name = "PictureViewer1"
        Me.PictureViewer1.Size = New System.Drawing.Size(739, 446)
        Me.PictureViewer1.TabIndex = 0
        '
        'PictureViewerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 491)
        Me.Controls.Add(Me.PictureViewer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "PictureViewerForm"
        Me.Text = "PictureViewerForm"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureViewer1 As PictureViewer
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbFitPage As ToolStripButton
    Friend WithEvents tsbOriginalSize As ToolStripButton
End Class
