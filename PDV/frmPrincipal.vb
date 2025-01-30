Imports System.ComponentModel


Public Class frmPrincipal

    Private loc As Point, idle As Date, hidden As Boolean
    Public FormularioPdvVenda As New frmPdvVenda
    Private LogApp As New clsFuncoes.Log


    Public Sub AbrirfrmPdvVenda()

        With FormularioPdvVenda
            .TopLevel = False
            .Parent = Panel1
            .Dock = DockStyle.Fill
            .Left = (Panel1.Width - .Width) / 2
            .Top = (Panel1.Height - .Height) / 2
        End With

        FormularioPdvVenda.Show()

    End Sub
#Region "FUNCOES - SUB PROCEDIMENTOS"
    'CONSTRUTOR
    Public Sub New()

        InitializeComponent()

    End Sub

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
    'OCUTA FORMULARIO E FECHA APLICACAO
    Public Sub CloseFormDegrade()
        For i = 0 To 30
            wait(50)
            Me.Opacity = Me.Opacity - 0.1
            If Me.Opacity <= 0 Then
                Exit For
            End If
        Next
    End Sub

#End Region

    'EVENTOS FORM PRINCIPAL (LOAD - KEYDOWN - CLOSING)
#Region "EVENTOS FORMULARIO PRINCIPAL"
    'LOAD
    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "PDVSelfComanda Add-in - Build: 29/01/2025 :: 11:11"
        Me.FormBorderStyle = FormBorderStyle.None
        'Me.FormBorderStyle = FormBorderStyle.Sizable
        'Me.WindowState = FormWindowState.Normal
        Me.WindowState = FormWindowState.Maximized

        'tmrMouse.Enabled = True
        'tmrMouse.Start()

        AbrirfrmPdvVenda()
    End Sub
    'KEYDOWN
    Private Sub frmPrincipal_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.Alt And e.KeyCode = Keys.Enter Then
            If Me.FormBorderStyle = FormBorderStyle.None Then
                Me.FormBorderStyle = FormBorderStyle.Sizable
                Me.WindowState = FormWindowState.Normal
                Me.ShowIcon = True
                Me.Icon = My.Resources.icon_icone
            Else
                Me.FormBorderStyle = FormBorderStyle.None
                Me.WindowState = FormWindowState.Maximized
            End If
        End If

        If e.KeyCode = Keys.Escape Then
            '     frmConfig.frmPdvVendaAberto = True
            frmConfig.ShowDialog()
        End If


        If e.KeyCode = Keys.End Then

            Try
                Dim Dialogo As DialogResult = MessageBox.Show("Deseja sair da aplicação?", My.Application.Info.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Dialogo = DialogResult.Yes Then
                    FormularioPdvVenda.SalvarPosicao_Split()
                    FormularioPdvVenda.Close()
                    FormularioPdvVenda.Dispose()
                    CloseFormDegrade()


                    Application.Exit()
                End If

            Catch ex As Exception
                LogApp.GerarLogErro(ex, "frmPrincipal", "frmPrincipal_KeyDown")
            End Try

        End If


        If e.Control And e.KeyCode = Keys.C Then

        End If


    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub
    'CLOSING
    Private Sub frmPrincipal_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        Try
            Dim Dialogo As DialogResult = MessageBox.Show("Deseja sair da aplicação?", My.Application.Info.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Dialogo = DialogResult.Yes Then
                FormularioPdvVenda.SalvarPosicao_Split()
                FormularioPdvVenda.Close()
                FormularioPdvVenda.Dispose()
                CloseFormDegrade()


                frmPdvVenda.tmrSolicitaBalanca.Stop()
                frmPdvVenda.tmrSolicitar.Stop()
                frmPdvVenda.tmrSolicitaBalanca.Enabled = False
                frmPdvVenda.tmrSolicitar.Enabled = False
                frmPdvVenda.FecharPortaSerial()
                Application.Exit()
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
            LogApp.GerarLogErro(ex, "frmPrincipal", "Private Sub frmPrincipal_Closing")
        End Try


    End Sub




#End Region

    Private Sub tmrMouse_Tick(sender As Object, e As EventArgs) Handles tmrMouse.Tick
        'OCUTAR/EXIBIR MOUSE
        If loc <> Cursor.Position Then
            If hidden Then
                Cursor.Show()
                hidden = False
            End If
            loc = Cursor.Position
            idle = Date.Now
        ElseIf Not hidden AndAlso (Date.Now - idle).TotalSeconds > 3 Then

            Cursor.Hide()
            hidden = True
        End If

        'If hidden = True AndAlso (Date.Now - idle).TotalSeconds > 300 Then
        'LogApp.LogEntry("Finalizando Aplicação:", "tmrMouse - Tick")
        'Application.Exit()
        'End If
    End Sub

    Private Sub frmPrincipal_Shown(sender As Object, e As EventArgs) Handles Me.Shown



    End Sub
End Class
