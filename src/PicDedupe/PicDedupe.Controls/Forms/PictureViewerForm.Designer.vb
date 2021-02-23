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
        Me.PictureViewer1 = New PicDedupe.Controls.PictureViewer()
        Me.SuspendLayout()
        '
        'PictureViewer1
        '
        Me.PictureViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureViewer1.Location = New System.Drawing.Point(15, 13)
        Me.PictureViewer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureViewer1.Name = "PictureViewer1"
        Me.PictureViewer1.Size = New System.Drawing.Size(627, 360)
        Me.PictureViewer1.TabIndex = 0
        '
        'PictureViewerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(657, 393)
        Me.Controls.Add(Me.PictureViewer1)
        Me.Name = "PictureViewerForm"
        Me.Text = "PictureViewerForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureViewer1 As PictureViewer
End Class
