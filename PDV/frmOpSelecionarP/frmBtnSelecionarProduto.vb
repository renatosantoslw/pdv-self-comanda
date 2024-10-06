Public Class frmBtnSelecionarProduto
    Dim imgNomral As New Bitmap(My.Resources.B_1_E)
    Dim imgClick As New Bitmap(My.Resources.B_2_E)

    Private Sub wait(ByVal millisecondsTimeout As Integer)
        Threading.Thread.Sleep(millisecondsTimeout)
        Application.DoEvents()
    End Sub
    Private Sub frmBtnSelecionarProduto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblNomeProduto.BackColor = Color.Transparent
        lblindice.BackColor = Color.Transparent
        lblNomeProduto.Parent = picbtnProduto
        lblindice.Parent = lblNomeProduto
    End Sub

    Private Sub lblNomeProduto_Click(sender As Object, e As EventArgs) Handles lblNomeProduto.Click
        frmPrincipal.FormularioPdvVenda.ConsultarProduto(Me.Name)
    End Sub

    Private Sub lblNomeProduto_MouseDown(sender As Object, e As MouseEventArgs) Handles lblNomeProduto.MouseDown
        picbtnProduto.Image = imgClick
    End Sub

    Private Sub lblNomeProduto_MouseUp(sender As Object, e As MouseEventArgs) Handles lblNomeProduto.MouseUp
        picbtnProduto.Image = imgNomral
    End Sub
End Class