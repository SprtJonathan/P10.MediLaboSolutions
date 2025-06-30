IF DB_ID('MediLaboSolutions') IS NULL
BEGIN
  CREATE DATABASE [MediLaboSolutions];
END

USE [MediLaboSolutions]
GO
-- Object:  Table [dbo].[Patients]    Script Date: 16/06/2025 13:55:52
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](100) NOT NULL,
	[Prenom] [nvarchar](100) NOT NULL,
	[DateNaissance] [datetime2](7) NOT NULL,
	[Genre] [tinyint] NOT NULL,
	[Telephone] [bigint] NULL,
	[AdresseId] [int] NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Patients] ON

DECLARE @PrenomTest NVARCHAR(100) = N'Test';

IF NOT EXISTS (
	SELECT 1 FROM [dbo].[Patients]
	WHERE [Nom] = N'TestNone' AND [Prenom] = @PrenomTest AND [DateNaissance] = '1966-12-31' AND [Genre] = 2 AND [Telephone] = 1002223333 AND [AdresseId] = 2
)
INSERT [dbo].[Patients] ([Id], [Nom], [Prenom], [DateNaissance], [Genre], [Telephone], [AdresseId])
VALUES (2, N'TestNone', @PrenomTest, '1966-12-31', 2, 1002223333, 2)

IF NOT EXISTS (
	SELECT 1 FROM [dbo].[Patients]
	WHERE [Nom] = N'TestBorderline' AND [Prenom] = @PrenomTest AND [DateNaissance] = '1945-06-24' AND [Genre] = 1 AND [Telephone] = 2003334444 AND [AdresseId] = 3
)
INSERT [dbo].[Patients] ([Id], [Nom], [Prenom], [DateNaissance], [Genre], [Telephone], [AdresseId])
VALUES (3, N'TestBorderline', @PrenomTest, '1945-06-24', 1, 2003334444, 3)

IF NOT EXISTS (
	SELECT 1 FROM [dbo].[Patients]
	WHERE [Nom] = N'TestInDanger' AND [Prenom] = @PrenomTest AND [DateNaissance] = '2004-06-24' AND [Genre] = 1 AND [Telephone] = 3004445555 AND [AdresseId] = 4
)
INSERT [dbo].[Patients] ([Id], [Nom], [Prenom], [DateNaissance], [Genre], [Telephone], [AdresseId])
VALUES (4, N'TestInDanger', @PrenomTest, '2004-06-24', 1, 3004445555, 4)

IF NOT EXISTS (
	SELECT 1 FROM [dbo].[Patients]
	WHERE [Nom] = N'TestEarlyOnset' AND [Prenom] = @PrenomTest AND [DateNaissance] = '2002-06-28' AND [Genre] = 2 AND [Telephone] = 4005556666 AND [AdresseId] = 5
)
INSERT [dbo].[Patients] ([Id], [Nom], [Prenom], [DateNaissance], [Genre], [Telephone], [AdresseId])
VALUES (5, N'TestEarlyOnset', @PrenomTest, '2002-06-28', 2, 4005556666, 5)

SET IDENTITY_INSERT [dbo].[Patients] OFF

GO
-- Object:  Index [IX_Patients_AdresseId]    Script Date: 16/06/2025 13:55:52
CREATE NONCLUSTERED INDEX [IX_Patients_AdresseId] ON [dbo].[Patients]
(
	[AdresseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Adresses_AdresseId] FOREIGN KEY([AdresseId])
REFERENCES [dbo].[Adresses] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Adresses_AdresseId]
GO
