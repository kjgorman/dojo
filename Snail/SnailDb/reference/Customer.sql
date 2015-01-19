PRINT 'Insert customer'

DECLARE @CustomerReference TABLE ([Id] BIGINT)

INSERT INTO @CustomerReference VALUES (1)

INSERT INTO [dbo].[Customer]
	SELECT src.Id 
	FROM @CustomerReference src
	LEFT OUTER JOIN [dbo].[Customer] tgt on src.Id = tgt.Id
	WHERE tgt.Id IS NULL