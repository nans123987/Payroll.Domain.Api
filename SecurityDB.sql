--create a db with name Security
USE [Security]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/27/2022 1:36:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] PRIMARY KEY NOT NULL,
	[UserId] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Role] [int] NOT NULL,
	[PasswordHash] [varchar](max) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[ClientId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

GO
INSERT [dbo].[Users] ([Id], [UserId], [FirstName], [LastName], [Role], [PasswordHash], [Username], [ClientId]) VALUES (N'd1afa786-816d-4990-8a78-bdf7681a7854', 3, N'Darren', N'Employee', 2, N'$2y$10$7x0y01gTO8yZ7R4ktbDW5uQDKnjoJPZBBTikLH2GmK4QR4JASop.G', N'Darren', 301190)
GO
INSERT [dbo].[Users] ([Id], [UserId], [FirstName], [LastName], [Role], [PasswordHash], [Username], [ClientId]) VALUES (N'3691a1d5-d57f-456f-90dc-cdc85c483f29', 2, N'Emily', N'Employee', 2, N'$2y$10$7x0y01gTO8yZ7R4ktbDW5uQDKnjoJPZBBTikLH2GmK4QR4JASop.G', N'Emily', 301190)
GO
INSERT [dbo].[Users] ([Id], [UserId], [FirstName], [LastName], [Role], [PasswordHash], [Username], [ClientId]) VALUES (N'636463a7-245d-4ff8-99f0-d2bf96ba2e5c', 4, N'AJ', N'Employee', 2, N'$2y$10$7x0y01gTO8yZ7R4ktbDW5uQDKnjoJPZBBTikLH2GmK4QR4JASop.G', N'AJ', 301190)
GO
INSERT [dbo].[Users] ([Id], [UserId], [FirstName], [LastName], [Role], [PasswordHash], [Username], [ClientId]) VALUES (N'687a067a-b49c-4b0b-b247-ffd2e01a3d53', 1, N'Arron', N'Admin', 1, N'$2y$10$OL0WqCbv1ENqS1TBmziWVuXsO.k2Nd8stIxfYP8xEPq/dRagx9U2e', N'Arron', 301190)
GO
/****** Object:  Index [NC_Idx_UserId]    Script Date: 11/27/2022 1:36:04 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [NC_Idx_UserId] ON [dbo].[Users]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NC_idx_UserName]    Script Date: 11/27/2022 1:36:04 PM ******/
CREATE NONCLUSTERED INDEX [NC_idx_UserName] ON [dbo].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
