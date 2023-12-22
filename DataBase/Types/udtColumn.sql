IF NOT EXISTS(SELECT 1 FROM sys.types WHERE name = 'udtColumn' AND is_table_type = 1 AND SCHEMA_ID('dbo') = schema_id)
BEGIN
CREATE TYPE dbo.udtColumn AS TABLE(
	 Id	bigint	NULL
	,History	nvarchar(256)	NULL
	,Chat	nvarchar(256)	NULL
	,CreatedBy	nvarchar(50)	NULL
	,CreatedDate	datetime	NULL
	,ModifiedBy	nvarchar(50)	NULL
	,ModifiedDate	datetime	NULL
	,IsDeleted	bit	NULL
	,Title	nvarchar(256)	NULL
	,Subtitle	nvarchar(256)	NULL
	,[Description]	nvarchar(256)	NULL
	,BasementId	bigint	NULL
	,AttachmentFileOriginalName	nvarchar(MAX)	NULL
	,AttachmentFileReName	nvarchar(MAX)	NULL
)
END
GO