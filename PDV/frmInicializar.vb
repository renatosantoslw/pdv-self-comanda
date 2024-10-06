Imports System.IO
Public Class frmInicializar
    Private Parametros As New clsFuncoes.CarregarParametros_INI
    Private SQLControl As New clsFuncoes.SQLControl(Parametros)
    Private Produto As New clsFuncoes.SQLControl.ProdutoObj
    Private Mensagem As New clsFuncoes.SQLControl.MensagemObj
    Private LogApp As New clsFuncoes.Log


    Sub New()

        InitializeComponent()
        'VERIFICA ARQUIVO DE CONFIGURACAO
        If Not File.Exists(NomeArquivo("Config.ini")) Then
            MessageBox.Show("Não foi possível localizar o arquivo de configuração: Config.ini", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Parametros.GravaArquivoini()
        End If

        File.WriteAllBytes(Path.GetTempPath + "rptComanda.rpt", My.Resources.rptComanda)
        File.WriteAllBytes(Path.GetTempPath + "rptRelatorio.rpt", My.Resources.rptRelatorio)

        InitializeComponent()

    End Sub

    'VERIFICA EXISTENCIA DE ARQUIVO
    Private Function NomeArquivo(strNome As String)
        Dim nome_arquivo As String = Application.StartupPath + "\"
        ' nome_arquivo = nome_arquivo.Substring(0, nome_arquivo.LastIndexOf("\"))
        Return nome_arquivo + strNome
    End Function

    'FUNCAO VALIDAR CONFIGURACOES
    Private Function Validar_Configuracoes() As Boolean

        'VERIFICA EXISTENCIA ARQUIVO LOGO
        If Not File.Exists(Parametros.strEnderecoLogo) Then
            Dim img As Image = My.Resources.logo
            img.Save(Application.StartupPath + "\logo.png")
            Parametros.strEnderecoLogo = Application.StartupPath + "\logo.png"
            Parametros.GravaArquivoini()
        End If

        'VERIFICA CONEXAO COM BANCO DE DADOS
        If SQLControl.Testar_Conn("frmInicializar - Validar_Configuracoes") = False Then
            MessageBox.Show("Não foi possível estabelecer uma conexão com o Banco de Dados.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            Exit Function
        End If

        ' //
        'VERIFICA EMPRESA CONFIGURADA
        ' If SQLControl.Verifica_Empresa("NOME EMPRESA") = False Then
        '     MessageBox.Show("Aplicação não licenciada para este usuario.",
        ' Application.CompanyName,
        ' MessageBoxButtons.OK,
        ' MessageBoxIcon.Warning)
        '     Application.Exit()
        ' End If

        'VERIFICA PRODUTO CONFIGURADO
        If SQLControl.ConsultarProduto(Parametros.strCodProduto, Produto, "frmInicializar - Validar_Configuracoes") = False Then
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
                If SQLControl.ConsultarProduto(palavra, Produto, "frmInicializar - Validar_Configuracoes") = False And Parametros.strstrCodProdutoOpcio <> String.Empty Then
                    MessageBox.Show("Ocorreu um erro na verificação do(s) produto(s) configurado(s)." + vbCrLf + "Opcionais: " + palavra + vbCrLf + vbCrLf + "Mensagem: " + Produto.Nome, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                    Exit Function
                End If
            Next
        End If


        Return True
    End Function


    Private Sub frmInicializar_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        If Validar_Configuracoes() = True Then
            frmPrincipal.Show()
            Me.Close()
        Else
            frmConfig.Show()
            Me.Close()
        End If


    End Sub


End Class