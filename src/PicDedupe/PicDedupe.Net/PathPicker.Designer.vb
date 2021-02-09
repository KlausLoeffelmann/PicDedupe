<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PathPicker
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.btnShowDialog = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.lblCaption, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPath, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnShowDialog, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(594, 42)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblCaption
        '
        Me.lblCaption.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCaption.AutoSize = True
        Me.lblCaption.Location = New System.Drawing.Point(3, 11)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(40, 20)
        Me.lblCaption.TabIndex = 0
        Me.lblCaption.Text = "Path:"
        '
        'txtPath
        '
        Me.txtPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPath.Location = New System.Drawing.Point(49, 7)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(508, 27)
        Me.txtPath.TabIndex = 1
        '
        'btnShowDialog
        '
        Me.btnShowDialog.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnShowDialog.Location = New System.Drawing.Point(563, 7)
        Me.btnShowDialog.Name = "btnShowDialog"
        Me.btnShowDialog.Size = New System.Drawing.Size(28, 27)
        Me.btnShowDialog.TabIndex = 2
        Me.btnShowDialog.Text = "..."
        Me.btnShowDialog.UseVisualStyleBackColor = True
        '
        'PathPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "PathPicker"
        Me.Size = New System.Drawing.Size(594, 42)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblCaption As Label
    Friend WithEvents txtPath As TextBox
    Friend WithEvents btnShowDialog As Button
End Class
