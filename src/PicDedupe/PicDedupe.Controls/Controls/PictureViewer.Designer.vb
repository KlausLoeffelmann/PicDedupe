<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PictureViewer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.pictureBox = New System.Windows.Forms.PictureBox()
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureBox
        '
        Me.pictureBox.Location = New System.Drawing.Point(13, 12)
        Me.pictureBox.Name = "pictureBox"
        Me.pictureBox.Size = New System.Drawing.Size(638, 380)
        Me.pictureBox.TabIndex = 0
        Me.pictureBox.TabStop = False
        '
        'PictureViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pictureBox)
        Me.Name = "PictureViewer"
        Me.Size = New System.Drawing.Size(824, 619)
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pictureBox As PictureBox
End Class
