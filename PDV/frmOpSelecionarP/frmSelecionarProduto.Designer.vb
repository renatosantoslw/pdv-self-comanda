<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelecionarProduto
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelecionarProduto))
        Me.flpOP = New System.Windows.Forms.FlowLayoutPanel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'flpOP
        '
        Me.flpOP.AutoScroll = True
        Me.flpOP.AutoScrollMargin = New System.Drawing.Size(1, 1)
        Me.flpOP.AutoScrollMinSize = New System.Drawing.Size(1, 1)
        Me.flpOP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpOP.Location = New System.Drawing.Point(0, 0)
        Me.flpOP.Margin = New System.Windows.Forms.Padding(4)
        Me.flpOP.Name = "flpOP"
        Me.flpOP.Size = New System.Drawing.Size(1325, 518)
        Me.flpOP.TabIndex = 6
        '
        'frmSelecionarProduto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1325, 518)
        Me.Controls.Add(Me.flpOP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "frmSelecionarProduto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Selecionar Produto Opcional."
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents flpOP As FlowLayoutPanel
    Friend WithEvents ToolTip1 As ToolTip
End Class
