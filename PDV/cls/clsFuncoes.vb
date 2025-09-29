Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System
Imports System.Data
Imports Npgsql

Public Class clsFuncoes
    Private Declare Auto Function GetPrivateProfileString Lib "Kernel32" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileString Lib "Kernel32" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer


    Public Class Log
        Public Sub LogEntry(texto As [String], evento As [String])
            Dim mensagem As [String] = [String].Format("{0} - {1} {2}", DateTime.Now, texto, evento)
            File.AppendAllLines(Environment.CurrentDirectory + "\Log_Operacaoes.txt", New [String]() {mensagem})
        End Sub

        Public Sub GerarLogErro(ByRef e As Exception, Formulario As String, FuncaoProcedimento As String)
            Try
                Dim sw As New StreamWriter(Environment.CurrentDirectory + "\Log_Erro.txt", True)
                With sw
                    .WriteLine("Data: " & DateTime.Now.ToShortDateString())
                    .WriteLine("Hora: " & DateTime.Now.ToShortTimeString())
                    .WriteLine("Descrição do erro: " & e.Message)
                    .WriteLine("TargetSite: " & e.TargetSite.ToString())
                    .WriteLine("StackTrace: " & e.StackTrace)
                    .WriteLine("Formulário: " & Formulario)
                    .WriteLine("Funcao/Procedimento: " & FuncaoProcedimento)
                    .WriteLine("Computador: " & My.Computer.Name)
                    .WriteLine("Usuário: " & My.User.Name)
                    .WriteLine("--------------------------------------")
                    .Flush()
                    .Dispose()
                End With
            Catch
            End Try
        End Sub
    End Class

    Public Class CarregarParametros_INI : Implements IDisposable

        Dim nome_arquivo_ini As String = nomeArquivoINI()

        Public strSistemaIntegrado = String.Empty

        'PORTA SERIAL
        Public strPortaCOM = String.Empty
        Public strBitsPorSegundo = String.Empty
        Public strBitsDeDados = String.Empty
        Public strParidade = String.Empty
        Public strBitsDeParada = String.Empty
        Public strControleFluxo = String.Empty

        'MODELO
        Public strModeloBalanca = String.Empty

        'CONEXAO BANCO
        Public strEnderecoServidor = String.Empty
        Public strNomeBanco = String.Empty
        Public strUsuarioBanco = String.Empty
        Public strSenhaBanco = String.Empty

        'PRODUTO
        Public strCodProduto = String.Empty
        Public strCodProduto2 = String.Empty

        Public strstrCodProdutoOpcio = String.Empty
        Public strVoltarPadrao = String.Empty

        Public strCodEmpresa = String.Empty
        Public strCodDeposito = String.Empty
        Public strCodVendedor = String.Empty

        Public CodChurrasco1 = String.Empty

        Public CodMarmitex1 = String.Empty
        Public CodMarmitex2 = String.Empty
        Public CodMarmitex3 = String.Empty

        Public CodSelf1 = String.Empty
        Public CodSelf2 = String.Empty
        Public CodSelf3 = String.Empty

        Public CodSelf4 = String.Empty
        Public CodSelf5 = String.Empty
        Public CodSelf6 = String.Empty

        'IMPRESSORA
        Public strRpt = String.Empty
        Public strImpressora = String.Empty
        Public strModeloLayout = String.Empty
        Public strRodape = String.Empty
        Public strEnderecoLogo = String.Empty
        Public strImpCodBarr = String.Empty
        Public strImpQRCod = String.Empty
        Public strImpLogo = String.Empty

        'COMANDA
        Public strModoGravarComanda = String.Empty
        Public strPrefixoComanda = String.Empty
        Public strModoAuto = String.Empty
        Public strCodMesa = String.Empty

        'LAYOUT
        Public strD1 = String.Empty
        Public strD2 = String.Empty

        Private Function nomeArquivoINI() As String
            Dim nome_arquivo_ini As String = Application.StartupPath
            'nome_arquivo_ini = nome_arquivo_ini.Substring(0, nome_arquivo_ini.LastIndexOf("\"))
            Return nome_arquivo_ini & "\Config.ini"
        End Function

        Private Function LeArquivoINI(ByVal file_name As String, ByVal section_name As String, ByVal key_name As String, ByVal default_value As String) As String
            Const MAX_LENGTH As Integer = 500
            Dim string_builder As New StringBuilder(MAX_LENGTH)
            GetPrivateProfileString(section_name, key_name, default_value, string_builder, MAX_LENGTH, file_name)
            Return string_builder.ToString()
        End Function

        Public Sub GravaArquivoini()


            'CONEXAO
            WritePrivateProfileString("Conexao", "Servidor", strEnderecoServidor, nome_arquivo_ini)
            WritePrivateProfileString("Conexao", "NomeBancoDados", strNomeBanco, nome_arquivo_ini)
            WritePrivateProfileString("Conexao", "UsuarioBanco", strUsuarioBanco, nome_arquivo_ini)
            WritePrivateProfileString("Conexao", "SenhaBanco", strSenhaBanco, nome_arquivo_ini)

            'BALANCA
            WritePrivateProfileString("Balanca", "Modelo", strModeloBalanca, nome_arquivo_ini)

            'PORTA SERIAL
            WritePrivateProfileString("PortaSerial", "NumeroPortaCOM", strPortaCOM, nome_arquivo_ini)
            WritePrivateProfileString("PortaSerial", "BitsSegundo", strBitsPorSegundo, nome_arquivo_ini)
            WritePrivateProfileString("PortaSerial", "BitsDados", strBitsDeDados, nome_arquivo_ini)
            WritePrivateProfileString("PortaSerial", "Paridade", strParidade, nome_arquivo_ini)
            WritePrivateProfileString("PortaSerial", "BitsParada", strBitsDeParada, nome_arquivo_ini)
            WritePrivateProfileString("PortaSerial", "ControleFluxo", strControleFluxo, nome_arquivo_ini)

            'PRODUTO
            WritePrivateProfileString("Produto", "Codigo", strCodProduto, nome_arquivo_ini)
            WritePrivateProfileString("Produto", "CodigoEmpresa", strCodEmpresa, nome_arquivo_ini)
            WritePrivateProfileString("Produto", "CodigoVendedor", strCodVendedor, nome_arquivo_ini)
            WritePrivateProfileString("Produto", "CodigoDeposito", strCodDeposito, nome_arquivo_ini)
            WritePrivateProfileString("Produto", "CodigoOP", strstrCodProdutoOpcio, nome_arquivo_ini)
            WritePrivateProfileString("Produto", "VoltarPardao", strVoltarPadrao, nome_arquivo_ini)

            'IMPRESSORA
            WritePrivateProfileString("Impressao", "Impressora", strImpressora, nome_arquivo_ini)
            WritePrivateProfileString("Impressao", "Layout", strModeloLayout, nome_arquivo_ini)

            WritePrivateProfileString("Impressao", "Barra", strImpCodBarr, nome_arquivo_ini)
            WritePrivateProfileString("Impressao", "QR", strImpQRCod, nome_arquivo_ini)
            WritePrivateProfileString("Impressao", "Logoimp", strImpLogo, nome_arquivo_ini)

            WritePrivateProfileString("Impressao", "Rodape", strRodape, nome_arquivo_ini)
            WritePrivateProfileString("Impressao", "Logo", strEnderecoLogo, nome_arquivo_ini)

            'COMANDA
            WritePrivateProfileString("Comanda", "ModoFuncionamento", strModoGravarComanda, nome_arquivo_ini)
            WritePrivateProfileString("Comanda", "Prefixo", strPrefixoComanda, nome_arquivo_ini)
            WritePrivateProfileString("Comanda", "ModoAuto", strModoAuto, nome_arquivo_ini)
            WritePrivateProfileString("Comanda", "Mesa", strCodMesa, nome_arquivo_ini)

            'LAYOUT
            WritePrivateProfileString("Layout", "d1", strD1, nome_arquivo_ini)
            WritePrivateProfileString("Layout", "d2", strD2, nome_arquivo_ini)

            Debug.Print("Public Class CarregarParametros_INI : Public Sub GravaArquivoini()")
        End Sub

        Public Sub LerArquivoini()


            strSistemaIntegrado = LeArquivoINI(nome_arquivo_ini, "Sistema", "Integracao", "Sismoura")

            'CONEXAO
            strEnderecoServidor = LeArquivoINI(nome_arquivo_ini, "Conexao", "Servidor", ".")
            strNomeBanco = LeArquivoINI(nome_arquivo_ini, "Conexao", "NomeBancoDados", "SISMOURA")
            strUsuarioBanco = LeArquivoINI(nome_arquivo_ini, "Conexao", "UsuarioBanco", "sa")
            strSenhaBanco = LeArquivoINI(nome_arquivo_ini, "Conexao", "SenhaBanco", "epilef")

            'BALANCA
            strModeloBalanca = LeArquivoINI(nome_arquivo_ini, "Balanca", "Modelo", "Toledo")

            'PORTA SERIAL
            strPortaCOM = LeArquivoINI(nome_arquivo_ini, "PortaSerial", "NumeroPortaCOM", "COM1")
            strBitsPorSegundo = LeArquivoINI(nome_arquivo_ini, "PortaSerial", "BitsSegundo", "2400")
            strBitsDeDados = LeArquivoINI(nome_arquivo_ini, "PortaSerial", "BitsDados", "8")
            strParidade = LeArquivoINI(nome_arquivo_ini, "PortaSerial", "Paridade", "Nenhum")
            strBitsDeParada = LeArquivoINI(nome_arquivo_ini, "PortaSerial", "BitsParada", "1")
            strControleFluxo = LeArquivoINI(nome_arquivo_ini, "PortaSerial", "ControleFluxo", "Nenhum")

            'PRODUTO
            strCodProduto = LeArquivoINI(nome_arquivo_ini, "Produto", "Codigo", "1")
            strstrCodProdutoOpcio = LeArquivoINI(nome_arquivo_ini, "Produto", "CodigoOP", "1")
            strCodProduto2 = LeArquivoINI(nome_arquivo_ini, "Produto", "Codigo2", "1")

            strVoltarPadrao = LeArquivoINI(nome_arquivo_ini, "Produto", "VoltarPardao", "SIM")

            strCodEmpresa = LeArquivoINI(nome_arquivo_ini, "Produto", "CodigoEmpresa", "1")
            strCodVendedor = LeArquivoINI(nome_arquivo_ini, "Produto", "CodigoVendedor", "1")
            strCodDeposito = LeArquivoINI(nome_arquivo_ini, "Produto", "CodigoDeposito", "1")

            CodChurrasco1 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodChurrasco1", "1")

            CodMarmitex1 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodMarmitex1", "1")
            CodMarmitex2 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodMarmitex2", "2")
            CodMarmitex3 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodMarmitex3", "3")

            CodSelf1 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodSelf1", "4")
            CodSelf2 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodSelf2", "5")
            CodSelf3 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodSelf3", "6")
            CodSelf4 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodSelf4", "7")
            CodSelf5 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodSelf5", "8")
            CodSelf6 = LeArquivoINI(nome_arquivo_ini, "Produto", "CodSelf6", "9")

            'IMPRESSORA            
            strImpressora = LeArquivoINI(nome_arquivo_ini, "Impressao", "Impressora", "Sem Impressora")
            strModeloLayout = LeArquivoINI(nome_arquivo_ini, "Impressao", "Layout", "Padrão")

            strRodape = LeArquivoINI(nome_arquivo_ini, "Impressao", "Rodape", "Volte Sempre.")
            strEnderecoLogo = LeArquivoINI(nome_arquivo_ini, "Impressao", "Logo", Application.StartupPath + "\logo.png")

            'COMANDA
            strModoGravarComanda = LeArquivoINI(nome_arquivo_ini, "Comanda", "ModoFuncionamento", "Automático")
            strPrefixoComanda = LeArquivoINI(nome_arquivo_ini, "Comanda", "Prefixo", "Nenhum")
            strModoAuto = LeArquivoINI(nome_arquivo_ini, "Comanda", "ModoAuto", "Dia - Hora - Min - Seg")
            strCodMesa = LeArquivoINI(nome_arquivo_ini, "Comanda", "Mesa", "99")

            strImpCodBarr = LeArquivoINI(nome_arquivo_ini, "Impressao", "Barra", "SIM")
            strImpQRCod = LeArquivoINI(nome_arquivo_ini, "Impressao", "QR", "SIM")
            strImpLogo = LeArquivoINI(nome_arquivo_ini, "Impressao", "Logoimp", Application.StartupPath + "SIM")

            'LAYOUT
            strD1 = LeArquivoINI(nome_arquivo_ini, "Layout", "d1", "407")
            strD2 = LeArquivoINI(nome_arquivo_ini, "Layout", "d2", "277")

            Debug.Print("Public Class CarregarParametros_INI : Public Sub LerArquivoini()")
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

    Public Class SQLControl : Implements IDisposable

        Private strEnderecoServidor = String.Empty
        Private strNomeBanco = String.Empty
        Private strUsuarioBanco = String.Empty
        Private strSenhaBanco = String.Empty
        Public strConexao As String = String.Empty
        Public strConexaoPostgres As String = String.Empty
        Private strInstrucao As String = String.Empty
        Private strInstrucaoPostgres As String = String.Empty

        Private objConexaoPostgres As New NpgsqlConnection()
        Private objCommandPostgres As New NpgsqlCommand(strInstrucaoPostgres, objConexaoPostgres)
        Private objDataReaderPostgres As NpgsqlDataReader
        Private daPostgres As New NpgsqlDataAdapter(strInstrucaoPostgres, strConexaoPostgres)

        Private objConexao As New SqlConnection()
        Private objCommand As New SqlCommand(strInstrucao, objConexao)
        Private objDataReader As SqlDataReader
        Private da As New SqlDataAdapter(strInstrucao, strConexao)

        Private log As New clsFuncoes.Log

        Public Class ProdutoObj : Implements IDisposable
            Public Codigo As String = String.Empty
            Public Codigo_Barra As String = String.Empty
            Public Nome As String = String.Empty
            Public Unidade As String = String.Empty
            Public Preco_Produto As String = String.Empty
#Region "IDisposable Support"
            Private disposedValue As Boolean ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: dispose managed state (managed objects).
                    End If

                    ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                    ' TODO: set large fields to null.
                End If
                Me.disposedValue = True
            End Sub

            ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
            'Protected Overrides Sub Finalize()
            '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            '    Dispose(False)
            '    MyBase.Finalize()
            'End Sub

            ' This code added by Visual Basic to correctly implement the disposable pattern.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region
        End Class

        Public Class MensagemObj : Implements IDisposable
            Public Mensagem As String = String.Empty

#Region "IDisposable Support"
            Private disposedValue As Boolean ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: dispose managed state (managed objects).
                    End If

                    ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                    ' TODO: set large fields to null.
                End If
                Me.disposedValue = True
            End Sub

            ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
            'Protected Overrides Sub Finalize()
            '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            '    Dispose(False)
            '    MyBase.Finalize()
            'End Sub

            ' This code added by Visual Basic to correctly implement the disposable pattern.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region
        End Class

        Sub New(ByVal objParametro As CarregarParametros_INI)
            objParametro.LerArquivoini()
            strEnderecoServidor = objParametro.strEnderecoServidor
            strNomeBanco = objParametro.strNomeBanco
            strUsuarioBanco = objParametro.strUsuarioBanco
            strSenhaBanco = objParametro.strSenhaBanco
            strConexao = "Data Source=" + strEnderecoServidor + ";Initial Catalog=" + strNomeBanco + ";User id=" + strUsuarioBanco + ";Password=" + strSenhaBanco + ";MultipleActiveResultSets=True;Connection Timeout=5"
            strConexaoPostgres = "Host=" + strEnderecoServidor + ";Port=5432;Database=" + strNomeBanco + ";Username=" + strUsuarioBanco + ";Password=" + strSenhaBanco + ";Timeout=5;"

            objConexao.ConnectionString = strConexao
            objConexaoPostgres.ConnectionString = strConexaoPostgres
        End Sub

        Public Function ConsultarProduto(ByVal Codigo_Produto As String, ByVal Produto As ProdutoObj, ByVal Formulario As String) As Boolean

            Try
                If objConexao.State = ConnectionState.Closed Then objConexao.Open()
                strInstrucao = ("Select Codigo,Codigo_Barra,Nome,Unidade,Preco_Produto From Produto Where Codigo =  @Codigo")

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao
                objCommand.Parameters.AddWithValue("@Codigo", Codigo_Produto)
                objCommand.CommandType = CommandType.Text
                objDataReader = objCommand.ExecuteReader()
                objDataReader.Read()

                If objDataReader.HasRows Then
                    Produto.Codigo = objDataReader("Codigo").ToString
                    Produto.Nome = objDataReader("Nome").ToString
                    Produto.Unidade = objDataReader("Unidade").ToString
                    Produto.Preco_Produto = objDataReader("Preco_Produto").ToString
                    objDataReader.Close()
                    Return True
                Else
                    Produto.Codigo = "0"
                    Produto.Nome = "Objeto não localizado."
                    Produto.Unidade = String.Empty
                    Produto.Preco_Produto = String.Empty

                    Return False
                End If

            Catch ex As Exception
                Produto.Codigo = String.Empty
                Produto.Nome = ex.Message.ToArray
                Produto.Unidade = String.Empty
                Produto.Preco_Produto = String.Empty
                log.GerarLogErro(ex, Formulario, "Public Function ConsultarProduto")
                Return False
            Finally
                objCommand.Parameters.Clear()
                objConexao.Close()
            End Try

        End Function

        Public Function ConsultarProdutoPostgres(ByVal Id As String, ByVal Produto As ProdutoObj, ByVal Formulario As String) As Boolean

            Try
                If objConexaoPostgres.State = ConnectionState.Closed Then objConexaoPostgres.Open()
                strInstrucaoPostgres = ("SELECT DISTINCT id,codigo_barras,descricao,unidade,valor_venda FROM public.produtos where id =  @Id")

                objCommandPostgres.CommandText = strInstrucaoPostgres
                objCommandPostgres.Connection = objConexaoPostgres
                objCommandPostgres.Parameters.AddWithValue("@Id", Convert.ToInt32(Id))
                objCommandPostgres.CommandType = CommandType.Text
                objDataReaderPostgres = objCommandPostgres.ExecuteReader()
                objDataReaderPostgres.Read()

                If objDataReaderPostgres.HasRows Then
                    Produto.Codigo = objDataReaderPostgres("id").ToString
                    Produto.Nome = objDataReaderPostgres("descricao").ToString
                    Produto.Unidade = objDataReaderPostgres("unidade").ToString
                    Produto.Preco_Produto = objDataReaderPostgres("valor_venda").ToString
                    objDataReaderPostgres.Close()
                    Return True
                Else
                    Produto.Codigo = "0"
                    Produto.Nome = "Objeto não localizado."
                    Produto.Unidade = String.Empty
                    Produto.Preco_Produto = String.Empty

                    Return False
                End If

            Catch ex As Exception
                Produto.Codigo = String.Empty
                Produto.Nome = ex.Message.ToArray
                Produto.Unidade = String.Empty
                Produto.Preco_Produto = String.Empty
                log.GerarLogErro(ex, Formulario, "Public Function ConsultarProdutoPostgres")
                Return False
            Finally
                objCommandPostgres.Parameters.Clear()
                objConexaoPostgres.Close()
            End Try

        End Function

        Function Testar_Conn(ByVal Formulario As String) As Boolean
            Try
                objConexao.Open()
                Return True
            Catch ex As Exception

                log.GerarLogErro(ex, Formulario, "Function Testar_Conn()")
                Return False
            Finally
                objConexao.Close()
            End Try
        End Function

        Function Testar_ConnPostgres(ByVal Formulario As String) As Boolean
            Try
                objConexaoPostgres.Open()
                Return True
            Catch ex As Exception

                log.GerarLogErro(ex, Formulario, "Function Testar_Conn()Postgres")
                Return False
            Finally
                objConexaoPostgres.Close()
            End Try
        End Function

        Function Testar_Conn_t(ByVal strEnderecoServidor_p As String,
                               ByVal strNomeBanco_p As String,
                               ByVal strUsuarioBanco_p As String,
                               ByVal strSenhaBanco_p As String,
                               ByVal Mensagem As MensagemObj,
                               ByVal Formulario As String) As Boolean

            strEnderecoServidor = strEnderecoServidor_p
            strNomeBanco = strNomeBanco_p
            strUsuarioBanco = strUsuarioBanco_p
            strSenhaBanco = strSenhaBanco_p
            strConexao = "Data Source=" + strEnderecoServidor + ";Initial Catalog=" + strNomeBanco + ";User id=" + strUsuarioBanco + ";Password=" + strSenhaBanco + ";MultipleActiveResultSets=True;Connection Timeout=3"
            objConexao.ConnectionString = strConexao

            Try
                objConexao.Open()
                Return True
            Catch ex As Exception
                Mensagem.Mensagem = ex.Message.ToString
                log.GerarLogErro(ex, Formulario, "Function Testar_Conn_t()")

                Return False
                '  MessageBox.Show(ex.Message.ToString, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                objConexao.Close()
            End Try
        End Function

        Function Testar_Conn_tPostgres(ByVal strEnderecoServidor_p As String,
                                       ByVal strNomeBanco_p As String,
                                       ByVal strUsuarioBanco_p As String,
                                       ByVal strSenhaBanco_p As String,
                                       ByVal Mensagem As MensagemObj,
                                       ByVal Formulario As String) As Boolean

            strEnderecoServidor = strEnderecoServidor_p
            strNomeBanco = strNomeBanco_p
            strUsuarioBanco = strUsuarioBanco_p
            strSenhaBanco = strSenhaBanco_p
            strConexaoPostgres = "Host=" + strEnderecoServidor + ";Port=5432;Database=" + strNomeBanco + ";Username=" + strUsuarioBanco + ";Password=" + strSenhaBanco + ";Timeout=5;"
            objConexaoPostgres.ConnectionString = strConexaoPostgres

            Try
                objConexaoPostgres.Open()
                Return True
            Catch ex As Exception
                Mensagem.Mensagem = ex.Message.ToString
                log.GerarLogErro(ex, Formulario, "Function Testar_Conn_t()Postgres")

                Return False
                '  MessageBox.Show(ex.Message.ToString, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                objConexaoPostgres.Close()
            End Try
        End Function

        Function GetCodigoUltimaComanda(ByVal Formulario As String)
            Try
                Dim CodigoUltimaComanda As String = String.Empty
                If objConexao.State = ConnectionState.Closed Then objConexao.Open()
                strInstrucao =
            "
                select max(Codigo+1) from Venda_Espera_SelfComanda
            "

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao
                objCommand.CommandType = CommandType.Text
                CodigoUltimaComanda = objCommand.ExecuteScalar
                objCommand.Parameters.Clear()
                objConexao.Close()
                Return CodigoUltimaComanda
            Catch ex As Exception
                log.GerarLogErro(ex, Formulario, "Function GetCodigoUltimaComanda()")
                Return 0
            Finally
                objConexao.Close()
            End Try
        End Function

        Function GetCodigoUltimaComanda_Config(ByVal Formulario As String)
            Try
                Dim CodigoUltimaComanda As String = String.Empty
                If objConexao.State = ConnectionState.Closed Then objConexao.Open()
                strInstrucao =
            "
                select max(Codigo_Ultima_Comanda+1) from Config_SelfComanda
            "

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao
                objCommand.CommandType = CommandType.Text
                CodigoUltimaComanda = objCommand.ExecuteScalar
                objCommand.Parameters.Clear()
                objConexao.Close()
                UpdateGetCodigoUltimaComanda_Config()
                Return CodigoUltimaComanda
            Catch ex As Exception
                log.GerarLogErro(ex, Formulario, "Function GetCodigoUltimaComanda()")
                Return 0
            Finally
                objConexao.Close()
            End Try
        End Function

        Function GetCodigoUltimaComanda_ConfigPostgres(ByVal Formulario As String)
            Try
                Dim CodigoUltimaComanda As String = String.Empty
                If objConexaoPostgres.State = ConnectionState.Closed Then objConexaoPostgres.Open()
                strInstrucaoPostgres =
            "
                select max(Codigo_Ultima_Comanda+1) from Config_SelfComanda
            "

                objCommandPostgres.CommandText = strInstrucaoPostgres
                objCommandPostgres.Connection = objConexaoPostgres
                objCommandPostgres.CommandType = CommandType.Text
                CodigoUltimaComanda = objCommandPostgres.ExecuteScalar
                objCommandPostgres.Parameters.Clear()
                objConexaoPostgres.Close()
                UpdateGetCodigoUltimaComanda_ConfigPostgres()
                Return CodigoUltimaComanda
            Catch ex As Exception
                log.GerarLogErro(ex, Formulario, "Function GetCodigoUltimaComanda()Postgres")
                Return 0
            Finally
                objConexaoPostgres.Close()
            End Try
        End Function

        Private Sub UpdateGetCodigoUltimaComanda_Config()
            Try

                If objConexao.State = ConnectionState.Closed Then objConexao.Open()
                strInstrucao =
            "
                Update Config_SelfComanda set Codigo_Ultima_Comanda = Codigo_Ultima_Comanda + 1
            "

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao
                objCommand.CommandType = CommandType.Text
                objCommand.ExecuteNonQuery()
                objCommand.Parameters.Clear()
                objConexao.Close()

            Catch ex As Exception
                log.GerarLogErro(ex, "UpdateGetCodigoUltimaComanda_Config()", "Function GetCodigoUltimaComanda()")

            Finally
                objConexao.Close()
            End Try
        End Sub

        Private Sub UpdateGetCodigoUltimaComanda_ConfigPostgres()
            Try

                If objConexaoPostgres.State = ConnectionState.Closed Then objConexaoPostgres.Open()
                strInstrucaoPostgres =
            "
                Update Config_SelfComanda set Codigo_Ultima_Comanda = Codigo_Ultima_Comanda + 1
            "

                objCommandPostgres.CommandText = strInstrucaoPostgres
                objCommandPostgres.Connection = objConexaoPostgres
                objCommandPostgres.CommandType = CommandType.Text
                objCommandPostgres.ExecuteNonQuery()
                objCommandPostgres.Parameters.Clear()
                objConexaoPostgres.Close()

            Catch ex As Exception
                log.GerarLogErro(ex, "UpdateGetCodigoUltimaComanda_Config()", "Function GetCodigoUltimaComanda()Postgres")

            Finally
                objConexaoPostgres.Close()
            End Try
        End Sub

        Public Sub UpdateGetCodigoUltimaComanda_Zerar()
            Try

                If objConexao.State = ConnectionState.Closed Then objConexao.Open()
                strInstrucao =
            "
                Update Config_SelfComanda set Codigo_Ultima_Comanda =0
            "

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao
                objCommand.CommandType = CommandType.Text
                objCommand.ExecuteNonQuery()
                objCommand.Parameters.Clear()
                objConexao.Close()
                MessageBox.Show("Comandas zeradas com sucesso", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                log.GerarLogErro(ex, "frmConfig", "UpdateGetCodigoUltimaComanda_Zerar()")

            Finally
                objConexao.Close()
            End Try
        End Sub

        Public Sub UpdateGetCodigoUltimaComanda_ZerarPostgres()
            Try

                If objConexaoPostgres.State = ConnectionState.Closed Then objConexaoPostgres.Open()
                strInstrucaoPostgres =
            "
                Update Config_SelfComanda set Codigo_Ultima_Comanda =0
            "

                objCommandPostgres.CommandText = strInstrucaoPostgres
                objCommandPostgres.Connection = objConexaoPostgres
                objCommandPostgres.CommandType = CommandType.Text
                objCommandPostgres.ExecuteNonQuery()
                objCommandPostgres.Parameters.Clear()
                objConexaoPostgres.Close()
                MessageBox.Show("Comandas zeradas com sucesso", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                log.GerarLogErro(ex, "frmConfig", "UpdateGetCodigoUltimaComanda_Zerar()Postgres")

            Finally
                objConexaoPostgres.Close()
            End Try
        End Sub

        Function GetDataHoraServidor()
            Try
                Dim DataHora As String = String.Empty

                If objConexao.State = ConnectionState.Closed Then objConexao.Open()

                strInstrucao =
            "
                select getdate()
            "

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao
                objCommand.CommandType = CommandType.Text
                DataHora = objCommand.ExecuteScalar
                objCommand.Parameters.Clear()
                objConexao.Close()
                Return DataHora
            Catch ex As Exception
                log.GerarLogErro(ex, "clsFuncoes", "Function GetDataHoraServidor()")
                Return 0
            End Try
        End Function

        Function InserirComanda(ByVal strCodProduto_p As String,
                                ByVal strCodEmpresa_p As String,
                                ByVal strCodDeposito_p As String,
                                ByVal strCodVendedor_p As String,
                                ByVal strQuantidade_p As String,
                                ByVal strUnitario_p As String,
                                ByVal strTotal_p As String,
                                ByVal strDiaHoraMinuto_p As String,
                                ByVal strCodMesa_p As String) As Boolean

            Dim strDiaHoraMinuto As String = strDiaHoraMinuto_p

            Try
                strInstrucao =
                "
                 INSERT INTO Venda_Espera(Item, Codigo,Mesa, Empresa, Data, Produto, Quantidade,Unitario, Total, Cancelado, Vendedor, Deposito)          
                 SELECT (SELECT ISNULL(MAX(Item),0)+1 FROM Venda_Espera WHERE Codigo=@strDiaHoraMinuto) AS Item,
                 @strDiaHoraMinuto,
                 @strCodMesa,
                 @strCodEmpresa_p,
                 GetDate(),
                 @strCodProduto_p,
                 @strQuantidade_p,
                 @strUnitario_p,
                 @strTotal_p,
                 'N',
                 @strCodVendedor_p,
                 @strCodDeposito_p
                "

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao

                objCommand.Parameters.AddWithValue("@strDiaHoraMinuto", SqlDbType.Char).Value = strDiaHoraMinuto
                objCommand.Parameters.Add("@strCodProduto_p", SqlDbType.Int).Value = CType(strCodProduto_p, Int32)
                objCommand.Parameters.Add("@strCodEmpresa_p", SqlDbType.Int).Value = CType(strCodEmpresa_p, Int32)
                objCommand.Parameters.AddWithValue("@strCodDeposito_p", SqlDbType.Int).Value = CType(strCodDeposito_p, Int32)
                objCommand.Parameters.AddWithValue("@strCodVendedor_p", SqlDbType.Int).Value = CType(strCodVendedor_p, Int32)
                objCommand.Parameters.AddWithValue("@strQuantidade_p", SqlDbType.Decimal).Value = CType(strQuantidade_p, Decimal)
                objCommand.Parameters.AddWithValue("@strUnitario_p", SqlDbType.Decimal).Value = CType(strUnitario_p, Decimal)
                objCommand.Parameters.AddWithValue("@strTotal_p", SqlDbType.Decimal).Value = CType(strTotal_p, Decimal)

                If strCodMesa_p = "" Or strCodMesa_p = String.Empty Then
                    objCommand.Parameters.AddWithValue("@strCodMesa", SqlDbType.Int).Value = DBNull.Value
                Else
                    objCommand.Parameters.AddWithValue("@strCodMesa", SqlDbType.Int).Value = CType(strCodMesa_p, Int32)
                End If


                objConexao.Open()
                objCommand.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                Application.CompanyName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                log.GerarLogErro(ex, "frmPdvVenda", "Function InserirComanda")

                Return False

            Finally
                objCommand.Parameters.Clear()
                objConexao.Close()
            End Try

        End Function

        Function InserirComandaPostgres(ByVal strCodProduto_p As String,
                                        ByVal strCodEmpresa_p As String,
                                        ByVal strCodDeposito_p As String,
                                        ByVal strCodVendedor_p As String,
                                        ByVal strQuantidade_p As String,
                                        ByVal strUnitario_p As String,
                                        ByVal strTotal_p As String,
                                        ByVal strDiaHoraMinuto_p As String,
                                        ByVal strCodMesa_p As String) As Boolean

            Dim strDiaHoraMinuto As String = strDiaHoraMinuto_p

            objConexaoPostgres.Open()
            ' Início da transação para garantir atomicidade
            Dim transaction As NpgsqlTransaction = objConexaoPostgres.BeginTransaction()

            Try
                objCommandPostgres.Transaction = transaction

                strInstrucaoPostgres =
                "
                        INSERT INTO public.almocos
                        (
                        sequencia_diaria,
                        natureza,
                        identificacao,
                        mesa,
                        complemento_identificador,
                        codigo_referencial_pessoa,
                        codigo_referencial,
                        data,
                        cancelado,
                        enviado,
                        pago,
                        fechado,
                        imprimir_resumo_fechamento,
                        delivery,
                        complemento_delivery,
                        levar_maquina_cartao,
                        levar_troco_para,
                        observacao,
                        hora,
                        troco,
                        valor_informado,
                        usuario_id,
                        modelo,
                        xml,
                        numero_pessoas,
                        status_nota,
                        nome_nota_fiscal,
                        cpf_nota_fiscal,
                        mensagem_retorno,
                        detalhe_retorno,
                        codigo_referencial_pessoa_entregador,
                        hora_saida_entrega,
                        hora_retorno_entrega,
                        chave,
                        tipo_emissao,
                        tipo_pedido,
                        tipo_importacao,
                        bandeira,
                        numero_pre_venda_dj,
                        numero_nota_fiscal,
                        serie_nota_fiscal,
                        retirar_no_local
                        )
                        VALUES 
                        (
                        @sequencia_diaria,
                        @natureza,
                        @identificacao,
                        @mesa,
                        @complemento_identificador,
                        @codigo_referencial_pessoa,
                        @codigo_referencial,
                        @data,
                        @cancelado,
                        @enviado,
                        @pago,
                        @fechado,
                        @imprimir_resumo_fechamento,
                        @delivery,
                        @complemento_delivery,
                        @levar_maquina_cartao,
                        @levar_troco_para,
                        @observacao,
                        @hora,
                        @troco,
                        @valor_informado,
                        @usuario_id,
                        @modelo,
                        @xml,
                        @numero_pessoas,
                        @status_nota,
                        @nome_nota_fiscal,
                        @cpf_nota_fiscal,
                        @mensagem_retorno,
                        @detalhe_retorno,
                        @codigo_referencial_pessoa_entregador,
                        @hora_saida_entrega,
                        @hora_retorno_entrega,
                        @chave,
                        @tipo_emissao,
                        @tipo_pedido,
                        @tipo_importacao,
                        @bandeira,
                        @numero_pre_venda_dj,
                        @numero_nota_fiscal,
                        @serie_nota_fiscal,
                        @retirar_no_local
                        ) RETURNING id;                   
                "

                objCommandPostgres.CommandText = strInstrucaoPostgres
                objCommandPostgres.Connection = objConexaoPostgres

                objCommandPostgres.Parameters.Clear()

                objCommandPostgres.Parameters.AddWithValue("@sequencia_diaria", Convert.ToInt32(strDiaHoraMinuto)) 'Numero comanda
                objCommandPostgres.Parameters.AddWithValue("@natureza", "S")
                objCommandPostgres.Parameters.AddWithValue("@identificacao", Convert.ToInt32(strDiaHoraMinuto)) 'Numero comanda             
                objCommandPostgres.Parameters.AddWithValue("@complemento_identificador", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@codigo_referencial_pessoa", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@codigo_referencial", CodigoReferencial)
                objCommandPostgres.Parameters.AddWithValue("@data", DateTime.Now.Date)
                objCommandPostgres.Parameters.AddWithValue("@cancelado", False)
                objCommandPostgres.Parameters.AddWithValue("@enviado", False)
                objCommandPostgres.Parameters.AddWithValue("@pago", False)
                objCommandPostgres.Parameters.AddWithValue("@fechado", False)
                objCommandPostgres.Parameters.AddWithValue("@imprimir_resumo_fechamento", False)
                objCommandPostgres.Parameters.AddWithValue("@delivery", False)
                objCommandPostgres.Parameters.AddWithValue("@complemento_delivery", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@levar_maquina_cartao", False)
                objCommandPostgres.Parameters.AddWithValue("@levar_troco_para", 0.00D)
                objCommandPostgres.Parameters.AddWithValue("@observacao", "Gerado pelo autoatendimento")
                objCommandPostgres.Parameters.AddWithValue("@hora", DateTime.Now)
                objCommandPostgres.Parameters.AddWithValue("@troco", 0.00D)
                objCommandPostgres.Parameters.AddWithValue("@valor_informado", 0.00D)
                objCommandPostgres.Parameters.AddWithValue("@usuario_id", 0)
                objCommandPostgres.Parameters.AddWithValue("@modelo", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@xml", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@numero_pessoas", 1)
                objCommandPostgres.Parameters.AddWithValue("@status_nota", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@nome_nota_fiscal", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@cpf_nota_fiscal", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@mensagem_retorno", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@detalhe_retorno", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@codigo_referencial_pessoa_entregador", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@hora_saida_entrega", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@hora_retorno_entrega", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@chave", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@tipo_emissao", 1)
                objCommandPostgres.Parameters.AddWithValue("@tipo_pedido", "AUTO_ATENDIMENTO_BALANCA")
                objCommandPostgres.Parameters.AddWithValue("@tipo_importacao", "SC")
                objCommandPostgres.Parameters.AddWithValue("@bandeira", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@numero_pre_venda_dj", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@numero_nota_fiscal", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@serie_nota_fiscal", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@retirar_no_local", False)

                If strCodMesa_p = "" Or strCodMesa_p = String.Empty Then
                    objCommandPostgres.Parameters.AddWithValue("@mesa", DBNull.Value)
                Else
                    objCommandPostgres.Parameters.AddWithValue("@mesa", Convert.ToInt32(strCodMesa_p))
                End If

                Dim idGerado As Integer = Convert.ToInt32(objCommandPostgres.ExecuteScalar())

                ' Segundo INSERT usando o ID gerado
                strInstrucaoPostgres =
                    "
                        INSERT INTO public.almoco_produtos 
                        (
                        almoco_id,
                        produto_id, 
                        quantidade,
                        valor_unitario,
                        valor_original,
                        desconto,
                        valor_total,
                        status,
                        almoco_produtos_id,
                        pago,
                        vale_impresso,
                        hora,
                        hora_notificacao,
                        hora_producao,
                        hora_conclusao_producao,
                        hora_entregue,
                        id_usuario_producao,
                        usuario_id,
                        ordem,
                        nome_cliente,
                        sequencia_producao,
                        tipo_pedido,
                        descricao_produto,
                        codigo_referencial
                        )
                        VALUES 
                        (
                        @almoco_id,
                        @produto_id, 
                        @quantidade,
                        @valor_unitario,
                        @valor_original,
                        @desconto,
                        @valor_total,
                        @status,
                        @almoco_produtos_id,
                        @pago,
                        @vale_impresso,
                        @hora,
                        @hora_notificacao,
                        @hora_producao,
                        @hora_conclusao_producao,
                        @hora_entregue,
                        @id_usuario_producao,
                        @usuario_id,
                        @ordem,
                        @nome_cliente,
                        @sequencia_producao,
                        @tipo_pedido,
                        @descricao_produto,
                        @codigo_referencial
                        )
                    "

                objCommandPostgres.CommandText = strInstrucaoPostgres

                objCommandPostgres.Parameters.Clear()

                objCommandPostgres.Parameters.AddWithValue("@almoco_id", idGerado)
                objCommandPostgres.Parameters.AddWithValue("@produto_id", Convert.ToInt32(strCodProduto_p))
                objCommandPostgres.Parameters.AddWithValue("@quantidade", Convert.ToDecimal(strQuantidade_p))
                objCommandPostgres.Parameters.AddWithValue("@valor_unitario", Convert.ToDecimal(strUnitario_p))
                objCommandPostgres.Parameters.AddWithValue("@valor_original", Convert.ToDecimal(strUnitario_p))
                objCommandPostgres.Parameters.AddWithValue("@desconto", 0)
                objCommandPostgres.Parameters.AddWithValue("@valor_total", Convert.ToDecimal(strTotal_p))
                objCommandPostgres.Parameters.AddWithValue("@status", "F")
                objCommandPostgres.Parameters.AddWithValue("@almoco_produtos_id", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@pago", False)
                objCommandPostgres.Parameters.AddWithValue("@vale_impresso", False)
                objCommandPostgres.Parameters.AddWithValue("@hora", DateTime.Now)
                objCommandPostgres.Parameters.AddWithValue("@hora_notificacao", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@hora_producao", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@hora_conclusao_producao", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@hora_entregue", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@id_usuario_producao", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@usuario_id", 18161)
                objCommandPostgres.Parameters.AddWithValue("@ordem", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@nome_cliente", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@sequencia_producao", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@tipo_pedido", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@descricao_produto", DBNull.Value)
                objCommandPostgres.Parameters.AddWithValue("@codigo_referencial", DBNull.Value)

                objCommandPostgres.ExecuteNonQuery()
                transaction.Commit()

                Return True

            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                Application.CompanyName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                log.GerarLogErro(ex, "frmPdvVenda", "Function InserirComandaPostgres")

                transaction.Rollback()
                Return False

            Finally
                objCommandPostgres.Parameters.Clear()
                transaction.Dispose()
                objConexaoPostgres.Close()
            End Try

        End Function

        Function CodigoReferencial() As String
            Dim agora As DateTime = DateTime.Now

            Dim codigo As String = "SISCHEF" &
            agora.Year.ToString("0000") &
            agora.Month.ToString("00") &
            agora.Day.ToString("00") &
            agora.Hour.ToString("00") &
            agora.Minute.ToString("00") &
            agora.Second.ToString("00") &
            agora.Millisecond.ToString("000000") ' 6 dígitos de milissegundos

            Return codigo
        End Function






        Function InserirComanda_s(ByVal strCodProduto_p As String,
                                  ByVal strCodEmpresa_p As String,
                                  ByVal strCodDeposito_p As String,
                                  ByVal strCodVendedor_p As String,
                                  ByVal strQuantidade_p As String,
                                  ByVal strUnitario_p As String,
                                  ByVal strTotal_p As String,
                                  ByVal strDiaHoraMinuto_p As String,
                                  ByVal strCodMesa_p As String) As Boolean

            Dim strDiaHoraMinuto As String = strDiaHoraMinuto_p

            Try
                strInstrucao =
            "
             INSERT INTO Venda_Espera_SelfComanda(Item, Codigo,Mesa, Empresa, Data, Produto, Quantidade,Unitario, Total, Cancelado, Vendedor, Deposito)          
                SELECT (SELECT ISNULL(MAX(Item),0)+1 FROM Venda_Espera_SelfComanda WHERE Codigo=@strDiaHoraMinuto) AS Item,
                @strDiaHoraMinuto,
                @strCodMesa,
                @strCodEmpresa_p,
                GetDate(),
                @strCodProduto_p,
                @strQuantidade_p,
                @strUnitario_p,
                @strTotal_p,
                'N',
                @strCodVendedor_p,
                @strCodDeposito_p
            "

                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao

                objCommand.Parameters.Add("@strDiaHoraMinuto", SqlDbType.Char).Value = strDiaHoraMinuto
                objCommand.Parameters.Add("@strCodProduto_p", SqlDbType.Int).Value = CType(strCodProduto_p, Int32)
                objCommand.Parameters.Add("@strCodEmpresa_p", SqlDbType.Int).Value = strCodEmpresa_p
                objCommand.Parameters.Add("@strCodDeposito_p", SqlDbType.Int).Value = CType(strCodDeposito_p, Int32)
                objCommand.Parameters.Add("@strCodVendedor_p", SqlDbType.Int).Value = CType(strCodVendedor_p, Int32)
                objCommand.Parameters.Add("@strQuantidade_p", SqlDbType.Decimal).Value = CType(strQuantidade_p, Decimal)
                objCommand.Parameters.Add("@strUnitario_p", SqlDbType.Decimal).Value = CType(strUnitario_p, Decimal)
                objCommand.Parameters.Add("@strTotal_p", SqlDbType.Decimal).Value = CType(strTotal_p, Decimal)

                If strCodMesa_p = "" Or strCodMesa_p = String.Empty Then
                    objCommand.Parameters.AddWithValue("@strCodMesa", SqlDbType.Int).Value = DBNull.Value
                Else
                    objCommand.Parameters.AddWithValue("@strCodMesa", SqlDbType.Int).Value = CType(strCodMesa_p, Int32)
                End If


                objConexao.Open()
                objCommand.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                Application.CompanyName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                log.GerarLogErro(ex, "frmPdvVenda", "Function InserirComanda_s")

                Return False

            Finally
                objCommand.Parameters.Clear()
                objConexao.Close()
            End Try

        End Function

        Function InserirComanda_sPostgres(ByVal strCodProduto_p As String,
                                  ByVal strCodEmpresa_p As String,
                                  ByVal strCodDeposito_p As String,
                                  ByVal strCodVendedor_p As String,
                                  ByVal strQuantidade_p As String,
                                  ByVal strUnitario_p As String,
                                  ByVal strTotal_p As String,
                                  ByVal strDiaHoraMinuto_p As String,
                                  ByVal strCodMesa_p As String) As Boolean

            Dim strDiaHoraMinuto As String = strDiaHoraMinuto_p

            Try
                strInstrucaoPostgres =
            "
INSERT INTO Venda_Espera_SelfComanda (
    Item, Codigo, Mesa, Empresa, Data, Produto, Quantidade, Unitario, Total, Cancelado, Vendedor, Deposito
)
    SELECT COALESCE(MAX(Item), 0) + 1, 
    @strDiaHoraMinuto,
    @strCodMesa,
    @strCodEmpresa_p,
    NOW(),
    @strCodProduto_p,
    @strQuantidade_p,
    @strUnitario_p,
    @strTotal_p,
    'N',
    @strCodVendedor_p,
    @strCodDeposito_p
    FROM Venda_Espera_SelfComanda 

            "

                objCommandPostgres.CommandText = strInstrucaoPostgres
                objCommandPostgres.Connection = objConexaoPostgres

                objCommandPostgres.Parameters.AddWithValue("@strDiaHoraMinuto", strDiaHoraMinuto)
                objCommandPostgres.Parameters.AddWithValue("@strCodProduto_p", Convert.ToInt32(strCodProduto_p))
                objCommandPostgres.Parameters.AddWithValue("@strCodEmpresa_p", Convert.ToInt32(strCodEmpresa_p))
                objCommandPostgres.Parameters.AddWithValue("@strCodDeposito_p",Convert.ToInt32(strCodDeposito_p))
                objCommandPostgres.Parameters.AddWithValue("@strCodVendedor_p",Convert.ToInt32(strCodVendedor_p))
                objCommandPostgres.Parameters.AddWithValue("@strQuantidade_p", Convert.ToDecimal(strQuantidade_p))
                objCommandPostgres.Parameters.AddWithValue("@strUnitario_p", Convert.ToDecimal(strUnitario_p))
                objCommandPostgres.Parameters.AddWithValue("@strTotal_p", Convert.ToDecimal(strTotal_p))

                If strCodMesa_p = "" Or strCodMesa_p = String.Empty Then
                    objCommandPostgres.Parameters.AddWithValue("@strCodMesa", DBNull.Value)
                Else
                    objCommandPostgres.Parameters.AddWithValue("@strCodMesa", Convert.ToInt32(strCodMesa_p))
                End If


                objConexaoPostgres.Open()
                objCommandPostgres.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                Application.CompanyName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                log.GerarLogErro(ex, "frmPdvVenda", "Function InserirComanda_sPostgres")

                Return False

            Finally
                objCommandPostgres.Parameters.Clear()
                objConexaoPostgres.Close()
            End Try

        End Function


        Function Verifica_Empresa(ByVal strRazaoEmpresa As String) As Boolean

            Dim strEmpresa As String = String.Empty

            Try
                If objConexao.State = ConnectionState.Closed Then objConexao.Open()
                strInstrucao = "Select * from Empresa where padrao = 'S'"
                objCommand.CommandText = strInstrucao
                objCommand.Connection = objConexao
                objDataReader = objCommand.ExecuteReader

                While objDataReader.Read
                    If objDataReader.HasRows = True Then
                        strEmpresa = objDataReader.Item(1).ToString
                    End If
                End While
                objCommand.Parameters.Clear()
                objDataReader.Close()
                objConexao.Close()

                If strEmpresa <> strRazaoEmpresa Then
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception
                log.GerarLogErro(ex, "clsFuncoes", "Function Verifica_Empresa")
                Return 0
                Application.Exit()
            End Try


        End Function


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region


    End Class

    Public Class Imprimir

        Private strCodigoComanda As String = String.Empty
        Private strNomeProduto As String = String.Empty
        Private strPesoProduto As String = String.Empty
        Private strTotalProduto As String = String.Empty
        Private strRodape = String.Empty
        Private strImpressora = String.Empty
        Private strPrefixoComanda = String.Empty
        Private strEnderecoLogo = String.Empty
        Private picLogo As Bitmap
        Private log As New clsFuncoes.Log

        Sub New(ByVal objParametro As CarregarParametros_INI)
            strImpressora = objParametro.strImpressora
            strRodape = objParametro.strRodape
            strEnderecoLogo = objParametro.strEnderecoLogo
            picLogo = Grayscale(Image.FromFile(strEnderecoLogo))
        End Sub

        Public Function Grayscale(ByRef source As Bitmap) As Bitmap
            Dim luma As Integer
            Dim x As Integer
            Dim y As Integer
            Dim c As Color
            For y = 0 To source.Height - 1
                For x = 0 To source.Width - 1
                    c = source.GetPixel(x, y)
                    luma = CInt(c.R * 0.3 + c.G * 0.59 + c.B * 0.11)
                    source.SetPixel(x, y, Color.FromArgb(luma, luma, luma))
                Next
            Next
            Return source
            source.Dispose()
        End Function

        Public Shared Function ImageToByteArray(Image As System.Drawing.Image) As Byte()
            Using Ms = New System.IO.MemoryStream()
                Image.Save(Ms, Imaging.ImageFormat.Png)
                Return Ms.ToArray()
            End Using
        End Function

        Public Sub Imprimir(ByVal strCodigoComanda_p As String,
                            ByVal strNomeProduto_p As String,
                            ByVal strPesoProduto_p As String,
                            ByVal strTotalProduto_p As String,
                            ByVal impCodBarr As String,
                            ByVal impQRCode As String,
                            ByVal implOGO As String,
                            teste As Boolean
                           )

            Try

                Dim codigoBarras As Zen.Barcode.Code128BarcodeDraw = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum
                Dim codigoQrCode As Zen.Barcode.CodeQrBarcodeDraw = Zen.Barcode.BarcodeDrawFactory.CodeQr

                Dim byt_Logo_IMG() As Byte = ImageToByteArray(picLogo)
                Dim byt_CodC_IMG_QR() As Byte = ImageToByteArray(codigoQrCode.Draw(strCodigoComanda_p, 30))
                Dim byt_CodC_IMG() As Byte = ImageToByteArray(codigoBarras.Draw(strCodigoComanda_p, 30))


                Using ds As New dsImpressao
                    ds.dtImpressao.Rows.Add(byt_Logo_IMG,
                                            strNomeProduto_p,
                                            strPesoProduto_p,
                                            strTotalProduto_p,
                                            "",
                                            strCodigoComanda_p,
                                            strRodape,
                                            byt_CodC_IMG,
                                            byt_CodC_IMG_QR
                                            )

                    Using objRpt As New rptComanda

                        'oPageSettings.PaperSize = New System.Drawing.Printing.PaperSize("cozinha", 50, 50)

                        If teste = True Then
                            objRpt.FileName = Path.GetTempPath + "rptComanda.rpt"



                            objRpt.SetDataSource(ds.Tables("dtImpressao"))
                            objRpt.PrintOptions.NoPrinter = False

                            If implOGO = "NÃO" Then
                                objRpt.ReportDefinition.Sections("Section1").SectionFormat.EnableSuppress = True
                            End If

                            If impQRCode = "NÃO" Then
                                objRpt.ReportDefinition.Sections("DetailSection3").SectionFormat.EnableSuppress = True
                            End If

                            If impCodBarr = "NÃO" Then
                                objRpt.ReportDefinition.Sections("DetailSection2").SectionFormat.EnableSuppress = True
                            End If

                            objRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Application.StartupPath + "\" + strCodigoComanda_p + ".pdf")
                            Exit Sub
                        End If

                        If strImpressora = "Sem Impressora" Then
                            objRpt.FileName = Path.GetTempPath + "rptComanda.rpt"
                            objRpt.SetDataSource(ds.Tables("dtImpressao"))
                            objRpt.PrintOptions.NoPrinter = False

                            If implOGO = "NÃO" Then
                                objRpt.ReportDefinition.Sections("Section1").SectionFormat.EnableSuppress = True
                            End If

                            If impQRCode = "NÃO" Then
                                objRpt.ReportDefinition.Sections("DetailSection3").SectionFormat.EnableSuppress = True
                            End If

                            If impCodBarr = "NÃO" Then
                                objRpt.ReportDefinition.Sections("DetailSection2").SectionFormat.EnableSuppress = True
                            End If

                            objRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Application.StartupPath + "\" + strCodigoComanda_p + "_" + Format(Now, "ddHHmmss") + ".pdf")
                            Exit Sub
                        End If

                        objRpt.FileName = Path.GetTempPath + "rptComanda.rpt"
                        objRpt.SetDataSource(ds.Tables("dtImpressao"))
                        objRpt.PrintOptions.NoPrinter = False
                        objRpt.PrintOptions.PrinterName = strImpressora

                        If implOGO = "NÃO" Then
                            objRpt.ReportDefinition.Sections("Section1").SectionFormat.EnableSuppress = True
                        End If

                        If impQRCode = "NÃO" Then
                            objRpt.ReportDefinition.Sections("DetailSection3").SectionFormat.EnableSuppress = True
                        End If

                        If impCodBarr = "NÃO" Then
                            objRpt.ReportDefinition.Sections("DetailSection2").SectionFormat.EnableSuppress = True
                        End If

                        ' Using dialogo As New PrintDialog

                        ' If dialogo.ShowDialog = DialogResult.OK Then
                        '    objRpt.PrintToPrinter(dialogo.PrinterSettings, dialogo.PrinterSettings.DefaultPageSettings, False)
                        ' End If
                        'objRpt.PrintToPrinter(1, False, 0, 0)
                        'End Using
                        ' Dim oPrinterSettings As New Printing.PrinterSettings
                        ' Dim oPageSettings As New System.Drawing.Printing.PageSettings

                        ' oPrinterSettings.PrinterName = strImpressora
                        ' oPageSettings.PaperSize = New Printing.PaperSize("custon", 275, 3000)
                        'oPageSettings.PaperSize = New System.Drawing.Printing.PaperSize("cozinha", 50, 50)
                        ' objRpt.PrintToPrinter(oPrinterSettings, oPageSettings, False)

                        objRpt.PrintToPrinter(1, False, 0, 0)
                    End Using
                    ds.dtImpressao.Clear()
                End Using

            Catch ex As Exception
                log.GerarLogErro(ex, "clsFuncoes", "Public Sub Imprimir")
            End Try
        End Sub

    End Class


End Class
