<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRelatorio
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRelatorio))
        Me.rptVizualizar = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'rptVizualizar
        '
        Me.rptVizualizar.ActiveViewIndex = -1
        Me.rptVizualizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rptVizualizar.Cursor = System.Windows.Forms.Cursors.Default
        Me.rptVizualizar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptVizualizar.Location = New System.Drawing.Point(0, 0)
        Me.rptVizualizar.Name = "rptVizualizar"
        Me.rptVizualizar.ShowCloseButton = False
        Me.rptVizualizar.ShowExportButton = False
        Me.rptVizualizar.ShowGroupTreeButton = False
        Me.rptVizualizar.ShowLogo = False
        Me.rptVizualizar.ShowParameterPanelButton = False
        Me.rptVizualizar.ShowRefreshButton = False
        Me.rptVizualizar.ShowZoomButton = False
        Me.rptVizualizar.Size = New System.Drawing.Size(1183, 816)
        Me.rptVizualizar.TabIndex = 0
        Me.rptVizualizar.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'frmRelatorio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1183, 816)
        Me.Controls.Add(Me.rptVizualizar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frmRelatorio"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Relatorio"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rptVizualizar As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
