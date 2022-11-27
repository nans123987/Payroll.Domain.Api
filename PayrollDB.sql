If(db_id(N'Payroll') IS NULL)
BEGIN
	CREATE DATABASE Payroll
END

USE Payroll
GO
/****** Object:  Table [dbo].[RelationshipType]    Script Date: 11/27/2022 1:21:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RelationshipType]') AND type in (N'U'))
DROP TABLE [dbo].[RelationshipType]
GO
/****** Object:  Table [dbo].[EmployeeBenefits]    Script Date: 11/27/2022 1:21:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeBenefits]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeeBenefits]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/27/2022 1:21:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[Dependents]    Script Date: 11/27/2022 1:21:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dependents]') AND type in (N'U'))
DROP TABLE [dbo].[Dependents]
GO
/****** Object:  Table [dbo].[BenefitPlan]    Script Date: 11/27/2022 1:21:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BenefitPlan]') AND type in (N'U'))
DROP TABLE [dbo].[BenefitPlan]
GO
/****** Object:  Table [dbo].[BenefitPlan]    Script Date: 11/27/2022 1:21:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BenefitPlan](
	[Id] [uniqueidentifier] NOT NULL,
	[BenefitPlanProviderUid] [uniqueidentifier] NOT NULL,
	[BenefitPlanName] [varchar](100) NOT NULL,
	[BenefitDeductionCode] [varchar](10) NOT NULL,
	[EmployeeBenefitDeductionAmount] [decimal](10, 2) NOT NULL,
	[DependentBenefitDeductionAmount] [decimal](10, 2) NOT NULL,
	[ClientId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dependents]    Script Date: 11/27/2022 1:21:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dependents](
	[DependentId] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Employee] [uniqueidentifier] NULL,
	[Relationship] [int] NULL,
	[ClientId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/27/2022 1:21:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [int] NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[ClientId] [int] NULL,
	[Active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeBenefits]    Script Date: 11/27/2022 1:21:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeBenefits](
	[Id] [uniqueidentifier] NOT NULL,
	[ClientId] [int] NOT NULL,
	[Employee] [uniqueidentifier] NOT NULL,
	[BenefitPlan] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EmployeeBenefits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RelationshipType]    Script Date: 11/27/2022 1:21:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationshipType](
	[Id] [int] NOT NULL,
	[RelationshipType] [varchar](100) NULL,
 CONSTRAINT [PK_RelationshipType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BenefitPlan] ([Id], [BenefitPlanProviderUid], [BenefitPlanName], [BenefitDeductionCode], [EmployeeBenefitDeductionAmount], [DependentBenefitDeductionAmount], [ClientId]) VALUES (N'b14bd2ec-4131-4ab9-9b50-2fa738b3bef5', N'688846f7-d02e-4f5d-8040-238090671c26', N'United Health Care - PPO', N'Health', CAST(1000.00 AS Decimal(10, 2)), CAST(500.00 AS Decimal(10, 2)), 301190)
GO
INSERT [dbo].[BenefitPlan] ([Id], [BenefitPlanProviderUid], [BenefitPlanName], [BenefitDeductionCode], [EmployeeBenefitDeductionAmount], [DependentBenefitDeductionAmount], [ClientId]) VALUES (N'c58cc50a-8d87-48e7-b3ae-f84fa61ca3c8', N'150971b3-df7d-455b-8e11-e554336cbfc2', N'Athena - PPO', N'Health', CAST(1000.00 AS Decimal(10, 2)), CAST(500.00 AS Decimal(10, 2)), 301190)
GO
INSERT [dbo].[Dependents] ([DependentId], [FirstName], [LastName], [Employee], [Relationship], [ClientId]) VALUES (N'9f1056d7-83d9-45d6-ba5c-cf7c4b3efff9', N'Rocky', N'EmilyC', N'25700e6b-5d64-41e2-9670-06d31bf338b6', 1, 301190)
GO
INSERT [dbo].[Dependents] ([DependentId], [FirstName], [LastName], [Employee], [Relationship], [ClientId]) VALUES (N'fde2eebc-3e89-4e33-b4b2-4222431c461f', N'Mike', N'EmilyS', N'25700e6b-5d64-41e2-9670-06d31bf338b6', 2, 301190)
GO
INSERT [dbo].[Dependents] ([DependentId], [FirstName], [LastName], [Employee], [Relationship], [ClientId]) VALUES (N'4cfebd62-50a6-4afc-af57-5ab7704162a0', N'Erick', N'DarrenC', N'606200aa-b1ea-4d86-802a-32a7c139bf97', 1, 301190)
GO
INSERT [dbo].[Dependents] ([DependentId], [FirstName], [LastName], [Employee], [Relationship], [ClientId]) VALUES (N'8c1f7ce3-95e8-4912-9135-b96428f62643', N'Anthony', N'DarrenP', N'606200aa-b1ea-4d86-802a-32a7c139bf97', 3, 301190)
GO
INSERT [dbo].[Dependents] ([DependentId], [FirstName], [LastName], [Employee], [Relationship], [ClientId]) VALUES (N'02bb103c-e912-4c8f-9aaf-82d455ef1ab2', N'Mark', N'AJS', N'8ec5e1e1-0153-400c-8000-d976c98ab0f0', 2, 301190)
GO
INSERT [dbo].[Dependents] ([DependentId], [FirstName], [LastName], [Employee], [Relationship], [ClientId]) VALUES (N'2cf5f30d-10d9-47e0-a954-f73074734f71', N'Steven', N'AJP', N'8ec5e1e1-0153-400c-8000-d976c98ab0f0', 3, 301190)
GO
INSERT [dbo].[Employee] ([Id], [EmployeeId], [FirstName], [LastName], [ClientId], [Active]) VALUES (N'57469f28-ce50-4978-8eb7-548f4ae7ae3f', 1001, N'Arron', N'Admin', 301190, 1)
GO
INSERT [dbo].[Employee] ([Id], [EmployeeId], [FirstName], [LastName], [ClientId], [Active]) VALUES (N'25700e6b-5d64-41e2-9670-06d31bf338b6', 1002, N'Emily', N'Employee', 301190, 1)
GO
INSERT [dbo].[Employee] ([Id], [EmployeeId], [FirstName], [LastName], [ClientId], [Active]) VALUES (N'606200aa-b1ea-4d86-802a-32a7c139bf97', 1003, N'Darren', N'Employee', 301190, 1)
GO
INSERT [dbo].[Employee] ([Id], [EmployeeId], [FirstName], [LastName], [ClientId], [Active]) VALUES (N'8ec5e1e1-0153-400c-8000-d976c98ab0f0', 1004, N'AJ', N'Employee', 301190, 1)
GO
INSERT [dbo].[EmployeeBenefits] ([Id], [ClientId], [Employee], [BenefitPlan]) VALUES (N'806382ee-8754-4e7a-818e-55a432d434e3', 301190, N'606200aa-b1ea-4d86-802a-32a7c139bf97', N'b14bd2ec-4131-4ab9-9b50-2fa738b3bef5')
GO
INSERT [dbo].[EmployeeBenefits] ([Id], [ClientId], [Employee], [BenefitPlan]) VALUES (N'6f2adb4b-d35b-436b-a81c-b497a1cc8768', 301190, N'8ec5e1e1-0153-400c-8000-d976c98ab0f0', N'c58cc50a-8d87-48e7-b3ae-f84fa61ca3c8')
GO
INSERT [dbo].[EmployeeBenefits] ([Id], [ClientId], [Employee], [BenefitPlan]) VALUES (N'92ab5d1e-5aba-44df-b5cd-d4fe6aba5254', 301190, N'25700e6b-5d64-41e2-9670-06d31bf338b6', N'b14bd2ec-4131-4ab9-9b50-2fa738b3bef5')
GO
INSERT [dbo].[RelationshipType] ([Id], [RelationshipType]) VALUES (1, N'Child')
GO
INSERT [dbo].[RelationshipType] ([Id], [RelationshipType]) VALUES (2, N'Spouse')
GO
INSERT [dbo].[RelationshipType] ([Id], [RelationshipType]) VALUES (3, N'Parent')
GO
/****** Object:  Index [NC_Idx_BPProviderId_ClientId]    Script Date: 11/27/2022 1:21:11 PM ******/
CREATE NONCLUSTERED INDEX [NC_Idx_BPProviderId_ClientId] ON [dbo].[BenefitPlan]
(
	[BenefitPlanProviderUid] ASC,
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NC_Idx_ClientId]    Script Date: 11/27/2022 1:21:11 PM ******/
CREATE NONCLUSTERED INDEX [NC_Idx_ClientId] ON [dbo].[BenefitPlan]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NC_Idx_Employee]    Script Date: 11/27/2022 1:21:11 PM ******/
ALTER TABLE [dbo].[Dependents] ADD  CONSTRAINT [NC_Idx_Employee] PRIMARY KEY NONCLUSTERED 
(
	[DependentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NC_Idx_ClientId]    Script Date: 11/27/2022 1:21:11 PM ******/
CREATE NONCLUSTERED INDEX [NC_Idx_ClientId] ON [dbo].[Dependents]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NC_Idx_EmployeeId]    Script Date: 11/27/2022 1:21:11 PM ******/
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [NC_Idx_EmployeeId] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NC_Idx_ClientId]    Script Date: 11/27/2022 1:21:11 PM ******/
CREATE NONCLUSTERED INDEX [NC_Idx_ClientId] ON [dbo].[Employee]
(
	[ClientId] ASC,
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NC_Idx_ClientId]    Script Date: 11/27/2022 1:21:11 PM ******/
CREATE NONCLUSTERED INDEX [NC_Idx_ClientId] ON [dbo].[EmployeeBenefits]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-20221126-124119]    Script Date: 11/27/2022 1:21:11 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20221126-124119] ON [dbo].[EmployeeBenefits]
(
	[ClientId] ASC,
	[Employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
