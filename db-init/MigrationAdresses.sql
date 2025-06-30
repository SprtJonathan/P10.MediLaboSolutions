IF DB_ID('MediLaboSolutions') IS NULL
BEGIN
  CREATE DATABASE [MediLaboSolutions];
END

USE [MediLaboSolutions]
GO
-- Object:  Table [dbo].[Adresses]    Script Date: 16/06/2025 13:55:52
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Adresses' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Adresses](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Numero] [int] NULL,
		[Voie] [nvarchar](200) NULL,
		[Ville] [nvarchar](100) NULL,
		[CodePostal] [nvarchar](20) NULL,
		[Pays] [nvarchar](100) NULL,
	 CONSTRAINT [PK_Adresses] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Adresses] ON

DECLARE @BrooksideVoie NVARCHAR(200) = N'Brookside St';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Adresses] WHERE [Numero] = 1 AND [Voie] = @BrooksideVoie)
INSERT [dbo].[Adresses] ([Id], [Numero], [Voie], [Ville], [CodePostal], [Pays]) VALUES (2, 1, @BrooksideVoie, NULL, NULL, NULL)

IF NOT EXISTS (SELECT 1 FROM [dbo].[Adresses] WHERE [Numero] = 2 AND [Voie] = N'High St')
INSERT [dbo].[Adresses] ([Id], [Numero], [Voie], [Ville], [CodePostal], [Pays]) VALUES (3, 2, N'High St', NULL, NULL, NULL)

IF NOT EXISTS (SELECT 1 FROM [dbo].[Adresses] WHERE [Numero] = 3 AND [Voie] = N'Club Road')
INSERT [dbo].[Adresses] ([Id], [Numero], [Voie], [Ville], [CodePostal], [Pays]) VALUES (4, 3, N'Club Road', NULL, NULL, NULL)

IF NOT EXISTS (SELECT 1 FROM [dbo].[Adresses] WHERE [Numero] = 4 AND [Voie] = N'Valley Dr')
INSERT [dbo].[Adresses] ([Id], [Numero], [Voie], [Ville], [CodePostal], [Pays]) VALUES (5, 4, N'Valley Dr', NULL, NULL, NULL)

IF NOT EXISTS (SELECT 1 FROM [dbo].[Adresses] WHERE [Numero] = 2 AND [Voie] = @BrooksideVoie)
INSERT [dbo].[Adresses] ([Id], [Numero], [Voie], [Ville], [CodePostal], [Pays]) VALUES (22, 2, @BrooksideVoie, NULL, NULL, NULL)

IF NOT EXISTS (SELECT 1 FROM [dbo].[Adresses] WHERE [Numero] IS NULL AND [Voie] IS NULL AND [Ville] IS NULL AND [CodePostal] IS NULL AND [Pays] IS NULL)
INSERT [dbo].[Adresses] ([Id], [Numero], [Voie], [Ville], [CodePostal], [Pays]) VALUES (23, NULL, NULL, NULL, NULL, NULL)

SET IDENTITY_INSERT [dbo].[Adresses] OFF

GO
SET ANSI_PADDING ON
GO
-- Object:  Index [IX_Adresses_Numero_Voie_Ville_CodePostal_Pays]    Script Date: 16/06/2025 13:55:52
CREATE UNIQUE NONCLUSTERED INDEX [IX_Adresses_Numero_Voie_Ville_CodePostal_Pays] ON [dbo].[Adresses]
(
	[Numero] ASC,
	[Voie] ASC,
	[Ville] ASC,
	[CodePostal] ASC,
	[Pays] ASC
)
WHERE ([Numero] IS NOT NULL AND [Voie] IS NOT NULL AND [Ville] IS NOT NULL AND [CodePostal] IS NOT NULL AND [Pays] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
