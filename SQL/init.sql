CREATE TABLE [dbo].[mtRole](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[role] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

CREATE TABLE [dbo].[mtPermission](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	
	[description] [nvarchar](200) NOT NULL,
	[display] [nvarchar](200) NOT NULL,
	[seq] [int] NOT NULL,
	[sub_seq] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)


CREATE TABLE [dbo].[pRolePermission](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[role_id] [bigint] NOT NULL,
	[permission_id] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[pRolePermission]  WITH CHECK ADD  CONSTRAINT [FK_pRolePermission_mtRole_role_id] FOREIGN KEY([role_id])
REFERENCES [dbo].[mtRole] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[pRolePermission]  WITH CHECK ADD  CONSTRAINT [FK_pRolePermission_mtPermission_permission_id] FOREIGN KEY([permission_id])
REFERENCES [dbo].[mtPermission] ([id])
ON DELETE CASCADE
GO

CREATE TABLE [dbo].[mtAdmin](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_by] [nvarchar](128) NULL,
	[created] [datetime] NULL,
	[updated_by] [nvarchar](128) NULL,
	[updated] [datetime] NULL,

	[username] [nvarchar](200) NOT NULL,
	[email] [nvarchar](200) NULL,
	[password] [nvarchar](200) NULL,
	[password_salt] [nvarchar](200) NULL,
	[status] [nvarchar](200) NOT NULL,
	[role_id] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE mtAdmin
ADD CONSTRAINT UC_mtAdmin UNIQUE (username);
ALTER TABLE [dbo].[mtAdmin]  WITH CHECK ADD  CONSTRAINT [FK_mtAdmin_mtRole_role_id] FOREIGN KEY([role_id])
REFERENCES [dbo].[mtRole] ([id])
ON DELETE CASCADE
GO
