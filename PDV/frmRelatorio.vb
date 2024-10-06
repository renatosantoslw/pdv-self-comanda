Imports System.Data.SqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmRelatorio
    'Dim CaminhoRelatorioRPT As String = Application.StartupPath + "\rptRelatorio.rpt"
    Dim CaminhoRelatorioRPT As String = Path.GetTempPath + "\rptRelatorio.rpt"
    'Application.StartupPath + "\rptRelatorio.rpt"


    Dim objRpt As New rptRelatorio
    Public StringConexao
    Dim sServer
    Dim sDatabase
    Public sSQLQuery

    Public crtDatas As String
    Public crtPesos As String

    Public Sub DefinirConfiguracoes(ConexaoBanco As String, NomeServidor As String, NomeBanco As String, SQLQuery As String)
        StringConexao = ConexaoBanco
        sServer = NomeServidor
        sDatabase = NomeBanco
        sSQLQuery = SQLQuery
    End Sub

    Private Sub frmRelatorio_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim SQLCon As SqlConnection = New SqlConnection(StringConexao)
        Dim da As New SqlDataAdapter(sSQLQuery, SQLCon)
        Dim ds As New dsImpressao

        Try

            objRpt.FileName = CaminhoRelatorioRPT
            SQLCon.Open()
            da.Fill(ds, "Produto")
            objRpt.SetDataSource(ds.Tables(1))

            Dim crtTextObjectOnReportDatas As CrystalDecisions.CrystalReports.Engine.TextObject
            Dim crtTextObjectOnReportPesos As CrystalDecisions.CrystalReports.Engine.TextObject

            crtTextObjectOnReportDatas = CType(objRpt.ReportDefinition.ReportObjects.Item("crltxtDatas"), CrystalDecisions.CrystalReports.Engine.TextObject)
            crtTextObjectOnReportPesos = CType(objRpt.ReportDefinition.ReportObjects.Item("crltxtPesos"), CrystalDecisions.CrystalReports.Engine.TextObject)


            crtTextObjectOnReportDatas.Text = crtDatas
            crtTextObjectOnReportPesos.Text = crtPesos



            rptVizualizar.ReportSource = objRpt
            rptVizualizar.Refresh()
            rptVizualizar.RefreshReport()


        Catch ex As Exception
            MessageBox.Show(ex.Message,
                                    Application.CompanyName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning)

            Me.Close()
        Finally
            SQLCon.Close()
            SQLCon.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try




    End Sub
End Class