
Imports System.IO
Imports System.Text
Imports System.Reflection

Public Class frmPdvVenda

    Public frmSelecionarP As New frmSelecionarProduto

    Private WithEvents SerialPort As New System.IO.Ports.SerialPort

    Friend WithEvents tmrSolicitaBalanca As New System.Windows.Forms.Timer()

    Private Parametros As New clsFuncoes.CarregarParametros_INI
    Private SQLControl As New clsFuncoes.SQLControl(Parametros)
    Private Impressao As New clsFuncoes.Imprimir(Parametros)
    Private Produto As New clsFuncoes.SQLControl.ProdutoObj
    Private LogApp As New clsFuncoes.Log


    Private imgAtual As String = "imgColoque"
    Private imgColoque As New Bitmap(My.Resources.COLOQUE)
    Private imgPesando As New Bitmap(My.Resources.PESANDO)
    Private imgRetire As New Bitmap(My.Resources.RETIRE)

    Private strDadosPortaSerial As String = String.Empty

    Private strDadosPortaSerial_Peso As String = String.Empty
    Private intContadorLeitura_Peso As Integer = 0

    Private strDadosPortaSerial_Estado As String = String.Empty
    Private intContadorLeitura_Estado_N As Integer = 0

    Private ComandaAtivaCodigo As String = String.Empty

    Private strpicEstado As Boolean

    Public Sub New()
        'LogApp.LogEntry("Inicializando Formulario:", "frmPdvVenda - Public Sub New()")
        InitializeComponent()
    End Sub

    Public Sub ConsultarProduto(ByVal strCodProduto_p As String)

        If Parametros.strSistemaIntegrado = "Sischef" Then
            Try
                frmPrincipal.FormularioPdvVenda.frmSelecionarP.Visible = False
                frmPrincipal.FormularioPdvVenda.txtCodBarra.Visible = True
                frmPrincipal.FormularioPdvVenda.picEstado.Visible = True

                SQLControl.ConsultarProdutoPostgres(strCodProduto_p, Produto, "frmPdvVenda")
                lblNomeProduto.Text = Produto.Codigo + " - " + frmPrincipal.FormularioPdvVenda.Produto.Nome + " (R$ " + CType(frmPrincipal.FormularioPdvVenda.Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")"

            Catch ex As Exception
                LogApp.GerarLogErro(ex, "frmPdvVenda", "ConsultarProduto: " + strCodProduto_p)
            End Try
        End If


        If Parametros.strSistemaIntegrado = "Sismoura" Then
            Try
                frmPrincipal.FormularioPdvVenda.frmSelecionarP.Visible = False
                frmPrincipal.FormularioPdvVenda.txtCodBarra.Visible = True
                frmPrincipal.FormularioPdvVenda.picEstado.Visible = True

                SQLControl.ConsultarProduto(strCodProduto_p, Produto, "frmPdvVenda")
                lblNomeProduto.Text = Produto.Codigo + " - " + frmPrincipal.FormularioPdvVenda.Produto.Nome + " (R$ " + CType(frmPrincipal.FormularioPdvVenda.Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")"

            Catch ex As Exception
                LogApp.GerarLogErro(ex, "frmPdvVenda", "ConsultarProduto: " + strCodProduto_p)
            End Try
        End If

        'Debug.Print("frmPdvVenda : Public Sub ConsultarProduto: " + strCodProduto_p)
    End Sub

    Private Sub Serial_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived

        Try
            Dim DataFromPort As String = SerialPort.ReadExisting
            strDadosPortaSerial = GetAlphaNumericString(DataFromPort)
            'Debug.Print($"strDadosPortaSerial: {strDadosPortaSerial}")
            strDadosPortaSerial = strDadosPortaSerial.Substring(0, 5)
            'Debug.Print($"strDadosPortaSerial_Substring: {strDadosPortaSerial}")

            If Char.IsLetter(strDadosPortaSerial) Then
                strDadosPortaSerial_Estado = GetAlphaString(strDadosPortaSerial)

                If strDadosPortaSerial_Estado = "IIIII" Then
                    'Debug.Print(strDadosPortaSerial)

                    For c = 0 To 1
                        Dim r As New Random()
                        Dim i As Decimal = r.Next(0, 300) / 1000
                        Display(i.ToString("#,###0.000"), "0,00")
                        'intContadorLeitura_Estado_N = 0
                        'intContadorLeitura_Peso = 0
                        wait(500)
                    Next

                    Exit Sub
                End If

                If strDadosPortaSerial_Estado = "NNNNN" Then
                    intContadorLeitura_Estado_N = intContadorLeitura_Estado_N + 1
                    intContadorLeitura_Peso = 0

                    'Debug.Print("strDadosPortaSerial_Estado: " + strDadosPortaSerial_Estado)
                    'Debug.Print("intContadorLeitura_Estado_N: " + CType(intContadorLeitura_Estado_N, String))
                    strDadosPortaSerial_Peso = "0000"
                End If

            End If

            If Char.IsNumber(strDadosPortaSerial) Then
                strDadosPortaSerial_Peso = GetNumeric(strDadosPortaSerial)
                intContadorLeitura_Peso = intContadorLeitura_Peso + 1
                intContadorLeitura_Estado_N = 0

                Debug.Print("strDadosPortaSerial_Peso: " + strDadosPortaSerial_Peso)
                Debug.Print("intContadorLeitura_Peso: " + CType(intContadorLeitura_Peso, String))

                If CType(strDadosPortaSerial_Peso, Decimal) = "0000" Then
                    intContadorLeitura_Peso = 0
                    Display("0,000", "0,00")
                End If

            End If

            strDadosPortaSerial = String.Empty

            'Tratar_Peso_Normal()
            Tratar_Loja1()
            'Tratar_Loja2()
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "Serial_DataReceived: " + strDadosPortaSerial)
        End Try


    End Sub

    Private Sub InicializarPortaSerial(ByVal PortaSerial As String)

        Try
            If SerialPort.IsOpen Then SerialPort.Close()
            SerialPort.PortName = PortaSerial
            SerialPort.BaudRate = CType(Parametros.strBitsPorSegundo, Int32)
            SerialPort.DataBits = CType(Parametros.strBitsDeDados, Int32)
            SerialPort.Parity = Ports.Parity.None
            SerialPort.StopBits = Ports.StopBits.One
            'SerialPort.ReadTimeout = 5000
            SerialPort.DtrEnable = True
            SerialPort.RtsEnable = True
            SerialPort.Encoding = System.Text.Encoding.ASCII
            SerialPort.Handshake = IO.Ports.Handshake.None
            SerialPort.Open()
            tmrSolicitaBalanca.Interval = 1000
            tmrSolicitaBalanca.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message,
                                Application.CompanyName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            frmConfig.ShowDialog()
            frmPrincipal.Close()
            tmrSolicitaBalanca.Stop()

        Finally

        End Try

    End Sub
    Public Sub AbrirPortaSerial()
        SerialPort.Open()
        tmrSolicitaBalanca.Start()
    End Sub
    Public Sub FecharPortaSerial()
        SerialPort.Close()
        tmrSolicitaBalanca.Stop()
    End Sub
    Private Sub tmrSolicitaBalanca_Tick(sender As Object, ByVal e As EventArgs) Handles tmrSolicitaBalanca.Tick
        SerialPort.Write(ChrW(5))
        txtCodBarra.Focus()
    End Sub
    Private Sub tmrSolicitar_Tick(sender As Object, e As EventArgs) Handles tmrSolicitar.Tick
        lblComandaAtiva.Visible = False
        ComandaAtivaCodigo = String.Empty
        tmrSolicitar.Stop()
    End Sub


#Region "FUNCAO GET_ALPHANUMERIC"
    Public Function GetNumeric(value As String) As String
        Dim output As StringBuilder = New StringBuilder
        For i = 0 To value.Length - 1
            If IsNumeric(value(i)) Then
                output.Append(value(i))
            End If
        Next
        Return output.ToString()
    End Function

    Public Function GetAlphaNumericString(ByVal value As String) As String
        Dim mytext As String = value
        Dim Result As String = RegularExpressions.Regex.Replace(mytext, "[^a-zA-Z-0-9]", "")
        Dim myChars() As Char = Result.ToCharArray()
        Return myChars
    End Function

    Public Function GetAlphaString(ByVal value As String) As String
        Dim mytext As String = value
        Dim Result As String = RegularExpressions.Regex.Replace(mytext, "[0-9]", "")
        Dim myChars() As Char = Result.ToCharArray()
        Return myChars
    End Function
#End Region

    Private Sub frmPdvVenda_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            SplitContainer1.Visible = True
            SplitContainer2.Visible = True

            If Parametros.strSistemaIntegrado = "Sischef" Then
                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Load")
                lblNomeProduto.Text = Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")"

            End If


            If Parametros.strSistemaIntegrado = "Sismoura" Then
                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Load")
                lblNomeProduto.Text = Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")"

            End If



            'INICIALIZAR frmSelecionarP
            With frmSelecionarP
                '.Opacity = 0
                '.Visible = False
                .TopLevel = False
                .Parent = SplitContainer1.Panel2
                .Dock = DockStyle.Fill
                ' .Left = (Panel1.Width - .Width) / 2
                ' .Top = (Panel1.Height - .Height) / 2
                '.Show()
                '.Visible = False
            End With

            lblPesoKG.BackColor = Color.Transparent
            lblPesoKG.Parent = picPeso
            lblTotalRS.BackColor = Color.Transparent
            lblTotalRS.Parent = picValorTotal
            lblComandaAtiva.BackColor = Color.Transparent
            lblComandaAtiva.Parent = picEstado

            imprimir_pagina_teste()

        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "frmPdvVenda_Load")
        End Try
    End Sub

    Public Sub imprimir_pagina_teste()


        Impressao.Imprimir(
                          "Pagina de teste",
                          "PRODUTO", "(0.000Kg)", "R$ 0.00)", "1", "1",
                          "SIM",
                          True
                          )

    End Sub

    Private Sub Display(ByVal strPesoKG As String, ByVal strTotalRS As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() Display(strPesoKG, strTotalRS))
            Return
        End If
        lblPesoKG.Text = strPesoKG
        lblTotalRS.Text = strTotalRS
    End Sub

    Private Sub DisplayLabel_Limpar()
        If Me.InvokeRequired Then
            Me.Invoke(Sub() DisplayLabel_Limpar())
            Return
        End If
        lblComandaAtiva.Visible = False
        ComandaAtivaCodigo = String.Empty
        tmrSolicitar.Stop()
    End Sub

    Private Sub DisplayLabelNomeProduto(ByVal strNome As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() DisplayLabelNomeProduto(strNome))
            Return
        End If
        lblNomeProduto.Text = strNome
    End Sub

    Private Sub DebugMSG(ByVal strMSG As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() DebugMSG(strMSG))
            Return
        End If

    End Sub


    'TRATAMENTO DE PESO NORMAL POR KG
    Sub Tratar_Peso_Normal()

        If Parametros.strSistemaIntegrado = "Sischef" Then
            If SQLControl.Testar_ConnPostgres("frmPdvVenda - Tratar_Peso") = False Then
                Display("Banco de dados", "Sem Conexão.")

                SyncLock picEstado
                    picEstado.Image = imgColoque
                End SyncLock
                Exit Sub
            End If
        End If


        If Parametros.strSistemaIntegrado = "Sismoura" Then
            If SQLControl.Testar_Conn("frmPdvVenda - Tratar_Peso") = False Then
                Display("Banco de dados", "Sem Conexão.")

                SyncLock picEstado
                    picEstado.Image = imgColoque
                End SyncLock
                Exit Sub
            End If
        End If


        Try
            If intContadorLeitura_Estado_N > 0 Or CType(strDadosPortaSerial_Peso, Decimal) = "0000" Then

                Try

                    SyncLock picEstado
                        picEstado.Image = imgColoque
                    End SyncLock

                    Display("0,000", "0,00")
                    'Debug.Print("frmpPdvVenda: tratar_intContadorLeitura_Estado_N > 3")
                Catch ex As Exception
                    LogApp.GerarLogErro(ex, "frmPdvVenda", "Tratar_Peso() - intContadorLeitura_Estado_N > 0 - picEstado.Image = imgColoque")
                End Try


            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Estado_N > 0 Or CType(strDadosPortaSerial_Peso, Decimal) = 0000")
        End Try

        Try
            If intContadorLeitura_Peso = 1 And CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

                Try
                    picEstado.Image = imgPesando
                    Display("0,000", "0,00")
                Catch ex As Exception
                    LogApp.GerarLogErro(ex, "frmPdvVenda", "Tratar_Peso() - intContadorLeitura_Peso = 1 - picEstado.Image = imgPesando")
                End Try

            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Peso = 1 And CType(strDadosPortaSerial_Peso, Decimal) > 00000")
        End Try


        If Parametros.strSistemaIntegrado = "Sischef" Then
            Try
                If intContadorLeitura_Peso = 4 And CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

                    If CType(strDadosPortaSerial_Peso, Decimal) > "0000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                        'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                        Dim strCodComandaDHMS As String = String.Empty

                        If Parametros.strPrefixoComanda = "Nenhum" Then
                            Parametros.strPrefixoComanda = ""
                        End If

                        If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                            strCodComandaDHMS = Format(Now, "ddHHmmss")
                        End If

                        If Parametros.strModoAuto = "Sequencial" Then
                            If ComandaAtivaCodigo = String.Empty Then
                                strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                    strCodComandaDHMS = "1"
                                End If
                            End If
                        End If

                        If ComandaAtivaCodigo <> String.Empty Then
                            strCodComandaDHMS = ComandaAtivaCodigo
                        End If
#End Region

                        Dim dValor As Decimal = Produto.Preco_Produto
                        Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                        Dim dResultado As Decimal = dQtd * dValor

                        Display(
                        dQtd.ToString("#,###0.000"),
                        dResultado.ToString("#,##0.00"))

                        Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                        Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")


                        If SQLControl.InserirComandaPostgres(
                                                 Produto.Codigo,
                                                 Parametros.strCodEmpresa,
                                                 Parametros.strCodDeposito,
                                                 Parametros.strCodVendedor,
                                                 dQtd,
                                                 Produto.Preco_Produto,
                                                 dResultado,
                                                 Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                 "1") = True Then
                            ' Parametros.strPrefixoComanda +strCodComandaDHMS


                            SQLControl.InserirComanda_sPostgres(
                                                    Produto.Codigo,
                                                    Parametros.strCodEmpresa,
                                                    Parametros.strCodDeposito,
                                                    Parametros.strCodVendedor,
                                                    dQtd,
                                                    Produto.Preco_Produto,
                                                    dResultado,
                                                    Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                    "1")
                            'Parametros.strPrefixoComanda + strCodComandaDHMS

                            Impressao.Imprimir(
                                           Parametros.strPrefixoComanda + strCodComandaDHMS,
                                           Produto.Nome,
                                           strdQtd_IMP,
                                           strdResultado_IMP,
                                           Parametros.strImpCodBarr,
                                           Parametros.strImpQRCod,
                                           Parametros.strImpLogo,
                                           False
                                          )


                        Else
                            Application.Restart()
                        End If

                        picEstado.Image = imgRetire
                        DisplayLabel_Limpar()

                        If Parametros.strVoltarPadrao = "SIM" Then
                            wait(3000)
                            SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Double).ToString("#,##0.00") + ")")
                        End If

                    End If

                End If
            Catch ex As Exception
                LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Peso = 2 And CType(strDadosPortaSerial_Peso, Decimal) > 00000")
            End Try
        End If


        If Parametros.strSistemaIntegrado = "Sismoura" Then
            Try
                If intContadorLeitura_Peso = 4 And CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

                    If CType(strDadosPortaSerial_Peso, Decimal) > "0000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                        'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                        Dim strCodComandaDHMS As String = String.Empty

                        If Parametros.strPrefixoComanda = "Nenhum" Then
                            Parametros.strPrefixoComanda = ""
                        End If

                        If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                            strCodComandaDHMS = Format(Now, "ddHHmmss")
                        End If

                        If Parametros.strModoAuto = "Sequencial" Then
                            If ComandaAtivaCodigo = String.Empty Then
                                strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                    strCodComandaDHMS = "1"
                                End If
                            End If
                        End If

                        If ComandaAtivaCodigo <> String.Empty Then
                            strCodComandaDHMS = ComandaAtivaCodigo
                        End If
#End Region

                        Dim dValor As Decimal = Produto.Preco_Produto
                        Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                        Dim dResultado As Decimal = dQtd * dValor

                        Display(
                        dQtd.ToString("#,###0.000"),
                        dResultado.ToString("#,##0.00"))

                        Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                        Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")


                        If SQLControl.InserirComanda(
                                                 Produto.Codigo,
                                                 Parametros.strCodEmpresa,
                                                 Parametros.strCodDeposito,
                                                 Parametros.strCodVendedor,
                                                 dQtd,
                                                 Produto.Preco_Produto,
                                                 dResultado,
                                                 Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                 "1") = True Then
                            ' Parametros.strPrefixoComanda +strCodComandaDHMS


                            SQLControl.InserirComanda_s(
                                                    Produto.Codigo,
                                                    Parametros.strCodEmpresa,
                                                    Parametros.strCodDeposito,
                                                    Parametros.strCodVendedor,
                                                    dQtd,
                                                    Produto.Preco_Produto,
                                                    dResultado,
                                                    Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                    "1")
                            'Parametros.strPrefixoComanda + strCodComandaDHMS

                            Impressao.Imprimir(
                                           Parametros.strPrefixoComanda + strCodComandaDHMS,
                                           Produto.Nome,
                                           strdQtd_IMP,
                                           strdResultado_IMP,
                                           Parametros.strImpCodBarr,
                                           Parametros.strImpQRCod,
                                           Parametros.strImpLogo,
                                           False
                                          )


                        Else
                            Application.Restart()
                        End If

                        picEstado.Image = imgRetire
                        DisplayLabel_Limpar()

                        If Parametros.strVoltarPadrao = "SIM" Then
                            wait(3000)
                            SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Double).ToString("#,##0.00") + ")")
                        End If

                    End If

                End If
            Catch ex As Exception
                LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Peso = 2 And CType(strDadosPortaSerial_Peso, Decimal) > 00000")
            End Try
        End If



    End Sub

    'TRATAMENTO DE PESO PERSONALIZADO POR TIPO DE ITEM
    Sub Tratar_Loja1()


        If Parametros.strSistemaIntegrado = "Sischef" Then
            If SQLControl.Testar_ConnPostgres("frmPdvVenda - Tratar_Peso") = False Then
                Display("Banco de dados", "Sem Conexão.")

                SyncLock picEstado
                    picEstado.Image = imgColoque
                End SyncLock
                Exit Sub
            End If
        End If


        If Parametros.strSistemaIntegrado = "Sismoura" Then
            If SQLControl.Testar_Conn("frmPdvVenda - Tratar_Peso") = False Then
                Display("Banco de dados", "Sem Conexão.")

                SyncLock picEstado
                    picEstado.Image = imgColoque
                End SyncLock
                Exit Sub
            End If
        End If


        Try
            If intContadorLeitura_Estado_N > 0 Or CType(strDadosPortaSerial_Peso, Decimal) = "0000" Then

                Try

                    SyncLock picEstado
                        picEstado.Image = imgColoque
                    End SyncLock

                    Display("0,000", "0,00")
                    'Debug.Print("frmpPdvVenda: tratar_intContadorLeitura_Estado_N > 3")
                Catch ex As Exception
                    LogApp.GerarLogErro(ex, "frmPdvVenda", "Tratar_Peso() - intContadorLeitura_Estado_N > 0 - picEstado.Image = imgColoque")
                End Try


            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Estado_N > 0 Or CType(strDadosPortaSerial_Peso, Decimal) = 0000")
        End Try


        Try
            If intContadorLeitura_Peso = 1 And CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

                Try
                    picEstado.Image = imgPesando
                    Display("0,000", "0,00")
                Catch ex As Exception
                    LogApp.GerarLogErro(ex, "frmPdvVenda", "Tratar_Peso() - intContadorLeitura_Peso = 1 - picEstado.Image = imgPesando")
                End Try

            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Peso = 1 And CType(strDadosPortaSerial_Peso, Decimal) > 00000")
        End Try


        Try
            If intContadorLeitura_Peso = 4 And CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

                If lblNomeProduto.Text.Contains("CONVENIO") Then

                    If Parametros.strSistemaIntegrado = "Sischef" Then

                        'KG
                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf4, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                     ) = True Then


                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If

                        End If

                        'UN
                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf5, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If

                        'UN
                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf6, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If

                    End If


                    If Parametros.strSistemaIntegrado = "Sismoura" Then

                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
   CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProduto(Parametros.CodSelf4, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                     ) = True Then


                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If






                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProduto(Parametros.CodSelf5, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProduto(Parametros.CodSelf6, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If

                    End If

                    Exit Sub
                End If


                If Parametros.strSistemaIntegrado = "Sischef" Then

                    If CType(strDadosPortaSerial_Peso, Decimal) > "0000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                        'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                        Dim strCodComandaDHMS As String = String.Empty

                        If Parametros.strPrefixoComanda = "Nenhum" Then
                            Parametros.strPrefixoComanda = ""
                        End If

                        If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                            strCodComandaDHMS = Format(Now, "ddHHmmss")
                        End If

                        If Parametros.strModoAuto = "Sequencial" Then
                            If ComandaAtivaCodigo = String.Empty Then
                                strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                    strCodComandaDHMS = "1"
                                End If
                            End If
                        End If

                        If ComandaAtivaCodigo <> String.Empty Then
                            strCodComandaDHMS = ComandaAtivaCodigo
                        End If
#End Region

                        Dim dValor As Decimal = Produto.Preco_Produto
                        Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                        Dim dResultado As Decimal = dQtd * dValor

                        Display(
                         dQtd.ToString("#,###0.000"),
                         dResultado.ToString("#,##0.00"))

                        Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                        Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")


                        If SQLControl.InserirComandaPostgres(
                                                 Produto.Codigo,
                                                 Parametros.strCodEmpresa,
                                                 Parametros.strCodDeposito,
                                                 Parametros.strCodVendedor,
                                                 dQtd,
                                                 Produto.Preco_Produto,
                                                 dResultado,
                                                 Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                 "1") = True Then
                            ' Parametros.strPrefixoComanda +strCodComandaDHMS


                            SQLControl.InserirComanda_sPostgres(
                                                    Produto.Codigo,
                                                    Parametros.strCodEmpresa,
                                                    Parametros.strCodDeposito,
                                                    Parametros.strCodVendedor,
                                                    dQtd,
                                                    Produto.Preco_Produto,
                                                    dResultado,
                                                    Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                    "1")
                            'Parametros.strPrefixoComanda + strCodComandaDHMS

                            Impressao.Imprimir(
                                           Parametros.strPrefixoComanda + strCodComandaDHMS,
                                           Produto.Nome,
                                           strdQtd_IMP,
                                           strdResultado_IMP,
                                           Parametros.strImpCodBarr,
                                           Parametros.strImpQRCod,
                                           Parametros.strImpLogo,
                                           False
                                          )

                        Else
                            Application.Restart()
                        End If

                        picEstado.Image = imgRetire
                        DisplayLabel_Limpar()

                        If Parametros.strVoltarPadrao = "SIM" Then
                            wait(3000)
                            SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Double).ToString("#,##0.00") + ")")
                        End If

                    End If

                End If


                If Parametros.strSistemaIntegrado = "Sismoura" Then

                    If CType(strDadosPortaSerial_Peso, Decimal) > "0000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                        'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                        Dim strCodComandaDHMS As String = String.Empty

                        If Parametros.strPrefixoComanda = "Nenhum" Then
                            Parametros.strPrefixoComanda = ""
                        End If

                        If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                            strCodComandaDHMS = Format(Now, "ddHHmmss")
                        End If

                        If Parametros.strModoAuto = "Sequencial" Then
                            If ComandaAtivaCodigo = String.Empty Then
                                strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                    strCodComandaDHMS = "1"
                                End If
                            End If
                        End If

                        If ComandaAtivaCodigo <> String.Empty Then
                            strCodComandaDHMS = ComandaAtivaCodigo
                        End If
#End Region

                        Dim dValor As Decimal = Produto.Preco_Produto
                        Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                        Dim dResultado As Decimal = dQtd * dValor

                        Display(
                         dQtd.ToString("#,###0.000"),
                         dResultado.ToString("#,##0.00"))

                        Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                        Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")


                        If SQLControl.InserirComanda(
                                                 Produto.Codigo,
                                                 Parametros.strCodEmpresa,
                                                 Parametros.strCodDeposito,
                                                 Parametros.strCodVendedor,
                                                 dQtd,
                                                 Produto.Preco_Produto,
                                                 dResultado,
                                                 Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                 "1") = True Then
                            ' Parametros.strPrefixoComanda +strCodComandaDHMS


                            SQLControl.InserirComanda_s(
                                                    Produto.Codigo,
                                                    Parametros.strCodEmpresa,
                                                    Parametros.strCodDeposito,
                                                    Parametros.strCodVendedor,
                                                    dQtd,
                                                    Produto.Preco_Produto,
                                                    dResultado,
                                                    Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                    "1")
                            'Parametros.strPrefixoComanda + strCodComandaDHMS

                            Impressao.Imprimir(
                                           Parametros.strPrefixoComanda + strCodComandaDHMS,
                                           Produto.Nome,
                                           strdQtd_IMP,
                                           strdResultado_IMP,
                                           Parametros.strImpCodBarr,
                                           Parametros.strImpQRCod,
                                           Parametros.strImpLogo,
                                           False
                                          )

                        Else
                            Application.Restart()
                        End If

                        picEstado.Image = imgRetire
                        DisplayLabel_Limpar()

                        If Parametros.strVoltarPadrao = "SIM" Then
                            wait(3000)
                            SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Double).ToString("#,##0.00") + ")")
                        End If

                    End If

                End If

            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Peso = 2 And CType(strDadosPortaSerial_Peso, Decimal) > 00000")
        End Try


    End Sub

    Sub Tratar_Loja2()

        If Parametros.strSistemaIntegrado = "Sischef" Then
            If SQLControl.Testar_ConnPostgres("frmPdvVenda - Tratar_Peso") = False Then
                Display("Banco de dados", "Sem Conexão.")

                SyncLock picEstado
                    picEstado.Image = imgColoque
                    imgAtual = "imgColoque"
                End SyncLock
                Exit Sub
            End If
        End If


        If Parametros.strSistemaIntegrado = "Sismoura" Then
            If SQLControl.Testar_Conn("frmPdvVenda - Tratar_Peso") = False Then
                Display("Banco de dados", "Sem Conexão.")

                SyncLock picEstado
                    picEstado.Image = imgColoque
                    imgAtual = "imgColoque"
                End SyncLock
                Exit Sub
            End If
        End If



#Region "TRATAR PESO BALANÇA"
        'IMG COLOQUE O PESO-------------------------------------------------------------
        Try
            If intContadorLeitura_Estado_N > 0 Or CType(strDadosPortaSerial_Peso, Decimal) = "0000" Then

                Try

                    If imgAtual <> "imgColoque" Then
                        SyncLock picEstado
                            picEstado.Image = imgColoque
                            imgAtual = "imgColoque"
                        End SyncLock
                    End If

                    Display("0,000", "0,00")
                    'Debug.Print("frmpPdvVenda: tratar_intContadorLeitura_Estado_N > 3")
                Catch ex As Exception
                    LogApp.GerarLogErro(ex, "frmPdvVenda", "Tratar_Peso() - intContadorLeitura_Estado_N > 0 - picEstado.Image = imgColoque")
                End Try


            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Estado_N > 0 Or CType(strDadosPortaSerial_Peso, Decimal) = 0000")
        End Try

        'IMG PESANDO -------------------------------------------------------------------
        Try
            If intContadorLeitura_Peso = 3 And CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

                Try
                    If imgAtual <> "imgPesando" Then
                        picEstado.Image = imgPesando
                        imgAtual = "imgPesando"
                    End If
                    Display("0,000", "0,00")
                Catch ex As Exception
                    LogApp.GerarLogErro(ex, "frmPdvVenda", "Tratar_Peso() - intContadorLeitura_Peso = 1 - picEstado.Image = imgPesando")
                End Try

            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Peso = 1 And CType(strDadosPortaSerial_Peso, Decimal) > 00000")
        End Try
#End Region


#Region "TRATAR PESO CLIENTES"

        Try
            If intContadorLeitura_Peso = 4 And CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then



                If Parametros.strSistemaIntegrado = "Sischef" Then

                    'F5
                    If lblNomeProduto.Text.Contains("CONVENIO") Then

                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf4, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                     ) = True Then


                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf5, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf6, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If

                        Exit Sub
                    End If

                    'F3
                    If lblNomeProduto.Text.Contains("CHURRASCO") Then
                        If CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodChurrasco1, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                     ) = True Then


                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If

                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If
                        Exit Sub
                    End If

                    'F2
                    If lblNomeProduto.Text.Contains("MARMITEX") Then
                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
                       CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodMarmitex1, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                     ) = True Then


                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                            CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodMarmitex2, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            picEstado.Image = imgRetire
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProdutoPostgres(Parametros.CodMarmitex3, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If
                        Exit Sub
                    End If

                    'F1
                    If lblNomeProduto.Text.Contains("SELF") Then

                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf1, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                     ) = True Then


                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf2, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProdutoPostgres(Parametros.CodSelf3, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If

                        Exit Sub
                    End If

                    'F4
                    If lblNomeProduto.Text.Contains("MARMITX") Then
                        If CType(strDadosPortaSerial_Peso, Decimal) <> "00000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_ConfigPostgres("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region


                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComandaPostgres(
                                                         Produto.Codigo,
                                                         Parametros.strCodEmpresa,
                                                         Parametros.strCodDeposito,
                                                         Parametros.strCodVendedor,
                                                         dQtd,
                                                         Produto.Preco_Produto,
                                                         dResultado,
                                                         Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                         "32"
                                                         ) = True Then


                                SQLControl.InserirComanda_sPostgres(Produto.Codigo,
                                                            Parametros.strCodEmpresa,
                                                            Parametros.strCodDeposito,
                                                            Parametros.strCodVendedor,
                                                            dQtd,
                                                            Produto.Preco_Produto,
                                                            dResultado,
                                                            Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                            "32"
                                                            )

                                Impressao.Imprimir(
                                                   Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                   Produto.Nome,
                                                   strdQtd_IMP,
                                                   strdResultado_IMP,
                                                   Parametros.strImpCodBarr,
                                                   Parametros.strImpQRCod,
                                                   Parametros.strImpLogo, False
                                                   )
                            Else
                                Application.Restart()
                            End If
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProdutoPostgres(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If
                    End If

                End If


                If Parametros.strSistemaIntegrado = "Sismoura" Then

                    'F5
                    If lblNomeProduto.Text.Contains("CONVENIO") Then

                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProduto(Parametros.CodSelf4, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                     ) = True Then


                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProduto(Parametros.CodSelf5, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProduto(Parametros.CodSelf6, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If

                        Exit Sub
                    End If

                    'F3
                    If lblNomeProduto.Text.Contains("CHURRASCO") Then
                        If CType(strDadosPortaSerial_Peso, Decimal) > "00000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProduto(Parametros.CodChurrasco1, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                     ) = True Then


                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If

                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If
                        Exit Sub
                    End If

                    'F2
                    If lblNomeProduto.Text.Contains("MARMITEX") Then
                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
                       CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProduto(Parametros.CodMarmitex1, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                     ) = True Then


                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                            CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProduto(Parametros.CodMarmitex2, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            picEstado.Image = imgRetire
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProduto(Parametros.CodMarmitex3, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "32"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "32"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If
                        Exit Sub
                    End If

                    'F1
                    If lblNomeProduto.Text.Contains("SELF") Then

                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00001" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00538" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region

                            SQLControl.ConsultarProduto(Parametros.CodSelf1, Produto, "frmPdvVenda - Tratar")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd,
                                                     Produto.Preco_Produto,
                                                     dResultado,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                     ) = True Then


                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                               )
                            Else
                                Application.Restart()
                            End If

                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00539" And
                           CType(strDadosPortaSerial_Peso, Decimal) <= "00750" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region

                            SQLControl.ConsultarProduto(Parametros.CodSelf2, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If


                        If CType(strDadosPortaSerial_Peso, Decimal) >= "00751" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If

#End Region
                            'Parametros.strCodProduto2
                            SQLControl.ConsultarProduto(Parametros.CodSelf3, Produto, "frmPdvVenda - Tratar_Peso")
                            DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")

                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dQtd_UN As Decimal = 1
                            Dim dResultado As Decimal = dQtd * dValor
                            Dim dResultado_UN As Decimal = dQtd_UN * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado_UN.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado_UN.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                     Produto.Codigo,
                                                     Parametros.strCodEmpresa,
                                                     Parametros.strCodDeposito,
                                                     Parametros.strCodVendedor,
                                                     dQtd_UN,
                                                     Produto.Preco_Produto,
                                                     dResultado_UN,
                                                     Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                     "1"
                                                    ) = True Then

                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                        Parametros.strCodEmpresa,
                                                        Parametros.strCodDeposito,
                                                        Parametros.strCodVendedor,
                                                        dQtd,
                                                        Produto.Preco_Produto,
                                                        dResultado,
                                                        Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                        "1"
                                                        )

                                Impressao.Imprimir(
                                               Parametros.strPrefixoComanda + strCodComandaDHMS,
                                               Produto.Nome,
                                               strdQtd_IMP,
                                               strdResultado_IMP,
                                               Parametros.strImpCodBarr,
                                               Parametros.strImpQRCod,
                                               Parametros.strImpLogo, False
                                              )

                            Else
                                Application.Restart()
                            End If
                            DisplayLabel_Limpar()
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                            Exit Sub

                        End If

                        Exit Sub
                    End If

                    'F4
                    If lblNomeProduto.Text.Contains("MARMITX") Then
                        If CType(strDadosPortaSerial_Peso, Decimal) <> "00000" Then

#Region "PARAMETROS - CODIGO COMANDA"

                            'Dim strCodComandaDHMS As String = Format(Now, "ddhhmmss")
                            Dim strCodComandaDHMS As String = String.Empty

                            If Parametros.strPrefixoComanda = "Nenhum" Then
                                Parametros.strPrefixoComanda = ""
                            End If

                            If Parametros.strModoAuto = "Dia - Hora - Min - Seg" Then
                                strCodComandaDHMS = Format(Now, "ddHHmmss")
                            End If

                            If Parametros.strModoAuto = "Sequencial" Then
                                If ComandaAtivaCodigo = String.Empty Then
                                    strCodComandaDHMS = SQLControl.GetCodigoUltimaComanda_Config("frmPdvVenda - Tratar_Peso")

                                    If strCodComandaDHMS = "0" Or strCodComandaDHMS = "" Or strCodComandaDHMS = String.Empty Then
                                        strCodComandaDHMS = "1"
                                    End If
                                End If
                            End If

                            If ComandaAtivaCodigo <> String.Empty Then
                                strCodComandaDHMS = ComandaAtivaCodigo
                            End If
#End Region


                            Dim dValor As Decimal = Produto.Preco_Produto
                            Dim dQtd As Decimal = CType(strDadosPortaSerial_Peso, Decimal) / 1000
                            Dim dResultado As Decimal = dQtd * dValor

                            Display(
                            dQtd.ToString("#,###0.000"),
                            dResultado.ToString("#,##0.00"))

                            Dim strdQtd_IMP As String = "(" + dQtd.ToString("#,###0.000") + "Kg)"
                            Dim strdResultado_IMP As String = "R$ " + dResultado.ToString("#,##0.00")

                            If SQLControl.InserirComanda(
                                                         Produto.Codigo,
                                                         Parametros.strCodEmpresa,
                                                         Parametros.strCodDeposito,
                                                         Parametros.strCodVendedor,
                                                         dQtd,
                                                         Produto.Preco_Produto,
                                                         dResultado,
                                                         Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                         "32"
                                                         ) = True Then


                                SQLControl.InserirComanda_s(Produto.Codigo,
                                                            Parametros.strCodEmpresa,
                                                            Parametros.strCodDeposito,
                                                            Parametros.strCodVendedor,
                                                            dQtd,
                                                            Produto.Preco_Produto,
                                                            dResultado,
                                                            Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                            "32"
                                                            )

                                Impressao.Imprimir(
                                                   Parametros.strPrefixoComanda + strCodComandaDHMS,
                                                   Produto.Nome,
                                                   strdQtd_IMP,
                                                   strdResultado_IMP,
                                                   Parametros.strImpCodBarr,
                                                   Parametros.strImpQRCod,
                                                   Parametros.strImpLogo, False
                                                   )
                            Else
                                Application.Restart()
                            End If
                            If imgAtual <> "imgRetire" Then
                                picEstado.Image = imgRetire
                                imgAtual = "imgRetire"
                            End If
                            DisplayLabel_Limpar()
                            If Parametros.strVoltarPadrao = "SIM" Then
                                wait(3000)
                                SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmPdvVenda - Tratar_Peso")
                                DisplayLabelNomeProduto(Produto.Codigo + " - " + Produto.Nome + " (R$ " + CType(Produto.Preco_Produto, Decimal).ToString("#,##0.00") + ")")
                            End If
                        End If
                    End If

                End If



            End If

        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPdvVenda", "If intContadorLeitura_Peso = 2 And CType(strDadosPortaSerial_Peso, Decimal) > 00000")
        End Try

#End Region








    End Sub







    Private Sub picEstado_DoubleClick(sender As Object, e As EventArgs) Handles picEstado.DoubleClick

        If frmPrincipal.FormBorderStyle = FormBorderStyle.None Then
            frmPrincipal.FormBorderStyle = FormBorderStyle.Sizable
            frmPrincipal.WindowState = FormWindowState.Normal
        Else
            frmPrincipal.FormBorderStyle = FormBorderStyle.None
            frmPrincipal.WindowState = FormWindowState.Maximized
        End If

    End Sub

    Private Sub SplitContainer2_GotFocus(sender As Object, e As EventArgs) Handles SplitContainer2.GotFocus
        SplitContainer1.Focus()
    End Sub

    Private Sub txtCodBarra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodBarra.KeyPress

        If Asc(e.KeyChar) = 13 Then



            ComandaAtivaCodigo = txtCodBarra.Text
            lblComandaAtiva.Visible = True
            lblComandaAtiva.Text = "Comanda Ativa: " + ComandaAtivaCodigo
            txtCodBarra.Text = ""

            If lblComandaAtiva.Text = "Comanda Ativa:" Or
                lblComandaAtiva.Text = "Comanda Ativa: " Or
                lblComandaAtiva.Text = "Comanda Ativa:  " Or
                lblComandaAtiva.Text = "Comanda Ativa:   " Then

                lblComandaAtiva.Visible = False
                lblComandaAtiva.Visible = False
                ComandaAtivaCodigo = String.Empty
                Exit Sub
            End If

            tmrSolicitar.Start()
        End If

    End Sub

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved
        lblNomeProduto.Focus()
    End Sub

    Public Sub FecharOpcionais()
        frmSelecionarP.Close()
    End Sub

    Private Sub wait(ByVal millisecondsTimeout As Integer)
        Threading.Thread.Sleep(millisecondsTimeout)
        Application.DoEvents()
    End Sub

    Private Sub lblNomeProduto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNomeProduto.LinkClicked
        Abrir_Fechar_OpMenu()
    End Sub

    'OPCIONAIS MENU F1 - F8 / F11
    Private Sub frmPdvVenda_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode

            Case Keys.F1
                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 1 Then Exit Sub
                Dim i = palavras(0)
                ConsultarProduto(i.ToArray)

            Case Keys.F2
                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 2 Then Exit Sub
                Dim i = palavras(1)
                ConsultarProduto(i.ToArray)

            Case Keys.F3
                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 3 Then Exit Sub
                Dim i = palavras(2)
                ConsultarProduto(i.ToArray)

            Case Keys.F4
                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 4 Then Exit Sub
                Dim i = palavras(3)
                ConsultarProduto(i.ToArray)

            Case Keys.F5
                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 5 Then Exit Sub
                Dim i = palavras(4)
                ConsultarProduto(i.ToArray)

            Case Keys.F6
                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 6 Then Exit Sub
                Dim i = palavras(5)
                ConsultarProduto(i.ToArray)

            Case Keys.F7

                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 7 Then Exit Sub
                Dim i = palavras(6)
                ConsultarProduto(i.ToArray)

            Case Keys.F8

                Dim str As String = Parametros.strstrCodProdutoOpcio
                Dim palavras As String() = str.Split(New Char() {","c})
                If palavras.Length < 8 Then Exit Sub
                Dim i = palavras(7)
                ConsultarProduto(i.ToArray)

            Case Keys.F12
                Abrir_Fechar_OpMenu()
            Case Else

        End Select
    End Sub

    Sub Abrir_Fechar_OpMenu()
        If Parametros.strstrCodProdutoOpcio = String.Empty Then Exit Sub
        If frmSelecionarP.Visible = False Then
            picEstado.Visible = False
            txtCodBarra.Visible = False
            frmSelecionarP.Visible = True
        Else
            picEstado.Visible = True
            txtCodBarra.Visible = True
            frmSelecionarP.Visible = False
        End If
    End Sub


    Private Sub frmPdvVenda_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Me.SplitContainer1.SplitterDistance = Convert.ToInt32(Parametros.strD1)
        Me.SplitContainer2.SplitterDistance = Convert.ToInt32(Parametros.strD2)

        frmPrincipal.ShowFormDegrade()
        Cursor.Position = (New Point(500, 500))


        wait(5000)
        'erroserial
        InicializarPortaSerial(Parametros.strPortaCOM)
    End Sub

    Public Sub SalvarPosicao_Split()

        Parametros.strD1 = SplitContainer1.SplitterDistance
        Parametros.strD2 = SplitContainer2.SplitterDistance
        Parametros.GravaArquivoini()
    End Sub

    Private Sub picEstado_Click(sender As Object, e As EventArgs) Handles picEstado.Click

    End Sub

    Private Sub picEstado_MouseMove(sender As Object, e As MouseEventArgs) Handles picEstado.MouseMove

    End Sub
End Class