<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig))
        Me.tbcConfig = New System.Windows.Forms.TabControl()
        Me.tbpConexao = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblTesteConn = New System.Windows.Forms.LinkLabel()
        Me.lblLinha = New System.Windows.Forms.Label()
        Me.txtSenhaBanco = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtUsuarioBanco = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNomeBanco = New System.Windows.Forms.TextBox()
        Me.txtEnderecoServidor = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tbpConfig = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblDescriBalanca = New System.Windows.Forms.Label()
        Me.cbModeloBalanca = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbControleFluxo = New System.Windows.Forms.ComboBox()
        Me.cbBitsDeParada = New System.Windows.Forms.ComboBox()
        Me.cbParidade = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbBitsDeDados = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbBitsPorSegundo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbPortaCOM = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbpConfiguracao = New System.Windows.Forms.TabPage()
        Me.lblDescriMenuF12 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtCodProdutoOpcionais = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblConsultar = New System.Windows.Forms.LinkLabel()
        Me.txtCodDeposito = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCodVendedor = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtCodEmpresa = New System.Windows.Forms.TextBox()
        Me.txtCodProduto = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tbpImpressao = New System.Windows.Forms.TabPage()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.chkImprimirLogo = New System.Windows.Forms.CheckBox()
        Me.chkImprimirQR = New System.Windows.Forms.CheckBox()
        Me.txtRodape = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.chkImprimirCodBar = New System.Windows.Forms.CheckBox()
        Me.lblLogo = New System.Windows.Forms.Label()
        Me.btnSelecionarLogo = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cbModeloLayout = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbImpressora = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.tbComanda = New System.Windows.Forms.TabPage()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.lblZerarComanda = New System.Windows.Forms.LinkLabel()
        Me.txtCodigoMesa = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cbModoAuto = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbPrefixoComanda = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.tbRel = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.lblGerar = New System.Windows.Forms.LinkLabel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.dtFim = New System.Windows.Forms.DateTimePicker()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.dtInicio = New System.Windows.Forms.DateTimePicker()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtFimKG = New System.Windows.Forms.TextBox()
        Me.txtInicioKG = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.btnReal = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAplicar = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.pictBoxLogoPainel = New System.Windows.Forms.PictureBox()
        Me.lblPainel = New System.Windows.Forms.Label()
        Me.pictBoxPainel = New System.Windows.Forms.PictureBox()
        Me.btnTeste = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tbcConfig.SuspendLayout()
        Me.tbpConexao.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.tbpConfig.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tbpConfiguracao.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.tbpImpressao.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.tbComanda.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.tbRel.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.pictBoxLogoPainel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictBoxPainel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbcConfig
        '
        Me.tbcConfig.Controls.Add(Me.tbpConexao)
        Me.tbcConfig.Controls.Add(Me.tbpConfig)
        Me.tbcConfig.Controls.Add(Me.tbpConfiguracao)
        Me.tbcConfig.Controls.Add(Me.tbpImpressao)
        Me.tbcConfig.Controls.Add(Me.tbComanda)
        Me.tbcConfig.Controls.Add(Me.tbRel)
        Me.tbcConfig.Location = New System.Drawing.Point(21, 100)
        Me.tbcConfig.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbcConfig.Name = "tbcConfig"
        Me.tbcConfig.SelectedIndex = 0
        Me.tbcConfig.Size = New System.Drawing.Size(829, 326)
        Me.tbcConfig.TabIndex = 0
        '
        'tbpConexao
        '
        Me.tbpConexao.Controls.Add(Me.GroupBox3)
        Me.tbpConexao.Location = New System.Drawing.Point(4, 25)
        Me.tbpConexao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbpConexao.Name = "tbpConexao"
        Me.tbpConexao.Size = New System.Drawing.Size(821, 297)
        Me.tbpConexao.TabIndex = 1
        Me.tbpConexao.Text = "Conexão"
        Me.tbpConexao.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblTesteConn)
        Me.GroupBox3.Controls.Add(Me.lblLinha)
        Me.GroupBox3.Controls.Add(Me.txtSenhaBanco)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.txtUsuarioBanco)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.txtNomeBanco)
        Me.GroupBox3.Controls.Add(Me.txtEnderecoServidor)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox3.Location = New System.Drawing.Point(23, 18)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Banco de dados"
        '
        'lblTesteConn
        '
        Me.lblTesteConn.AutoSize = True
        Me.lblTesteConn.LinkColor = System.Drawing.Color.SteelBlue
        Me.lblTesteConn.Location = New System.Drawing.Point(27, 172)
        Me.lblTesteConn.Name = "lblTesteConn"
        Me.lblTesteConn.Size = New System.Drawing.Size(61, 16)
        Me.lblTesteConn.TabIndex = 3
        Me.lblTesteConn.TabStop = True
        Me.lblTesteConn.Text = "Conectar"
        '
        'lblLinha
        '
        Me.lblLinha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLinha.Location = New System.Drawing.Point(28, 132)
        Me.lblLinha.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLinha.Name = "lblLinha"
        Me.lblLinha.Size = New System.Drawing.Size(311, 2)
        Me.lblLinha.TabIndex = 70
        '
        'txtSenhaBanco
        '
        Me.txtSenhaBanco.Location = New System.Drawing.Point(187, 206)
        Me.txtSenhaBanco.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtSenhaBanco.MaxLength = 15
        Me.txtSenhaBanco.Name = "txtSenhaBanco"
        Me.txtSenhaBanco.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txtSenhaBanco.Size = New System.Drawing.Size(153, 22)
        Me.txtSenhaBanco.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(129, 207)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 16)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Senha:"
        '
        'txtUsuarioBanco
        '
        Me.txtUsuarioBanco.Location = New System.Drawing.Point(188, 170)
        Me.txtUsuarioBanco.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtUsuarioBanco.MaxLength = 15
        Me.txtUsuarioBanco.Name = "txtUsuarioBanco"
        Me.txtUsuarioBanco.Size = New System.Drawing.Size(153, 22)
        Me.txtUsuarioBanco.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(123, 172)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 16)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Usuario:"
        '
        'txtNomeBanco
        '
        Me.txtNomeBanco.Location = New System.Drawing.Point(187, 68)
        Me.txtNomeBanco.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtNomeBanco.MaxLength = 50
        Me.txtNomeBanco.Name = "txtNomeBanco"
        Me.txtNomeBanco.Size = New System.Drawing.Size(153, 22)
        Me.txtNomeBanco.TabIndex = 7
        '
        'txtEnderecoServidor
        '
        Me.txtEnderecoServidor.Location = New System.Drawing.Point(187, 34)
        Me.txtEnderecoServidor.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtEnderecoServidor.MaxLength = 30
        Me.txtEnderecoServidor.Name = "txtEnderecoServidor"
        Me.txtEnderecoServidor.Size = New System.Drawing.Size(153, 22)
        Me.txtEnderecoServidor.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(27, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(149, 16)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Nome banco de dados:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(117, 36)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 16)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Servidor:"
        '
        'tbpConfig
        '
        Me.tbpConfig.Controls.Add(Me.GroupBox2)
        Me.tbpConfig.Controls.Add(Me.GroupBox1)
        Me.tbpConfig.Location = New System.Drawing.Point(4, 25)
        Me.tbpConfig.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbpConfig.Name = "tbpConfig"
        Me.tbpConfig.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbpConfig.Size = New System.Drawing.Size(821, 297)
        Me.tbpConfig.TabIndex = 0
        Me.tbpConfig.Text = "Balança"
        Me.tbpConfig.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblDescriBalanca)
        Me.GroupBox2.Controls.Add(Me.cbModeloBalanca)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox2.Location = New System.Drawing.Point(23, 18)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Modelo"
        '
        'lblDescriBalanca
        '
        Me.lblDescriBalanca.AutoSize = True
        Me.lblDescriBalanca.Location = New System.Drawing.Point(183, 70)
        Me.lblDescriBalanca.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescriBalanca.Name = "lblDescriBalanca"
        Me.lblDescriBalanca.Size = New System.Drawing.Size(140, 16)
        Me.lblDescriBalanca.TabIndex = 4
        Me.lblDescriBalanca.Text = "Protocolo: Prt3 - C16 D"
        '
        'cbModeloBalanca
        '
        Me.cbModeloBalanca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbModeloBalanca.FormattingEnabled = True
        Me.cbModeloBalanca.Items.AddRange(New Object() {"Toledo"})
        Me.cbModeloBalanca.Location = New System.Drawing.Point(187, 33)
        Me.cbModeloBalanca.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbModeloBalanca.Name = "cbModeloBalanca"
        Me.cbModeloBalanca.Size = New System.Drawing.Size(153, 24)
        Me.cbModeloBalanca.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(69, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(108, 16)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Modelo balança:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbControleFluxo)
        Me.GroupBox1.Controls.Add(Me.cbBitsDeParada)
        Me.GroupBox1.Controls.Add(Me.cbParidade)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbBitsDeDados)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbBitsPorSegundo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cbPortaCOM)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox1.Location = New System.Drawing.Point(421, 18)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Porta Serial"
        '
        'cbControleFluxo
        '
        Me.cbControleFluxo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbControleFluxo.FormattingEnabled = True
        Me.cbControleFluxo.Items.AddRange(New Object() {"Xon / Xoff", "Hardware", "Nenhum"})
        Me.cbControleFluxo.Location = New System.Drawing.Point(188, 203)
        Me.cbControleFluxo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbControleFluxo.Name = "cbControleFluxo"
        Me.cbControleFluxo.Size = New System.Drawing.Size(153, 24)
        Me.cbControleFluxo.TabIndex = 11
        '
        'cbBitsDeParada
        '
        Me.cbBitsDeParada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBitsDeParada.FormattingEnabled = True
        Me.cbBitsDeParada.Items.AddRange(New Object() {"1", "1,5", "2"})
        Me.cbBitsDeParada.Location = New System.Drawing.Point(188, 169)
        Me.cbBitsDeParada.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbBitsDeParada.Name = "cbBitsDeParada"
        Me.cbBitsDeParada.Size = New System.Drawing.Size(153, 24)
        Me.cbBitsDeParada.TabIndex = 10
        '
        'cbParidade
        '
        Me.cbParidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbParidade.FormattingEnabled = True
        Me.cbParidade.Items.AddRange(New Object() {"Even", "Odd", "Nenhum", "Mark", "Space"})
        Me.cbParidade.Location = New System.Drawing.Point(188, 135)
        Me.cbParidade.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbParidade.Name = "cbParidade"
        Me.cbParidade.Size = New System.Drawing.Size(153, 24)
        Me.cbParidade.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(65, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Controle de fluxo:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(79, 172)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Bits de parada:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(115, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Paridade:"
        '
        'cbBitsDeDados
        '
        Me.cbBitsDeDados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBitsDeDados.FormattingEnabled = True
        Me.cbBitsDeDados.Items.AddRange(New Object() {"4", "5", "6", "7", "8"})
        Me.cbBitsDeDados.Location = New System.Drawing.Point(188, 101)
        Me.cbBitsDeDados.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbBitsDeDados.Name = "cbBitsDeDados"
        Me.cbBitsDeDados.Size = New System.Drawing.Size(153, 24)
        Me.cbBitsDeDados.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(85, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Bits de dados:"
        '
        'cbBitsPorSegundo
        '
        Me.cbBitsPorSegundo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBitsPorSegundo.FormattingEnabled = True
        Me.cbBitsPorSegundo.Items.AddRange(New Object() {"2400", "4800", "9600"})
        Me.cbBitsPorSegundo.Location = New System.Drawing.Point(188, 66)
        Me.cbBitsPorSegundo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbBitsPorSegundo.Name = "cbBitsPorSegundo"
        Me.cbBitsPorSegundo.Size = New System.Drawing.Size(153, 24)
        Me.cbBitsPorSegundo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(64, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Bist por segundo:"
        '
        'cbPortaCOM
        '
        Me.cbPortaCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPortaCOM.FormattingEnabled = True
        Me.cbPortaCOM.Location = New System.Drawing.Point(188, 33)
        Me.cbPortaCOM.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbPortaCOM.Name = "cbPortaCOM"
        Me.cbPortaCOM.Size = New System.Drawing.Size(153, 24)
        Me.cbPortaCOM.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(29, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Número da porta COM:"
        '
        'tbpConfiguracao
        '
        Me.tbpConfiguracao.Controls.Add(Me.lblDescriMenuF12)
        Me.tbpConfiguracao.Controls.Add(Me.GroupBox4)
        Me.tbpConfiguracao.Location = New System.Drawing.Point(4, 25)
        Me.tbpConfiguracao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbpConfiguracao.Name = "tbpConfiguracao"
        Me.tbpConfiguracao.Size = New System.Drawing.Size(821, 297)
        Me.tbpConfiguracao.TabIndex = 2
        Me.tbpConfiguracao.Text = "Produto"
        Me.tbpConfiguracao.UseVisualStyleBackColor = True
        '
        'lblDescriMenuF12
        '
        Me.lblDescriMenuF12.AutoSize = True
        Me.lblDescriMenuF12.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblDescriMenuF12.Location = New System.Drawing.Point(435, 52)
        Me.lblDescriMenuF12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescriMenuF12.Name = "lblDescriMenuF12"
        Me.lblDescriMenuF12.Size = New System.Drawing.Size(193, 16)
        Me.lblDescriMenuF12.TabIndex = 5
        Me.lblDescriMenuF12.Text = "Menu Opcionais: Teclado - F12"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.txtCodProdutoOpcionais)
        Me.GroupBox4.Controls.Add(Me.Label26)
        Me.GroupBox4.Controls.Add(Me.lblConsultar)
        Me.GroupBox4.Controls.Add(Me.txtCodDeposito)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.txtCodVendedor)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtCodEmpresa)
        Me.GroupBox4.Controls.Add(Me.txtCodProduto)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox4.Location = New System.Drawing.Point(23, 18)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Produto"
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Location = New System.Drawing.Point(31, 110)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(311, 2)
        Me.Label19.TabIndex = 78
        '
        'txtCodProdutoOpcionais
        '
        Me.txtCodProdutoOpcionais.Location = New System.Drawing.Point(187, 69)
        Me.txtCodProdutoOpcionais.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCodProdutoOpcionais.Name = "txtCodProdutoOpcionais"
        Me.txtCodProdutoOpcionais.Size = New System.Drawing.Size(153, 22)
        Me.txtCodProdutoOpcionais.TabIndex = 77
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(27, 71)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(150, 16)
        Me.Label26.TabIndex = 76
        Me.Label26.Text = "Codigo(s) Prod. Opcion:"
        '
        'lblConsultar
        '
        Me.lblConsultar.AutoSize = True
        Me.lblConsultar.LinkColor = System.Drawing.Color.SteelBlue
        Me.lblConsultar.Location = New System.Drawing.Point(184, 226)
        Me.lblConsultar.Name = "lblConsultar"
        Me.lblConsultar.Size = New System.Drawing.Size(63, 16)
        Me.lblConsultar.TabIndex = 75
        Me.lblConsultar.TabStop = True
        Me.lblConsultar.Text = "Consultar"
        '
        'txtCodDeposito
        '
        Me.txtCodDeposito.Location = New System.Drawing.Point(187, 194)
        Me.txtCodDeposito.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCodDeposito.Name = "txtCodDeposito"
        Me.txtCodDeposito.Size = New System.Drawing.Size(153, 22)
        Me.txtCodDeposito.TabIndex = 74
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(68, 198)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(112, 16)
        Me.Label13.TabIndex = 73
        Me.Label13.Text = "Codigo Deposito:"
        '
        'txtCodVendedor
        '
        Me.txtCodVendedor.Location = New System.Drawing.Point(187, 162)
        Me.txtCodVendedor.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCodVendedor.Name = "txtCodVendedor"
        Me.txtCodVendedor.Size = New System.Drawing.Size(153, 22)
        Me.txtCodVendedor.TabIndex = 72
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(61, 165)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(117, 16)
        Me.Label18.TabIndex = 71
        Me.Label18.Text = "Codigo Vendedor:"
        '
        'txtCodEmpresa
        '
        Me.txtCodEmpresa.Location = New System.Drawing.Point(187, 127)
        Me.txtCodEmpresa.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCodEmpresa.Name = "txtCodEmpresa"
        Me.txtCodEmpresa.Size = New System.Drawing.Size(153, 22)
        Me.txtCodEmpresa.TabIndex = 7
        '
        'txtCodProduto
        '
        Me.txtCodProduto.Location = New System.Drawing.Point(187, 34)
        Me.txtCodProduto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCodProduto.Name = "txtCodProduto"
        Me.txtCodProduto.Size = New System.Drawing.Size(153, 22)
        Me.txtCodProduto.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(68, 130)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(112, 16)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Codigo Empresa:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(29, 37)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(148, 16)
        Me.Label17.TabIndex = 2
        Me.Label17.Text = "Codigo Produto Padão:"
        '
        'tbpImpressao
        '
        Me.tbpImpressao.Controls.Add(Me.GroupBox6)
        Me.tbpImpressao.Controls.Add(Me.GroupBox5)
        Me.tbpImpressao.Location = New System.Drawing.Point(4, 25)
        Me.tbpImpressao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbpImpressao.Name = "tbpImpressao"
        Me.tbpImpressao.Size = New System.Drawing.Size(821, 297)
        Me.tbpImpressao.TabIndex = 3
        Me.tbpImpressao.Text = "Impressão"
        Me.tbpImpressao.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.chkImprimirLogo)
        Me.GroupBox6.Controls.Add(Me.chkImprimirQR)
        Me.GroupBox6.Controls.Add(Me.txtRodape)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.chkImprimirCodBar)
        Me.GroupBox6.Controls.Add(Me.lblLogo)
        Me.GroupBox6.Controls.Add(Me.btnSelecionarLogo)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.cbModeloLayout)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox6.Location = New System.Drawing.Point(421, 18)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox6.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Configuração"
        '
        'chkImprimirLogo
        '
        Me.chkImprimirLogo.AutoSize = True
        Me.chkImprimirLogo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkImprimirLogo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkImprimirLogo.Location = New System.Drawing.Point(221, 108)
        Me.chkImprimirLogo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkImprimirLogo.Name = "chkImprimirLogo"
        Me.chkImprimirLogo.Size = New System.Drawing.Size(113, 20)
        Me.chkImprimirLogo.TabIndex = 16
        Me.chkImprimirLogo.Text = "Imprimir Logo:"
        Me.chkImprimirLogo.UseVisualStyleBackColor = True
        '
        'chkImprimirQR
        '
        Me.chkImprimirQR.AutoSize = True
        Me.chkImprimirQR.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkImprimirQR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkImprimirQR.Location = New System.Drawing.Point(195, 162)
        Me.chkImprimirQR.Margin = New System.Windows.Forms.Padding(4)
        Me.chkImprimirQR.Name = "chkImprimirQR"
        Me.chkImprimirQR.Size = New System.Drawing.Size(138, 20)
        Me.chkImprimirQR.TabIndex = 15
        Me.chkImprimirQR.Text = "Imprimir QR Code:"
        Me.chkImprimirQR.UseVisualStyleBackColor = True
        '
        'txtRodape
        '
        Me.txtRodape.Location = New System.Drawing.Point(36, 202)
        Me.txtRodape.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtRodape.MaxLength = 255
        Me.txtRodape.Name = "txtRodape"
        Me.txtRodape.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRodape.Size = New System.Drawing.Size(307, 22)
        Me.txtRodape.TabIndex = 6
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(33, 183)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(60, 16)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "Rodapé:"
        '
        'chkImprimirCodBar
        '
        Me.chkImprimirCodBar.AutoSize = True
        Me.chkImprimirCodBar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkImprimirCodBar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkImprimirCodBar.Location = New System.Drawing.Point(145, 135)
        Me.chkImprimirCodBar.Margin = New System.Windows.Forms.Padding(4)
        Me.chkImprimirCodBar.Name = "chkImprimirCodBar"
        Me.chkImprimirCodBar.Size = New System.Drawing.Size(188, 20)
        Me.chkImprimirCodBar.TabIndex = 14
        Me.chkImprimirCodBar.Text = "Imprimir Codigo de Barras:"
        Me.chkImprimirCodBar.UseVisualStyleBackColor = True
        '
        'lblLogo
        '
        Me.lblLogo.AutoEllipsis = True
        Me.lblLogo.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblLogo.Location = New System.Drawing.Point(189, 76)
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Size = New System.Drawing.Size(112, 16)
        Me.lblLogo.TabIndex = 13
        Me.lblLogo.Text = "Logo:"
        '
        'btnSelecionarLogo
        '
        Me.btnSelecionarLogo.Location = New System.Drawing.Point(309, 70)
        Me.btnSelecionarLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSelecionarLogo.Name = "btnSelecionarLogo"
        Me.btnSelecionarLogo.Size = New System.Drawing.Size(35, 28)
        Me.btnSelecionarLogo.TabIndex = 12
        Me.btnSelecionarLogo.Text = "..."
        Me.btnSelecionarLogo.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(139, 76)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(41, 16)
        Me.Label25.TabIndex = 10
        Me.Label25.Text = "Logo:"
        '
        'cbModeloLayout
        '
        Me.cbModeloLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbModeloLayout.FormattingEnabled = True
        Me.cbModeloLayout.Items.AddRange(New Object() {"Padrão"})
        Me.cbModeloLayout.Location = New System.Drawing.Point(188, 34)
        Me.cbModeloLayout.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbModeloLayout.Name = "cbModeloLayout"
        Me.cbModeloLayout.Size = New System.Drawing.Size(153, 24)
        Me.cbModeloLayout.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(128, 39)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(50, 16)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Layout:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.cbImpressora)
        Me.GroupBox5.Controls.Add(Me.Label15)
        Me.GroupBox5.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox5.Location = New System.Drawing.Point(23, 18)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Modelo"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(183, 71)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(140, 16)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Requer Driver Gráfico."
        '
        'cbImpressora
        '
        Me.cbImpressora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbImpressora.FormattingEnabled = True
        Me.cbImpressora.Location = New System.Drawing.Point(187, 33)
        Me.cbImpressora.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbImpressora.Name = "cbImpressora"
        Me.cbImpressora.Size = New System.Drawing.Size(153, 24)
        Me.cbImpressora.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(103, 37)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 16)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Impressora:"
        '
        'tbComanda
        '
        Me.tbComanda.Controls.Add(Me.Label23)
        Me.tbComanda.Controls.Add(Me.GroupBox7)
        Me.tbComanda.Location = New System.Drawing.Point(4, 25)
        Me.tbComanda.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbComanda.Name = "tbComanda"
        Me.tbComanda.Size = New System.Drawing.Size(821, 297)
        Me.tbComanda.TabIndex = 4
        Me.tbComanda.Text = "Comanda"
        Me.tbComanda.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label23.Location = New System.Drawing.Point(435, 52)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(349, 16)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "Codigo Mesa: Mesa onde serão gravadas as comandas."
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblZerarComanda)
        Me.GroupBox7.Controls.Add(Me.txtCodigoMesa)
        Me.GroupBox7.Controls.Add(Me.Label27)
        Me.GroupBox7.Controls.Add(Me.cbModoAuto)
        Me.GroupBox7.Controls.Add(Me.Label14)
        Me.GroupBox7.Controls.Add(Me.cbPrefixoComanda)
        Me.GroupBox7.Controls.Add(Me.Label22)
        Me.GroupBox7.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox7.Location = New System.Drawing.Point(23, 18)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox7.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox7.TabIndex = 4
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Codigo Comanda"
        '
        'lblZerarComanda
        '
        Me.lblZerarComanda.AutoSize = True
        Me.lblZerarComanda.LinkColor = System.Drawing.Color.SteelBlue
        Me.lblZerarComanda.Location = New System.Drawing.Point(205, 139)
        Me.lblZerarComanda.Name = "lblZerarComanda"
        Me.lblZerarComanda.Size = New System.Drawing.Size(132, 16)
        Me.lblZerarComanda.TabIndex = 10
        Me.lblZerarComanda.TabStop = True
        Me.lblZerarComanda.Text = "Zerar Cod. Comanda"
        '
        'txtCodigoMesa
        '
        Me.txtCodigoMesa.Location = New System.Drawing.Point(187, 103)
        Me.txtCodigoMesa.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCodigoMesa.MaxLength = 15
        Me.txtCodigoMesa.Name = "txtCodigoMesa"
        Me.txtCodigoMesa.Size = New System.Drawing.Size(153, 22)
        Me.txtCodigoMesa.TabIndex = 9
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(91, 106)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(91, 16)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "Codigo Mesa:"
        '
        'cbModoAuto
        '
        Me.cbModoAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbModoAuto.FormattingEnabled = True
        Me.cbModoAuto.Items.AddRange(New Object() {"Sequencial", "Dia - Hora - Min - Seg"})
        Me.cbModoAuto.Location = New System.Drawing.Point(187, 68)
        Me.cbModoAuto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbModoAuto.Name = "cbModoAuto"
        Me.cbModoAuto.Size = New System.Drawing.Size(153, 24)
        Me.cbModoAuto.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(53, 71)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(124, 16)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Código Automático:"
        '
        'cbPrefixoComanda
        '
        Me.cbPrefixoComanda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrefixoComanda.FormattingEnabled = True
        Me.cbPrefixoComanda.Items.AddRange(New Object() {"Nenhum", "A", "B", "C", "M", "0", "00", "000", "1", "2", "3", "4", "5"})
        Me.cbPrefixoComanda.Location = New System.Drawing.Point(187, 33)
        Me.cbPrefixoComanda.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbPrefixoComanda.Name = "cbPrefixoComanda"
        Me.cbPrefixoComanda.Size = New System.Drawing.Size(153, 24)
        Me.cbPrefixoComanda.TabIndex = 5
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(129, 36)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(51, 16)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "Prefixo:"
        '
        'tbRel
        '
        Me.tbRel.Controls.Add(Me.GroupBox8)
        Me.tbRel.Controls.Add(Me.btnReal)
        Me.tbRel.Location = New System.Drawing.Point(4, 25)
        Me.tbRel.Name = "tbRel"
        Me.tbRel.Padding = New System.Windows.Forms.Padding(3)
        Me.tbRel.Size = New System.Drawing.Size(821, 297)
        Me.tbRel.TabIndex = 5
        Me.tbRel.Text = "Relatorio"
        Me.tbRel.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.lblGerar)
        Me.GroupBox8.Controls.Add(Me.Label30)
        Me.GroupBox8.Controls.Add(Me.dtFim)
        Me.GroupBox8.Controls.Add(Me.Label29)
        Me.GroupBox8.Controls.Add(Me.dtInicio)
        Me.GroupBox8.Controls.Add(Me.Label24)
        Me.GroupBox8.Controls.Add(Me.txtFimKG)
        Me.GroupBox8.Controls.Add(Me.txtInicioKG)
        Me.GroupBox8.Controls.Add(Me.Label28)
        Me.GroupBox8.ForeColor = System.Drawing.Color.SteelBlue
        Me.GroupBox8.Location = New System.Drawing.Point(23, 17)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox8.Size = New System.Drawing.Size(372, 255)
        Me.GroupBox8.TabIndex = 2
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Relatorio"
        '
        'lblGerar
        '
        Me.lblGerar.AutoSize = True
        Me.lblGerar.LinkColor = System.Drawing.Color.SteelBlue
        Me.lblGerar.Location = New System.Drawing.Point(277, 54)
        Me.lblGerar.Name = "lblGerar"
        Me.lblGerar.Size = New System.Drawing.Size(41, 16)
        Me.lblGerar.TabIndex = 78
        Me.lblGerar.TabStop = True
        Me.lblGerar.Text = "Gerar"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(24, 166)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(32, 16)
        Me.Label30.TabIndex = 77
        Me.Label30.Text = "Fim:"
        '
        'dtFim
        '
        Me.dtFim.Location = New System.Drawing.Point(27, 185)
        Me.dtFim.Name = "dtFim"
        Me.dtFim.Size = New System.Drawing.Size(291, 22)
        Me.dtFim.TabIndex = 76
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(24, 112)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(41, 16)
        Me.Label29.TabIndex = 75
        Me.Label29.Text = "Inicio:"
        '
        'dtInicio
        '
        Me.dtInicio.Location = New System.Drawing.Point(27, 131)
        Me.dtInicio.Name = "dtInicio"
        Me.dtInicio.Size = New System.Drawing.Size(291, 22)
        Me.dtInicio.TabIndex = 74
        '
        'Label24
        '
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Location = New System.Drawing.Point(27, 96)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(311, 2)
        Me.Label24.TabIndex = 73
        '
        'txtFimKG
        '
        Me.txtFimKG.Location = New System.Drawing.Point(153, 51)
        Me.txtFimKG.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtFimKG.MaxLength = 50
        Me.txtFimKG.Name = "txtFimKG"
        Me.txtFimKG.Size = New System.Drawing.Size(111, 22)
        Me.txtFimKG.TabIndex = 72
        Me.txtFimKG.Text = "0"
        '
        'txtInicioKG
        '
        Me.txtInicioKG.Location = New System.Drawing.Point(28, 51)
        Me.txtInicioKG.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtInicioKG.MaxLength = 30
        Me.txtInicioKG.Name = "txtInicioKG"
        Me.txtInicioKG.Size = New System.Drawing.Size(110, 22)
        Me.txtInicioKG.TabIndex = 71
        Me.txtInicioKG.Text = "0"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(25, 33)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(80, 16)
        Me.Label28.TabIndex = 2
        Me.Label28.Text = "Intervalo Kg:"
        '
        'btnReal
        '
        Me.btnReal.Location = New System.Drawing.Point(500, 137)
        Me.btnReal.Name = "btnReal"
        Me.btnReal.Size = New System.Drawing.Size(103, 27)
        Me.btnReal.TabIndex = 0
        Me.btnReal.Text = "Gerar"
        Me.btnReal.UseVisualStyleBackColor = True
        Me.btnReal.Visible = False
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(635, 436)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(103, 28)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAplicar
        '
        Me.btnAplicar.Location = New System.Drawing.Point(744, 436)
        Me.btnAplicar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(103, 28)
        Me.btnAplicar.TabIndex = 2
        Me.btnAplicar.Text = "Aplicar"
        Me.btnAplicar.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(525, 436)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(103, 28)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'pictBoxLogoPainel
        '
        Me.pictBoxLogoPainel.Image = CType(resources.GetObject("pictBoxLogoPainel.Image"), System.Drawing.Image)
        Me.pictBoxLogoPainel.Location = New System.Drawing.Point(28, 22)
        Me.pictBoxLogoPainel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pictBoxLogoPainel.Name = "pictBoxLogoPainel"
        Me.pictBoxLogoPainel.Size = New System.Drawing.Size(32, 32)
        Me.pictBoxLogoPainel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictBoxLogoPainel.TabIndex = 55
        Me.pictBoxLogoPainel.TabStop = False
        '
        'lblPainel
        '
        Me.lblPainel.AutoSize = True
        Me.lblPainel.BackColor = System.Drawing.Color.Transparent
        Me.lblPainel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPainel.ForeColor = System.Drawing.Color.White
        Me.lblPainel.Location = New System.Drawing.Point(83, 27)
        Me.lblPainel.Name = "lblPainel"
        Me.lblPainel.Size = New System.Drawing.Size(320, 29)
        Me.lblPainel.TabIndex = 54
        Me.lblPainel.Text = "Parâmetros de Configuração"
        '
        'pictBoxPainel
        '
        Me.pictBoxPainel.BackColor = System.Drawing.Color.SteelBlue
        Me.pictBoxPainel.Location = New System.Drawing.Point(-1, -1)
        Me.pictBoxPainel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pictBoxPainel.Name = "pictBoxPainel"
        Me.pictBoxPainel.Size = New System.Drawing.Size(900, 86)
        Me.pictBoxPainel.TabIndex = 53
        Me.pictBoxPainel.TabStop = False
        '
        'btnTeste
        '
        Me.btnTeste.Location = New System.Drawing.Point(417, 437)
        Me.btnTeste.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnTeste.Name = "btnTeste"
        Me.btnTeste.Size = New System.Drawing.Size(103, 28)
        Me.btnTeste.TabIndex = 56
        Me.btnTeste.Text = "Teste"
        Me.btnTeste.UseVisualStyleBackColor = True
        Me.btnTeste.Visible = False
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 478)
        Me.Controls.Add(Me.btnTeste)
        Me.Controls.Add(Me.pictBoxLogoPainel)
        Me.Controls.Add(Me.lblPainel)
        Me.Controls.Add(Me.pictBoxPainel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnAplicar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.tbcConfig)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.Name = "frmConfig"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parâmetros"
        Me.tbcConfig.ResumeLayout(False)
        Me.tbpConexao.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.tbpConfig.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tbpConfiguracao.ResumeLayout(False)
        Me.tbpConfiguracao.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.tbpImpressao.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.tbComanda.ResumeLayout(False)
        Me.tbComanda.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.tbRel.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.pictBoxLogoPainel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictBoxPainel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbcConfig As TabControl
    Friend WithEvents tbpConfig As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbPortaCOM As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbBitsPorSegundo As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbBitsDeDados As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbControleFluxo As ComboBox
    Friend WithEvents cbBitsDeParada As ComboBox
    Friend WithEvents cbParidade As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cbModeloBalanca As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnAplicar As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents tbpConexao As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtSenhaBanco As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtUsuarioBanco As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtNomeBanco As TextBox
    Friend WithEvents txtEnderecoServidor As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblLinha As Label
    Friend WithEvents tbpConfiguracao As TabPage
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtCodDeposito As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtCodVendedor As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtCodEmpresa As TextBox
    Friend WithEvents txtCodProduto As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents tbpImpressao As TabPage
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents cbImpressora As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents tbComanda As TabPage
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents cbModoAuto As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents cbPrefixoComanda As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents pictBoxLogoPainel As PictureBox
    Friend WithEvents lblPainel As Label
    Friend WithEvents pictBoxPainel As PictureBox
    Friend WithEvents lblTesteConn As LinkLabel
    Friend WithEvents lblConsultar As LinkLabel
    Friend WithEvents btnTeste As Button
    Friend WithEvents Label25 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtRodape As TextBox
    Friend WithEvents cbModeloLayout As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents lblLogo As Label
    Friend WithEvents btnSelecionarLogo As Button
    Friend WithEvents chkImprimirQR As CheckBox
    Friend WithEvents chkImprimirCodBar As CheckBox
    Friend WithEvents txtCodProdutoOpcionais As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents chkImprimirLogo As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents txtCodigoMesa As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lblDescriBalanca As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblZerarComanda As LinkLabel
    Friend WithEvents lblDescriMenuF12 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents tbRel As TabPage
    Friend WithEvents btnReal As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents dtFim As DateTimePicker
    Friend WithEvents Label29 As Label
    Friend WithEvents dtInicio As DateTimePicker
    Friend WithEvents Label24 As Label
    Friend WithEvents txtFimKG As TextBox
    Friend WithEvents txtInicioKG As TextBox
    Friend WithEvents lblGerar As LinkLabel
End Class
