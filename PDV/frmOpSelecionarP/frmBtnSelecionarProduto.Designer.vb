<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBtnSelecionarProduto
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnBT = New System.Windows.Forms.Panel()
        Me.lblindice = New System.Windows.Forms.Label()
        Me.lblNomeProduto = New System.Windows.Forms.Label()
        Me.picbtnProduto = New System.Windows.Forms.PictureBox()
        Me.pnBT.SuspendLayout()
        CType(Me.picbtnProduto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnBT
        '
        Me.pnBT.Controls.Add(Me.lblindice)
        Me.pnBT.Controls.Add(Me.lblNomeProduto)
        Me.pnBT.Controls.Add(Me.picbtnProduto)
        Me.pnBT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnBT.Location = New System.Drawing.Point(0, 0)
        Me.pnBT.Margin = New System.Windows.Forms.Padding(4)
        Me.pnBT.Name = "pnBT"
        Me.pnBT.Size = New System.Drawing.Size(329, 256)
        Me.pnBT.TabIndex = 0
        '
        'lblindice
        '
        Me.lblindice.AutoSize = True
        Me.lblindice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblindice.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblindice.Location = New System.Drawing.Point(19, 20)
        Me.lblindice.Name = "lblindice"
        Me.lblindice.Size = New System.Drawing.Size(23, 25)
        Me.lblindice.TabIndex = 6
        Me.lblindice.Text = "1"
        '
        'lblNomeProduto
        '
        Me.lblNomeProduto.Font = New System.Drawing.Font("Calibri", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNomeProduto.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblNomeProduto.Location = New System.Drawing.Point(24, 20)
        Me.lblNomeProduto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNomeProduto.Name = "lblNomeProduto"
        Me.lblNomeProduto.Size = New System.Drawing.Size(280, 215)
        Me.lblNomeProduto.TabIndex = 5
        Me.lblNomeProduto.Text = "1 - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
        Me.lblNomeProduto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picbtnProduto
        '
        Me.picbtnProduto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picbtnProduto.Image = Global.PDV.My.Resources.Resources.B_1_E
        Me.picbtnProduto.Location = New System.Drawing.Point(0, 0)
        Me.picbtnProduto.Margin = New System.Windows.Forms.Padding(4)
        Me.picbtnProduto.Name = "picbtnProduto"
        Me.picbtnProduto.Size = New System.Drawing.Size(329, 256)
        Me.picbtnProduto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picbtnProduto.TabIndex = 4
        Me.picbtnProduto.TabStop = False
        '
        'frmBtnSelecionarProduto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(329, 256)
        Me.Controls.Add(Me.pnBT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmBtnSelecionarProduto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmBtnSelecionarProduto"
        Me.pnBT.ResumeLayout(False)
        Me.pnBT.PerformLayout()
        CType(Me.picbtnProduto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnBT As Panel
    Friend WithEvents lblNomeProduto As Label
    Friend WithEvents picbtnProduto As PictureBox
    Friend WithEvents lblindice As Label
End Class
