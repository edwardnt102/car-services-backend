TRUNCATE TABLE [dbo].DataType
SET IDENTITY_INSERT [dbo].DataType ON 

INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (1 ,N'Customers')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (2 ,N'Subscriptions')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (3 ,N'Cars')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (4 ,N'Prices')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (5 ,N'Workers')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (6 ,N'Slots')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (7 ,N'Jobs')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (8 ,N'Columns')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (9 ,N'Zones')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (10 ,N'Teams')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (11 ,N'Withdraws')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (12 ,N'Basements')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (13 ,N'Places')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (14 ,N'Rules')
INSERT INTO [dbo].[DataType] ([Id], [Title]) VALUES (15 ,N'Staffs')

SET IDENTITY_INSERT [dbo].DataType OFF
GO

SELECT * FROM [dbo].DataType