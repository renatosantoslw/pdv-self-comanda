Imports System.Drawing.Printing
Imports System.IO

Public Class frmConfig
    Public Shared ReadOnly Property InstalledPrinters As PrinterSettings.StringCollection

    Private Parametros As New clsFuncoes.CarregarParametros_INI
    Private SQLControl As New clsFuncoes.SQLControl(Parametros)
    Private Produto As New clsFuncoes.SQLControl.ProdutoObj
    Private Mensagem As New clsFuncoes.SQLControl.MensagemObj
    Private LogApp As New clsFuncoes.Log

    Public frmPdvVendaAberto As Boolean = False

    'CONSTRUTOR
    Sub New()

        'VERIFICA ARQUIVO DE CONFIGURACAO
        If Not File.Exists(NomeArquivo("Config.ini")) Then
            MessageBox.Show("Não foi possível localizar o arquivo de configuração: Config.ini", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Parametros.GravaArquivoini()
            Application.Restart()
        End If

        File.WriteAllBytes(Path.GetTempPath + "rptComanda.rpt", My.Resources.rptComanda)
        File.WriteAllBytes(Path.GetTempPath + "rptRelatorio.rpt", My.Resources.rptRelatorio)

        InitializeComponent()

    End Sub

#Region "FUNCOES - SUB PROCEDIMENTOS"

    Function SoNumeros(ByVal Keyascii As Short) As Short
        If InStr("1234567890", Chr(Keyascii)) = 0 Then
            SoNumeros = 0
        Else
            SoNumeros = Keyascii
        End If

        Select Case Keyascii
            Case 8
                SoNumeros = Keyascii
            Case 13
                'SoNumeros = Keyascii
            Case 32
                ' SoNumeros = Keyascii
        End Select

    End Function

    Function SoNumerosVirgula(ByVal Keyascii As Short) As Short
        If InStr("1234567890,", Chr(Keyascii)) = 0 Then
            SoNumerosVirgula = 0
        Else
            SoNumerosVirgula = Keyascii
        End If

        Select Case Keyascii
            Case 8
                SoNumerosVirgula = Keyascii
            Case 13
                'SoNumeros = Keyascii
            Case 32
                ' SoNumeros = Keyascii
        End Select

    End Function

    'POPULA COMBO PORTAS SERIAIS
    Sub GetSerialPortNames()

        If My.Computer.Ports.SerialPortNames.Count > 0 Then

            For Each sp As String In My.Computer.Ports.SerialPortNames
                cbPortaCOM.Items.Add(sp)
            Next
        Else



            MessageBox.Show("Não existe nenhuma porta serial instalada.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End If
    End Sub
    'POPULA COMBO IMPRESSORAS INSTALADAS
    Private Sub PopulateInstalledPrintersCombo()
        Dim i As Integer
        Dim pkInstalledPrinters As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings
        cbImpressora.Items.Add("Sem Impressora")

        If PrinterSettings.InstalledPrinters.Count > 0 Then

            For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
                pkInstalledPrinters = PrinterSettings.InstalledPrinters.Item(i)
                cbImpressora.Items.Add(pkInstalledPrinters)
            Next
        Else
            Parametros.strImpressora = "Sem Impressora"
            cbImpressora.Text = "Sem Impressora"
        End If



        'cbImpressora.Text = oPS.PrinterName
    End Sub


    'VERIFICA EXISTENCIA DE ARQUIVO
    Private Function NomeArquivo(strNome As String)
        Dim nome_arquivo As String = Application.StartupPath + "\"
        ' nome_arquivo = nome_arquivo.Substring(0, nome_arquivo.LastIndexOf("\"))
        Return nome_arquivo + strNome
    End Function
    'FUNCAO DE ESPERA
    Private Sub wait(ByVal millisecondsTimeout As Integer)
        Threading.Thread.Sleep(millisecondsTimeout)
        Application.DoEvents()
    End Sub
    'EXIBE FORMULARIO
    Public Sub ShowFormDegrade()
        For i = 0 To 30
            wait(50)
            Me.Opacity = Me.Opacity + 0.1
            If Me.Opacity >= 1.0 Then
                Exit For
            End If
        Next
    End Sub

    Function Verificar_Porta_Serial() As Boolean
        Using SerialPort As New Ports.SerialPort
            Try
                If SerialPort.IsOpen Then SerialPort.Close()
                SerialPort.PortName = Parametros.strPortaCOM
                SerialPort.BaudRate = CType(Parametros.strBitsPorSegundo, Int32)
                SerialPort.DataBits = CType(Parametros.strBitsDeDados, Int32)
                SerialPort.Parity = Ports.Parity.None
                SerialPort.StopBits = Ports.StopBits.One
                SerialPort.DtrEnable = True
                SerialPort.RtsEnable = True
                SerialPort.Encoding = System.Text.Encoding.ASCII
                SerialPort.Handshake = IO.Ports.Handshake.None
                SerialPort.Open()
                Return True
                SerialPort.Open()
                SerialPort.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                    Application.CompanyName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning)
                SerialPort.Close()
                Return False
            Finally

            End Try
        End Using
    End Function

    Function Validar_Configuracoes() As Boolean


        'VERIFICA PORTA SERIAL
        If Verificar_Porta_Serial() = False Then
            MessageBox.Show("Não foi possível inicializar a porta serial:" + Parametros.strPortaCOM, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            Exit Function
        End If

        'VERIFICA CONEXAO COM BANCO DE DADOS
        If SQLControl.Testar_Conn("frmConfig") = False Then
            MessageBox.Show("Não foi possível estabelecer uma conexão com o servidor sql server.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            Exit Function
        End If

        'VERIFICA PRODUTO CONFIGURADO
        If SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmConfig") = False Then
            MessageBox.Show("Ocorreu um erro na verificação do produto configurado." + vbCrLf + vbCrLf + "Mensagem: " + Produto.Nome, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            Exit Function
        End If

        'VERIFICA PRODUTOS CONFIGURADOS(OPCIONAIS)
        If Parametros.strstrCodProdutoOpcio <> String.Empty Or Parametros.strstrCodProdutoOpcio = "" Then
            Dim str As String = Parametros.strstrCodProdutoOpcio
            Dim palavras As String() = str.Split(New Char() {","c})
            Dim palavra As String
            For Each palavra In palavras
                If SQLControl.ConsultarProduto(palavra, Produto, "frmConfig - Validar_Configuracoes") = False And Parametros.strstrCodProdutoOpcio <> String.Empty Then
                    MessageBox.Show("Ocorreu um erro na verificação do(s) produto(s) configurado(s)." + vbCrLf + "Opcionais: " + palavra + vbCrLf + vbCrLf + "Mensagem: " + Produto.Nome, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                    Exit Function
                End If
            Next
        End If

        'VERIFICA EXISTENCIA ARQUIVO LOGO
        If Not File.Exists(Parametros.strEnderecoLogo) Then
            MessageBox.Show("NAO EXISTE")
            Dim img As Image = My.Resources.logo
            img.Save(Application.StartupPath + "\logo.png")
            Parametros.strEnderecoLogo = Application.StartupPath + "\logo.png"
            Parametros.GravaArquivoini()
            Application.Restart()
        End If

        Return True
    End Function

#End Region

    'FORMULARIO EVENTO LOAD
    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        GetSerialPortNames()
        PopulateInstalledPrintersCombo()

        ToolTip1.SetToolTip(txtEnderecoServidor, "Endereço IP ou Hostname do servidor.")
        ToolTip1.SetToolTip(txtNomeBanco, "Nome do banco de dados.")
        ToolTip1.SetToolTip(txtUsuarioBanco, "Usuario do banco. Padrão=sa")
        ToolTip1.SetToolTip(txtSenhaBanco, "Senha do banco de dados.")

        ToolTip1.SetToolTip(txtCodProduto, "Codigo do produto padrão principal.")
        ToolTip1.SetToolTip(txtCodProdutoOpcionais, "Codigo do produtos opcionais separado por virgula.")


        ToolTip1.SetToolTip(lblTesteConn, "Clique para testar a conexão.")

        ToolTip1.SetToolTip(lblDescriBalanca, "Parâmetro: C16(Envio de dados Continuo) - Desabilitado.")

        ToolTip1.SetToolTip(lblDescriMenuF12, "Tecla F12 para abrir o menu.")
        ToolTip1.SetToolTip(Label8, "O Sistema requer o driver gráfico da impressora instalado.")

        ToolTip1.SetToolTip(cbImpressora, "Selecione a impressora de comandas.")

        ToolTip1.SetToolTip(txtCodigoMesa, "Ultilizado para comanda na mesa. Vazio grava 'Null' no banco de dados.")


        ToolTip1.SetToolTip(lblZerarComanda, "Clique para zerar o contador de comandas sequencial.")

        ToolTip1.SetToolTip(cbPrefixoComanda, "Prefixo que vira antes do codigo da comanda.")

        ToolTip1.SetToolTip(txtInicioKG, "Peso inicial Ex: 0.200")

        ToolTip1.SetToolTip(txtFimKG, "Peso final Ex: 1.200")

        ToolTip1.SetToolTip(lblGerar, "Clique para gerar o relatorio")

        Me.Text = "Parâmetros - PDV Self-Comanda - Compilação: 26/08/2022"


        Carregar_Componentes()
    End Sub




    Sub Carregar_Componentes()



        Dim img As New Bitmap(pictBoxPainel.Width, pictBoxPainel.Height)
        Dim brush As New Drawing.Drawing2D.LinearGradientBrush(New PointF(0, 0), New PointF(img.Width, img.Height), Color.SteelBlue, SystemColors.Control)
        Dim gr As Graphics = Graphics.FromImage(img)
        gr.FillRectangle(brush, New RectangleF(0, 0, img.Width, img.Height))
        pictBoxPainel.BackgroundImage = img


        lblPainel.Parent = pictBoxPainel
        lblPainel.BackColor = Color.Transparent
        lblPainel.BringToFront()

        pictBoxLogoPainel.Parent = pictBoxPainel
        pictBoxLogoPainel.BackColor = Color.Transparent
        pictBoxLogoPainel.BringToFront()

        Passar_Parametros_Objetos()


    End Sub


    Private Sub PopularComboBoxLayout()
        Dim DirDiretorio As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
        Dim oFileInfoCollection() As FileInfo
        Dim oFileInfo As FileInfo
        Dim i As Integer
        oFileInfoCollection = DirDiretorio.GetFiles("*.rpt")

        For i = 0 To oFileInfoCollection.Length() - 1
            oFileInfo = oFileInfoCollection.GetValue(i)
            cbModeloLayout.Items.Add(oFileInfo)
        Next
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Public Sub Passar_Parametros_Objetos()


        'CONEXAO
        txtEnderecoServidor.Text = Parametros.strEnderecoServidor
        txtNomeBanco.Text = Parametros.strNomeBanco
        txtUsuarioBanco.Text = Parametros.strUsuarioBanco
        txtSenhaBanco.Text = Parametros.strSenhaBanco

        'BALANCA
        cbModeloBalanca.Text = Parametros.strModeloBalanca


        'PORTA SERIAL
        cbPortaCOM.Text = Parametros.strPortaCOM
        cbBitsPorSegundo.Text = Parametros.strBitsPorSegundo
        cbBitsDeDados.Text = Parametros.strBitsDeDados
        cbParidade.Text = Parametros.strParidade
        cbBitsDeParada.Text = Parametros.strBitsDeParada
        cbControleFluxo.Text = Parametros.strControleFluxo

        'PRODUTO
        txtCodProduto.Text = Parametros.strCodProduto
        txtCodEmpresa.Text = Parametros.strCodEmpresa
        txtCodVendedor.Text = Parametros.strCodVendedor
        txtCodDeposito.Text = Parametros.strCodDeposito
        txtCodProdutoOpcionais.Text = Parametros.strstrCodProdutoOpcio


        'IMPRESSORA
        cbImpressora.Text = Parametros.strImpressora
        cbModeloLayout.Text = Parametros.strModeloLayout

        If Parametros.strImpCodBarr = "SIM" Then
            chkImprimirCodBar.Checked = True
        End If
        If Parametros.strImpCodBarr = "NÃO" Then
            chkImprimirCodBar.Checked = False
        End If


        If Parametros.strImpLogo = "SIM" Then
            chkImprimirLogo.Checked = True
        End If
        If Parametros.strImpLogo = "NÃO" Then
            chkImprimirLogo.Checked = False
        End If

        If Parametros.strImpQRCod = "SIM" Then
            chkImprimirQR.Checked = True
        End If

        If Parametros.strImpQRCod = "NÃO" Then
            chkImprimirQR.Checked = False
        End If

        txtRodape.Text = Parametros.strRodape
        lblLogo.Text = Parametros.strEnderecoLogo

        'COMANDA
        If Parametros.strPrefixoComanda = "" Or Parametros.strPrefixoComanda = String.Empty Then
            Parametros.strPrefixoComanda = "Nenhum"
        End If

        cbPrefixoComanda.Text = Parametros.strPrefixoComanda
        cbModoAuto.Text = Parametros.strModoAuto
        txtCodigoMesa.Text = Parametros.strCodMesa
    End Sub

    Public Sub Gravar_Parametros()

        'CONEXAO
        Parametros.strEnderecoServidor = txtEnderecoServidor.Text
        Parametros.strNomeBanco = txtNomeBanco.Text
        Parametros.strUsuarioBanco = txtUsuarioBanco.Text
        Parametros.strSenhaBanco = txtSenhaBanco.Text

        'BALANCA
        Parametros.strModeloBalanca = cbModeloBalanca.Text

        'PORTA SERIAL
        Parametros.strPortaCOM = cbPortaCOM.Text
        Parametros.strBitsPorSegundo = cbBitsPorSegundo.Text
        Parametros.strBitsDeDados = cbBitsDeDados.Text
        Parametros.strParidade = cbParidade.Text
        Parametros.strBitsDeParada = cbBitsDeParada.Text
        Parametros.strControleFluxo = cbControleFluxo.Text

        'PRODUTO
        Parametros.strCodProduto = txtCodProduto.Text
        Parametros.strCodEmpresa = txtCodEmpresa.Text
        Parametros.strCodVendedor = txtCodVendedor.Text
        Parametros.strCodDeposito = txtCodDeposito.Text
        Parametros.strstrCodProdutoOpcio = txtCodProdutoOpcionais.Text

        'IMPRESSORA
        Parametros.strImpressora = cbImpressora.Text
        Parametros.strModeloLayout = cbModeloLayout.Text

        If chkImprimirQR.Checked = False Then
            Parametros.strImpQRCod = "NÃO"
        End If

        If chkImprimirQR.Checked = True Then
            Parametros.strImpQRCod = "SIM"
        End If

        If chkImprimirCodBar.Checked = True Then
            Parametros.strImpCodBarr = "SIM"
        End If

        If chkImprimirCodBar.Checked = False Then
            Parametros.strImpCodBarr = "NÃO"
        End If

        If chkImprimirLogo.Checked = True Then
            Parametros.strImpLogo = "SIM"
        End If

        If chkImprimirLogo.Checked = False Then
            Parametros.strImpLogo = "NÃO"
        End If


        Parametros.strRodape = txtRodape.Text
        Parametros.strEnderecoLogo = lblLogo.Text

        'COMANDA

        Parametros.strPrefixoComanda = cbPrefixoComanda.Text

        Parametros.strModoAuto = cbModoAuto.Text
        Parametros.strCodMesa = txtCodigoMesa.Text

        Parametros.GravaArquivoini()
    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        Gravar_Parametros()
        MessageBox.Show("Configurações gravadas com sucesso.",
    Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnAplicar.Enabled = False
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Application.Restart()
    End Sub

    Private Sub lblTesteConn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTesteConn.LinkClicked


        If SQLControl.Testar_Conn_t(txtEnderecoServidor.Text,
                                      txtNomeBanco.Text,
                                      txtUsuarioBanco.Text,
                                      txtSenhaBanco.Text, Mensagem, "lblTesteConn_LinkClicked") = True Then

            MessageBox.Show("Conexão com o banco de dados estabelecida com sucesso.",
                Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Não foi possivel estabelecer uma conexão com banco de dados." + vbCr + vbCr + Mensagem.Mensagem,
                Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub lblConsultar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblConsultar.LinkClicked


        If SQLControl.ConsultarProduto(txtCodProduto.Text, Produto, "lblConsultar_LinkClicked") = True Then
            MessageBox.Show("Codigo: " + Produto.Codigo + vbCrLf +
                            "Codigo Barra: " + Produto.Codigo_Barra + vbCrLf +
                            "Nome: " + Produto.Nome + vbCrLf +
                            "Preço: R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00"),
            Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Mensagem: " + vbCrLf + vbCrLf + Produto.Nome + vbCrLf,
    Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

        If txtCodProdutoOpcionais.Text = "" Then Exit Sub
        'VERIFICA PRODUTOS CONFIGURADOS
        Dim str As String = txtCodProdutoOpcionais.Text
        Dim palavras As String() = str.Split(New Char() {","c})
        Dim palavra As String

        For Each palavra In palavras
            If SQLControl.ConsultarProduto(palavra, Produto, "lblConsultar_LinkClicked") = False And Parametros.strstrCodProdutoOpcio <> String.Empty Then
                MessageBox.Show("Ocorreu um erro na verificação do(s) produto(s) configurado(s)." + vbCrLf + "Opcionais: " + palavra + vbCrLf + vbCrLf + "Mensagem: " + Produto.Nome, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Next

        If palavras.Length > 9 Then
            MessageBox.Show("Alerta: Número de opcionais superior a 9 Codigos.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

#Region "EVENTO CHANGED OBJETOS"
    Private Sub txtEnderecoServidor_TextChanged(sender As Object, e As EventArgs) Handles txtEnderecoServidor.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtNomeBanco_TextChanged(sender As Object, e As EventArgs) Handles txtNomeBanco.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtUsuarioBanco_TextChanged(sender As Object, e As EventArgs) Handles txtUsuarioBanco.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtSenhaBanco_TextChanged(sender As Object, e As EventArgs) Handles txtSenhaBanco.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbModeloBalanca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbModeloBalanca.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbModoFuncionamento_SelectedIndexChanged(sender As Object, e As EventArgs)
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbPortaCOM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPortaCOM.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbBitsPorSegundo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbBitsPorSegundo.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbBitsDeDados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbBitsDeDados.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbParidade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbParidade.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbBitsDeParada_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbBitsDeParada.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbControleFluxo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbControleFluxo.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtCodProduto_TextChanged(sender As Object, e As EventArgs) Handles txtCodProduto.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtCodEmpresa_TextChanged(sender As Object, e As EventArgs) Handles txtCodEmpresa.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtCodVendedor_TextChanged(sender As Object, e As EventArgs) Handles txtCodVendedor.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtCodDeposito_TextChanged(sender As Object, e As EventArgs) Handles txtCodDeposito.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbImpressora_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbImpressora.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbModeloLayout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbModeloLayout.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbTamanhoCodigo_SelectedIndexChanged(sender As Object, e As EventArgs)
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbTipoCodigo_SelectedIndexChanged(sender As Object, e As EventArgs)
        btnAplicar.Enabled = True
    End Sub

    Private Sub txtRodape_TextChanged(sender As Object, e As EventArgs) Handles txtRodape.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbModoGravarComanda_SelectedIndexChanged(sender As Object, e As EventArgs)
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbPrefixoComanda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPrefixoComanda.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub cbModoAuto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbModoAuto.SelectedIndexChanged
        btnAplicar.Enabled = True
    End Sub
#End Region



    Private Sub btnSelecionarLogo_Click(sender As Object, e As EventArgs) Handles btnSelecionarLogo.Click


        Using OpDialogoLogo As New OpenFileDialog

            OpDialogoLogo.RestoreDirectory = True
            OpDialogoLogo.ReadOnlyChecked = True
            OpDialogoLogo.ShowReadOnly = True

            OpDialogoLogo.DefaultExt = "png"
            OpDialogoLogo.FilterIndex = 3
            OpDialogoLogo.CheckFileExists = True
            OpDialogoLogo.CheckPathExists = True

            OpDialogoLogo.Title = "Localizar arquivo."
            OpDialogoLogo.Filter = "PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg;*.jpe)|*.jpg;*.jpeg;*.jpe|Todos os arquivos de imagem|*.jpg;*.jpeg;*.jpe;*.png;*.bmp"
            OpDialogoLogo.InitialDirectory = Application.StartupPath
            Dim dr As DialogResult = OpDialogoLogo.ShowDialog
            If dr = System.Windows.Forms.DialogResult.OK Then
                lblLogo.Text = OpDialogoLogo.FileName
            End If

        End Using

    End Sub

    Private Sub tbcConfig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbcConfig.SelectedIndexChanged
        If tbcConfig.SelectedTab Is tbpConfiguracao Then
            txtCodProduto.Select()
        End If
    End Sub

    Private Sub txtCodigoMesa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodigoMesa.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(SoNumeros(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCodProdutoOpcionais_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodProdutoOpcionais.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(SoNumerosVirgula(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCodProduto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodProduto.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(SoNumeros(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCodEmpresa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodEmpresa.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(SoNumeros(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCodVendedor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodVendedor.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(SoNumeros(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCodDeposito_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodDeposito.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(SoNumeros(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCodigoMesa_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoMesa.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub chkImprimirLogo_CheckedChanged(sender As Object, e As EventArgs) Handles chkImprimirLogo.CheckedChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub chkImprimirCodBar_CheckedChanged(sender As Object, e As EventArgs) Handles chkImprimirCodBar.CheckedChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub chkImprimirQR_CheckedChanged(sender As Object, e As EventArgs) Handles chkImprimirQR.CheckedChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub lblLogo_TextChanged(sender As Object, e As EventArgs) Handles lblLogo.TextChanged
        btnAplicar.Enabled = True
    End Sub

    Private Sub lblZerarComanda_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblZerarComanda.LinkClicked
        SQLControl.UpdateGetCodigoUltimaComanda_Zerar()

    End Sub

    Private Sub btnReal_Click(sender As Object, e As EventArgs) Handles btnReal.Click
        Dim Ini As String = Format(dtInicio.Value, "yyyy-MM-dd") + " 00:00:00.000"
        Dim Fim As String = Format(dtFim.Value, "yyyy-MM-dd") + " 23:59:00.000"

        Dim SQLQ = "SELECT v.Produto,
        p.Nome,sum(v.Quantidade) as KG,
	    sum(v.Total) as Valor,
	    count(*) as Quantidade 
	    FROM Venda_Espera_SelfComanda v 
	    inner join Produto p on v.Produto=p.Codigo 
	    where v.Quantidade >= " + txtInicioKG.Text + " and v.Quantidade <= " + txtFimKG.Text + "  and 
	    data BETWEEN '" + Ini + "' AND '" + Fim + "'  group by v.Produto,p.Nome"

        frmRelatorio.sSQLQuery = SQLQ
        frmRelatorio.StringConexao = SQLControl.strConexao
        frmRelatorio.Show()
    End Sub

    Private Sub tbRel_Click(sender As Object, e As EventArgs) Handles tbRel.Click

    End Sub

    Private Sub lblGerar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblGerar.LinkClicked

        If txtFimKG.Text = "" Or txtInicioKG.Text = "" Then
            txtInicioKG.Focus()
            Return
        End If

        Dim kgInicial As String = txtInicioKG.Text
        kgInicial = kgInicial.Replace(",", ".")

        Dim kgFinal As String = txtFimKG.Text
        kgFinal = kgFinal.Replace(",", ".")


        Dim Ini As String = Format(dtInicio.Value, "yyyy-MM-dd") + " 00:00:00.000"
        Dim Fim As String = Format(dtFim.Value, "yyyy-MM-dd") + " 23:59:00.000"




        Dim SQLQ = "SELECT v.Produto,
        p.Nome,sum(v.Quantidade) as KG,
	    sum(v.Total) as Valor,
	    count(*) as Quantidade 
	    FROM Venda_Espera_SelfComanda v 
	    inner join Produto p on v.Produto=p.Codigo 
	    where v.Quantidade >= " + kgInicial + " and v.Quantidade <= " + kgFinal + "  and 
	    data BETWEEN '" + Ini + "' AND '" + Fim + "'  group by v.Produto,p.Nome"

        frmRelatorio.crtDatas = "Datas: " + Format(dtInicio.Value, "dd/MM/yyyy") + " - " + Format(dtFim.Value, "dd/MM/yyyy")
        frmRelatorio.crtPesos = "Pesos: " + kgInicial + "Kg - " + kgFinal + "Kg"

        frmRelatorio.sSQLQuery = SQLQ
        frmRelatorio.StringConexao = SQLControl.strConexao
        frmRelatorio.Show()
    End Sub

    Private Sub txtInicioKG_TextChanged(sender As Object, e As EventArgs) Handles txtInicioKG.TextChanged

    End Sub

    Private Sub txtInicioKG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInicioKG.KeyPress
        Dim strValid As String
        strValid = "0123456789.," & ControlChars.Back


        ' If e.KeyChar = Convert.ToChar(Keys.Back) Then
        'SendKeys.Send(Chr(8))
        ' End If

        If InStr(strValid, e.KeyChar) = 0 Then
            e.Handled = True
        End If '
    End Sub

    Private Sub txtInicioKG_Click(sender As Object, e As EventArgs) Handles txtInicioKG.Click
        txtInicioKG.SelectAll()
    End Sub

    Private Sub txtFimKG_TextChanged(sender As Object, e As EventArgs) Handles txtFimKG.TextChanged

    End Sub

    Private Sub txtFimKG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFimKG.KeyPress
        Dim strValid As String
        strValid = "0123456789.," & ControlChars.Back


        ' If e.KeyChar = Convert.ToChar(Keys.Back) Then
        'SendKeys.Send(Chr(8))
        ' End If

        If InStr(strValid, e.KeyChar) = 0 Then
            e.Handled = True
        End If '
    End Sub

    Private Sub txtFimKG_Click(sender As Object, e As EventArgs) Handles txtFimKG.Click
        txtFimKG.SelectAll()
    End Sub
End Class