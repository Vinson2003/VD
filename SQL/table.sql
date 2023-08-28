CREATE TABLE [dbo].[mtBrand](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_by] [nvarchar](128) NULL,
	[created] [datetime] NULL,
	[updated_by] [nvarchar](128) NULL,
	[updated] [datetime] NULL,
	[flg_deleted] [bit] NOT NULL,

	[name] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE mtBrand
ADD CONSTRAINT UC_mtBrand UNIQUE (name);



CREATE TABLE [dbo].[pTransaction](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_by] [nvarchar](128) NULL,
	[created] [datetime] NULL,
	[updated_by] [nvarchar](128) NULL,
	[updated] [datetime] NULL,
	[flg_deleted] [bit] NOT NULL,

	[date] [datetime] NOT NULL,
	[brand_id] [bigint] NOT NULL,

	[result] [nvarchar](255) NULL
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[pTransaction]  WITH CHECK ADD  CONSTRAINT [FK_pTransaction_mtBrand_brandId] FOREIGN KEY([brand_id])
REFERENCES [dbo].[mtBrand] ([id])
ON DELETE CASCADE
GO
