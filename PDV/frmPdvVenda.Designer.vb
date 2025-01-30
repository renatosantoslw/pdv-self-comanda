<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPdvVenda
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPdvVenda))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblNomeProduto = New System.Windows.Forms.LinkLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblTotalRS = New System.Windows.Forms.Label()
        Me.picValorTotal = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblPesoKG = New System.Windows.Forms.Label()
        Me.picPeso = New System.Windows.Forms.PictureBox()
        Me.lblComandaAtiva = New System.Windows.Forms.Label()
        Me.picEstado = New System.Windows.Forms.PictureBox()
        Me.txtCodBarra = New System.Windows.Forms.TextBox()
        Me.tmrSolicitar = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.picValorTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.picPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEstado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblComandaAtiva)
        Me.SplitContainer1.Panel2.Controls.Add(Me.picEstado)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtCodBarra)
        Me.SplitContainer1.Size = New System.Drawing.Size(1387, 788)
        Me.SplitContainer1.SplitterDistance = 528
        Me.SplitContainer1.TabIndex = 1
        Me.SplitContainer1.Visible = False
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.picLogo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblNomeProduto)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Size = New System.Drawing.Size(528, 788)
        Me.SplitContainer2.SplitterDistance = 412
        Me.SplitContainer2.TabIndex = 2
        Me.SplitContainer2.TabStop = False
        Me.SplitContainer2.Visible = False
        '
        'picLogo
        '
        Me.picLogo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picLogo.Image = Global.PDV.My.Resources.Resources.PESE
        Me.picLogo.Location = New System.Drawing.Point(0, 0)
        Me.picLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(528, 412)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLogo.TabIndex = 1
        Me.picLogo.TabStop = False
        '
        'lblNomeProduto
        '
        Me.lblNomeProduto.ActiveLinkColor = System.Drawing.Color.Red
        Me.lblNomeProduto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNomeProduto.AutoEllipsis = True
        Me.lblNomeProduto.DisabledLinkColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.lblNomeProduto.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNomeProduto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNomeProduto.LinkColor = System.Drawing.Color.LightSkyBlue
        Me.lblNomeProduto.Location = New System.Drawing.Point(3, 277)
        Me.lblNomeProduto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNomeProduto.Name = "lblNomeProduto"
        Me.lblNomeProduto.Size = New System.Drawing.Size(521, 91)
        Me.lblNomeProduto.TabIndex = 1
        Me.lblNomeProduto.TabStop = True
        Me.lblNomeProduto.Text = "Nome produto R$ UN - EX: TESTE NOME"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(528, 274)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblTotalRS)
        Me.Panel3.Controls.Add(Me.picValorTotal)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 139)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(528, 135)
        Me.Panel3.TabIndex = 1
        '
        'lblTotalRS
        '
        Me.lblTotalRS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalRS.AutoEllipsis = True
        Me.lblTotalRS.BackColor = System.Drawing.Color.Black
        Me.lblTotalRS.Font = New System.Drawing.Font("Calibri", 40.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalRS.ForeColor = System.Drawing.Color.White
        Me.lblTotalRS.Location = New System.Drawing.Point(36, 46)
        Me.lblTotalRS.Name = "lblTotalRS"
        Me.lblTotalRS.Size = New System.Drawing.Size(400, 73)
        Me.lblTotalRS.TabIndex = 5
        Me.lblTotalRS.Text = "0,00"
        '
        'picValorTotal
        '
        Me.picValorTotal.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.picValorTotal.Image = Global.PDV.My.Resources.Resources.TOTAL
        Me.picValorTotal.Location = New System.Drawing.Point(0, 1)
        Me.picValorTotal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.picValorTotal.Name = "picValorTotal"
        Me.picValorTotal.Size = New System.Drawing.Size(528, 134)
        Me.picValorTotal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picValorTotal.TabIndex = 3
        Me.picValorTotal.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblPesoKG)
        Me.Panel2.Controls.Add(Me.picPeso)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(528, 139)
        Me.Panel2.TabIndex = 0
        '
        'lblPesoKG
        '
        Me.lblPesoKG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPesoKG.AutoEllipsis = True
        Me.lblPesoKG.BackColor = System.Drawing.Color.Black
        Me.lblPesoKG.Font = New System.Drawing.Font("Calibri", 40.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPesoKG.ForeColor = System.Drawing.Color.White
        Me.lblPesoKG.Location = New System.Drawing.Point(36, 49)
        Me.lblPesoKG.Name = "lblPesoKG"
        Me.lblPesoKG.Size = New System.Drawing.Size(400, 73)
        Me.lblPesoKG.TabIndex = 4
        Me.lblPesoKG.Text = "0,000"
        '
        'picPeso
        '
        Me.picPeso.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.picPeso.Image = Global.PDV.My.Resources.Resources.PESO
        Me.picPeso.Location = New System.Drawing.Point(0, 0)
        Me.picPeso.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.picPeso.Name = "picPeso"
        Me.picPeso.Size = New System.Drawing.Size(528, 139)
        Me.picPeso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPeso.TabIndex = 2
        Me.picPeso.TabStop = False
        '
        'lblComandaAtiva
        '
        Me.lblComandaAtiva.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblComandaAtiva.AutoSize = True
        Me.lblComandaAtiva.BackColor = System.Drawing.Color.Black
        Me.lblComandaAtiva.Font = New System.Drawing.Font("Calibri", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComandaAtiva.ForeColor = System.Drawing.Color.White
        Me.lblComandaAtiva.Location = New System.Drawing.Point(56, 48)
        Me.lblComandaAtiva.Name = "lblComandaAtiva"
        Me.lblComandaAtiva.Size = New System.Drawing.Size(314, 54)
        Me.lblComandaAtiva.TabIndex = 5
        Me.lblComandaAtiva.Text = "Comanda Ativa:"
        Me.lblComandaAtiva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComandaAtiva.Visible = False
        '
        'picEstado
        '
        Me.picEstado.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.picEstado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picEstado.Image = Global.PDV.My.Resources.Resources.COLOQUE
        Me.picEstado.Location = New System.Drawing.Point(0, 0)
        Me.picEstado.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.picEstado.Name = "picEstado"
        Me.picEstado.Size = New System.Drawing.Size(855, 788)
        Me.picEstado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEstado.TabIndex = 2
        Me.picEstado.TabStop = False
        '
        'txtCodBarra
        '
        Me.txtCodBarra.BackColor = System.Drawing.Color.White
        Me.txtCodBarra.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCodBarra.Font = New System.Drawing.Font("Verdana", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodBarra.Location = New System.Drawing.Point(56, 121)
        Me.txtCodBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodBarra.MaxLength = 13
        Me.txtCodBarra.Name = "txtCodBarra"
        Me.txtCodBarra.Size = New System.Drawing.Size(315, 49)
        Me.txtCodBarra.TabIndex = 3
        Me.txtCodBarra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tmrSolicitar
        '
        Me.tmrSolicitar.Interval = 10000
        '
        'frmPdvVenda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1387, 788)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmPdvVenda"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Venda"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.picValorTotal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.picPeso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEstado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents picEstado As PictureBox
    Friend WithEvents picLogo As PictureBox
    Friend WithEvents picValorTotal As PictureBox
    Friend WithEvents picPeso As PictureBox
    Friend WithEvents lblPesoKG As Label
    Friend WithEvents lblTotalRS As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents tmrSolicitar As Timer
    Friend WithEvents lblComandaAtiva As Label
    Friend WithEvents txtCodBarra As TextBox
    Friend WithEvents lblNomeProduto As LinkLabel
End Class
