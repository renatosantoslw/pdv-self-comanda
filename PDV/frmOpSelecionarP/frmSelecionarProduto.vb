Public Class frmSelecionarProduto

    Private Parametros As New clsFuncoes.CarregarParametros_INI
    Private SQLControl As New clsFuncoes.SQLControl(Parametros)
    Private ProdutoObj As New clsFuncoes.SQLControl.ProdutoObj


    Private Sub wait(ByVal millisecondsTimeout As Integer)
        Threading.Thread.Sleep(millisecondsTimeout)
        Application.DoEvents()
    End Sub
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Dim str As String = Parametros.strstrCodProdutoOpcio
        Dim palavras As String() = str.Split(New Char() {","c})
        Dim palavra As String
        Dim array As Integer = 1

        For Each palavra In palavras
            Dim btnProdutos As New frmBtnSelecionarProduto
            With btnProdutos
                .Opacity = 0
                .lblindice.Text = "F" + array.ToString
                .Name = palavra
                .TopLevel = False
                .Parent = flpOP
                .Dock = DockStyle.None
                .Left = (btnProdutos.pnBT.Width - .Width) / 2
                .Top = (btnProdutos.pnBT.Height - .Height) / 2
            End With

            array = array + 1

            If Parametros.strSistemaIntegrado = "Sischef" Then
                SQLControl.ConsultarProdutoPostgres(palavra, ProdutoObj, "frmSelecionarProduto")
            End If


            If Parametros.strSistemaIntegrado = "Sismoura" Then
                SQLControl.ConsultarProduto(palavra, ProdutoObj, "frmSelecionarProduto")
            End If

            btnProdutos.lblNomeProduto.Text = ProdutoObj.Codigo + "-" + ProdutoObj.Nome
            btnProdutos.Show()
        Next

    End Sub

End Class