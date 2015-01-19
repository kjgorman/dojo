PRINT 'Inserting documents'

DECLARE @DocumentsReference TABLE (
	[Id] BIGINT NOT NULL PRIMARY KEY,
	[DocumentType] INT NOT NULL,
	[Amount] MONEY NOT NULL,
	[IssueDate] DATETIME2 NOT NULL,
	[ShipmentDate] DATETIME2 NULL,
	[ReceiptDate] DATETIME2 NULL,
	[CustomerAgreement] BIT NULL,
	[CustomerId] BIGINT NOT NULL, 
	[ParentDocumentId] BIGINT NULL)

INSERT INTO @DocumentsReference VALUES (1, 1, 10, GETUTCDATE(), NULL, NULL, 1, 1, NULL)
INSERT INTO @DocumentsReference VALUES (2, 2, 10, GETUTCDATE(), GETUTCDATE(), NULL, NULL, 1, 1)
INSERT INTO @DocumentsReference VALUES (3, 3, 10, GETUTCDATE(), NULL, GETUTCDATE(), NULL, 1, 2)

INSERT INTO [dbo].[Documents]
	SELECT src.DocumentType, src.Amount, src.IssueDate, src.ShipmentDate, src.ReceiptDate, src.CustomerAgreement, src.CustomerId, src.ParentDocumentId
	FROM @DocumentsReference src
	LEFT OUTER JOIN [dbo].[Documents] tgt on src.Id = tgt.Id
	WHERE tgt.Id IS NULL