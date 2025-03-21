USE [AuthorBooksb]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 20.02.2025 8:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 20.02.2025 8:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[AuthorId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Authors] ON 
INSERT [dbo].[Authors] ([Id], [Name]) VALUES (1, N'Достоевский Федор')
INSERT [dbo].[Authors] ([Id], [Name]) VALUES (2, N'Кафка Франц')
INSERT [dbo].[Authors] ([Id], [Name]) VALUES (3, N'Фицджеральд Фрэнсис Скотт')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [AuthorId]) VALUES (1, N'Преступление и наказание', 1)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId]) VALUES (2, N'Идиот', 1)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId]) VALUES (3, N'Превращение', 2)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId]) VALUES (4, N'Процесс', 2)
INSERT [dbo].[Books] ([Id], [Title], [AuthorId]) VALUES (5, N'Великий Гэтсби', 3)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Authors] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Authors]
GO
