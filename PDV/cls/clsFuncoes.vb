Imports System.Text
Imports System.Data.SqlClient
Imports System.IO

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
        Private strInstrucao As String = String.Empty

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

            objConexao.ConnectionString = strConexao
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

        Function Testar_Conn_t(ByVal strEnderecoServidor_p As String,
                             ByVal strNomeBanco_p As String,
                             ByVal strUsuarioBanco_p As String,
                             ByVal strSenhaBanco_p As String,
                             ByVal Mensagem As MensagemObj, ByVal Formulario As String) As Boolean

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
