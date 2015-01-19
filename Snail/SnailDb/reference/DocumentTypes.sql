PRINT 'Inserting document types'

DECLARE @DocumentTypesReference TABLE ([DocumentType] INT NOT NULL,	[DocumentTypeName] NVARCHAR(255) NOT NULL)

INSERT INTO @DocumentTypesReference VALUES (1, 'Quote')
INSERT INTO @DocumentTypesReference VALUES (2, 'Bill of Lading')
INSERT INTO @DocumentTypesReference VALUES (3, 'Receipt')

INSERT INTO [dbo].[DocumentType]
	SELECT src.[DocumentType], src.[DocumentTypeName]
	FROM @DocumentTypesReference src LEFT OUTER JOIN [dbo].[DocumentType] tgt WITH (UPDLOCK, HOLDLOCK) ON src.[DocumentType] = tgt.[DocumentType]
WHERE tgt.[DocumentType] IS NULL