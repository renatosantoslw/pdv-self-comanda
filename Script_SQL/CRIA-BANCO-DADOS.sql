CREATE DATABASE [PDVSELF]
GO
CREATE TABLE [PDVSELF].[dbo].[PRODUTO](
	[CODIGO] [varchar](50) NULL,
	[CODIGO_BARRA] [varchar](50) NULL,
	[UNIDADE] [varchar](50) NULL,
	[PRECO_PRODUTO] [varchar](50) NULL,
	[NOME] [varchar](50) NULL
) ON [PRIMARY]
GO
INSERT INTO [PDVSELF].[dbo].[PRODUTO] (CODIGO,CODIGO_BARRA,UNIDADE,PRECO_PRODUTO,NOME) VALUES (1,1,'KG',10,'SELF SERVICE')
GO
INSERT INTO [PDVSELF].[dbo].[PRODUTO] (CODIGO,CODIGO_BARRA,UNIDADE,PRECO_PRODUTO,NOME) VALUES (2,3,'KG',15,'CHURRASCO')
GO
INSERT INTO [PDVSELF].[dbo].[PRODUTO] (CODIGO,CODIGO_BARRA,UNIDADE,PRECO_PRODUTO,NOME) VALUES (3,3,'KG',15,'COMERCIAL')
GO
CREATE TABLE [PDVSELF].[dbo].[Venda_Espera](
	[Vendedor] [int] NULL,
	[Item] [smallint] NULL,
	[Produto] [int] NULL,
	[Quantidade] [decimal](18, 3) NULL,
	[Unitario] [decimal](18, 3) NULL,
	[Total] [decimal](18, 3) NULL,
	[Cancelado] [varchar](1) NULL,
	[Preco_Custo] [decimal](18, 3) NULL,
	[Unidade] [varchar](10) NULL,
	[Comissao] [decimal](18, 3) NULL,
	[Codigo] [varchar](100) NULL,
	[Observacao] [text] NULL,
	[Cliente] [bigint] NULL,
	[Mesa] [decimal](6, 0) NULL,
	[Data] [datetime] NULL,
	[Numero_Entrega] [bigint] NULL,
	[Finalizar_Atendimento] [varchar](1) NULL,
	[Taxa_Tributaria] [smallint] NULL,
	[Dependente] [int] NULL,
	[Texto_Final] [text] NULL,
	[Fator2] [decimal](18, 3) NULL,
	[Fator3] [decimal](18, 3) NULL,
	[Atendimento] [int] NULL,
	[Codigo_OS] [int] NULL,
	[Numero_Chamada] [int] NULL,
	[Codigo_Troca] [bigint] NULL,
	[Desconto_Troca] [decimal](18, 3) NULL,
	[Codigo_Pocket] [int] NULL,
	[PDV] [smallint] NULL,
	[Valor_Desconto_Item] [decimal](18, 3) NULL,
	[Valor_Acrescimo_Item] [decimal](18, 3) NULL,
	[PreVenda] [varchar](13) NULL,
	[Mesa_Anterior] [numeric](9, 0) NULL,
	[Data_Abertura] [datetime] NULL,
	[Cupom_Conferencia_COO] [int] NULL,
	[Cupom_Conferencia_Numero_ECF] [int] NULL,
	[Impresso] [varchar](1) NULL,
	[entrega] [varchar](1) NULL,
	[Lote] [varchar](100) NULL,
	[Tipo_Receituario_Medicamento] [varchar](1) NULL,
	[Numero_Notificacao_Medicamento] [varchar](10) NULL,
	[Data_Prescricao_Medicamento] [datetime] NULL,
	[Nome_Prescritor] [varchar](100) NULL,
	[Numero_Registro_Profissional] [varchar](30) NULL,
	[Conselho_Profissional] [varchar](4) NULL,
	[UF_Conselho] [varchar](2) NULL,
	[Uso_Medicamento] [varchar](1) NULL,
	[Nome_Comprador] [varchar](100) NULL,
	[Orgao_Expedidor] [varchar](10) NULL,
	[UF_Emissao_Documento] [varchar](2) NULL,
	[Tipo_Documento] [varchar](2) NULL,
	[Numero_Documento] [varchar](30) NULL,
	[Impresso_Comanda] [varchar](1) NULL,
	[Epharma_Comanda] [int] NULL,
	[Empresa] [int] NULL,
	[Aberto_Micro] [varchar](60) NULL,
	[Aberto_Usuario] [int] NULL,
	[Aberto_Data] [datetime] NULL,
	[Numero_Mesa] [numeric](18, 0) NULL,
	[Cod_Seq] [bigint] IDENTITY(1,1) NOT NULL,
	[Valor_Desconto_Item_Porcentagem] [numeric](18, 5) NOT NULL,
	[Codigo_Fornecedor] [int] NULL,
	[Observacao_Fechamento] [text] NULL,
	[Venda_Controlado] [int] NULL,
	[Cod_Abastecimento] [numeric](9, 0) NULL,
	[Deposito] [int] NULL,
	[Manipulacao_Orcamento] [int] NULL,
	[Manipulacao_Formula] [int] NULL,
	[Venda_FP_IS_FP] [char](1) NULL,
	[Venda_FP_Nome_Participante] [varchar](50) NULL,
	[Venda_FP_CPF_Participante] [varchar](14) NULL,
	[Venda_FP_CRM_Prescritor] [varchar](10) NULL,
	[Venda_FP_UF_Prescritor] [varchar](2) NULL,
	[Venda_FP_Data_Emissao_Prescricao] [varchar](12) NULL,
	[Venda_FP_Qtde_Prescrita] [decimal](18, 5) NULL,
	[Venda_FP_Qtde_Solicitada] [decimal](18, 5) NULL,
	[Cardapio_Online_Pedido] [bigint] NULL,
	[Cardapio_Online_Mesa] [bigint] NULL,
	[Transacao_BIG] [varchar](50) NULL,
	[ConvenioBIG] [varchar](1) NULL,
	[Programa_Desconto] [varchar](1) NULL,
	[assinatura_digital_registro] [varchar](300) NULL,
	[Bico] [decimal](18, 0) NULL,
	[Bomba] [decimal](18, 0) NULL,
	[Cliente_Entrega] [bigint] NULL,
	[Comanda_Microterminal] [varchar](1) NULL,
	[Servico_Tela_Fechamento] [varchar](1) NULL,
	[Venda_iFarma] [varchar](1) NULL,
	[Desconto_Fechamento] [decimal](18, 5) NULL,
	[KIT] [varchar](1) NULL,
	[impresso_ambiente] [varchar](1) NULL,
	[isVendaDiferenciada] [varchar](1) NULL,
	[Mesa_Originou_Pedido] [int] NULL,
	[IdMouraBurger_Venda] [bigint] NULL,
	[Id_Venda_Controlado] [bigint] NULL,
	[PV] [varchar](10) NULL,
	[Verificado_Qtde_Promocao] [varchar](1) NULL,
	[Fator] [decimal](18, 5) NULL,
	[Venda_FP_Autorizacao] [varchar](50) NULL,
	[Comanda_Origem] [varchar](100) NULL,
	[IFood] [bigint] NULL,
	[EPC] [varchar](24) NULL,
	[Monitor] [char](1) NULL,
	[Captura] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Seq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [PDVSELF].[dbo].[Venda_Espera_SelfComanda](
	[Vendedor] [int] NULL,
	[Item] [smallint] NULL,
	[Produto] [int] NULL,
	[Quantidade] [decimal](18, 3) NULL,
	[Unitario] [decimal](18, 3) NULL,
	[Total] [decimal](18, 3) NULL,
	[Cancelado] [varchar](1) NULL,
	[Preco_Custo] [decimal](18, 3) NULL,
	[Unidade] [varchar](10) NULL,
	[Comissao] [decimal](18, 3) NULL,
	[Codigo] [varchar](100) NULL,
	[Observacao] [text] NULL,
	[Cliente] [bigint] NULL,
	[Mesa] [decimal](6, 0) NULL,
	[Data] [datetime] NULL,
	[Numero_Entrega] [bigint] NULL,
	[Finalizar_Atendimento] [varchar](1) NULL,
	[Taxa_Tributaria] [smallint] NULL,
	[Dependente] [int] NULL,
	[Texto_Final] [text] NULL,
	[Fator2] [decimal](18, 3) NULL,
	[Fator3] [decimal](18, 3) NULL,
	[Atendimento] [int] NULL,
	[Codigo_OS] [int] NULL,
	[Numero_Chamada] [int] NULL,
	[Codigo_Troca] [bigint] NULL,
	[Desconto_Troca] [decimal](18, 3) NULL,
	[Codigo_Pocket] [int] NULL,
	[PDV] [smallint] NULL,
	[Valor_Desconto_Item] [decimal](18, 3) NULL,
	[Valor_Acrescimo_Item] [decimal](18, 3) NULL,
	[PreVenda] [varchar](13) NULL,
	[Mesa_Anterior] [numeric](9, 0) NULL,
	[Data_Abertura] [datetime] NULL,
	[Cupom_Conferencia_COO] [int] NULL,
	[Cupom_Conferencia_Numero_ECF] [int] NULL,
	[Impresso] [varchar](1) NULL,
	[entrega] [varchar](1) NULL,
	[Lote] [varchar](100) NULL,
	[Tipo_Receituario_Medicamento] [varchar](1) NULL,
	[Numero_Notificacao_Medicamento] [varchar](10) NULL,
	[Data_Prescricao_Medicamento] [datetime] NULL,
	[Nome_Prescritor] [varchar](100) NULL,
	[Numero_Registro_Profissional] [varchar](30) NULL,
	[Conselho_Profissional] [varchar](4) NULL,
	[UF_Conselho] [varchar](2) NULL,
	[Uso_Medicamento] [varchar](1) NULL,
	[Nome_Comprador] [varchar](100) NULL,
	[Orgao_Expedidor] [varchar](10) NULL,
	[UF_Emissao_Documento] [varchar](2) NULL,
	[Tipo_Documento] [varchar](2) NULL,
	[Numero_Documento] [varchar](30) NULL,
	[Impresso_Comanda] [varchar](1) NULL,
	[Epharma_Comanda] [int] NULL,
	[Empresa] [int] NULL,
	[Aberto_Micro] [varchar](60) NULL,
	[Aberto_Usuario] [int] NULL,
	[Aberto_Data] [datetime] NULL,
	[Numero_Mesa] [numeric](18, 0) NULL,
	[Cod_Seq] [bigint] IDENTITY(1,1) NOT NULL,
	[Valor_Desconto_Item_Porcentagem] [numeric](18, 5) NOT NULL,
	[Codigo_Fornecedor] [int] NULL,
	[Observacao_Fechamento] [text] NULL,
	[Venda_Controlado] [int] NULL,
	[Cod_Abastecimento] [numeric](9, 0) NULL,
	[Deposito] [int] NULL,
	[Manipulacao_Orcamento] [int] NULL,
	[Manipulacao_Formula] [int] NULL,
	[Venda_FP_IS_FP] [char](1) NULL,
	[Venda_FP_Nome_Participante] [varchar](50) NULL,
	[Venda_FP_CPF_Participante] [varchar](14) NULL,
	[Venda_FP_CRM_Prescritor] [varchar](10) NULL,
	[Venda_FP_UF_Prescritor] [varchar](2) NULL,
	[Venda_FP_Data_Emissao_Prescricao] [varchar](12) NULL,
	[Venda_FP_Qtde_Prescrita] [decimal](18, 5) NULL,
	[Venda_FP_Qtde_Solicitada] [decimal](18, 5) NULL,
	[Cardapio_Online_Pedido] [bigint] NULL,
	[Cardapio_Online_Mesa] [bigint] NULL,
	[Transacao_BIG] [varchar](50) NULL,
	[ConvenioBIG] [varchar](1) NULL,
	[Programa_Desconto] [varchar](1) NULL,
	[assinatura_digital_registro] [varchar](300) NULL,
	[Bico] [decimal](18, 0) NULL,
	[Bomba] [decimal](18, 0) NULL,
	[Cliente_Entrega] [bigint] NULL,
	[Comanda_Microterminal] [varchar](1) NULL,
	[Servico_Tela_Fechamento] [varchar](1) NULL,
	[Venda_iFarma] [varchar](1) NULL,
	[Desconto_Fechamento] [decimal](18, 5) NULL,
	[KIT] [varchar](1) NULL,
	[impresso_ambiente] [varchar](1) NULL,
	[isVendaDiferenciada] [varchar](1) NULL,
	[Mesa_Originou_Pedido] [int] NULL,
	[IdMouraBurger_Venda] [bigint] NULL,
	[Id_Venda_Controlado] [bigint] NULL,
	[PV] [varchar](10) NULL,
	[Verificado_Qtde_Promocao] [varchar](1) NULL,
	[Fator] [decimal](18, 5) NULL,
	[Venda_FP_Autorizacao] [varchar](50) NULL,
	[Comanda_Origem] [varchar](100) NULL,
	[IFood] [bigint] NULL,
	[EPC] [varchar](24) NULL,
	[Monitor] [char](1) NULL,
	[Captura] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Seq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [PDVSELF].[dbo].[Config_SelfComanda](
	[Codigo_Ultima_Comanda] [bigint] NOT NULL,
 CONSTRAINT [PK_Config_SelfComanda] PRIMARY KEY CLUSTERED 
(
	[Codigo_Ultima_Comanda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [PDVSELF].[dbo].[Config_SelfComanda] (Codigo_Ultima_Comanda) VALUES (0)
