CREATE TABLE [dbo].[Documents]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[DocumentType] INT NOT NULL,
	[Amount] MONEY NOT NULL,
	[IssueDate] DATETIME2 NOT NULL,
	[ShipmentDate] DATETIME2 NULL,
	[ReceiptDate] DATETIME2 NULL,
	[CustomerAgreement] BIT NULL,
	[CustomerId] BIGINT NOT NULL, 
	[ParentDocumentId] BIGINT NULL,

	CONSTRAINT [FK_Documents_DocumentType] FOREIGN KEY ([DocumentType]) REFERENCES [dbo].[DocumentType]([DocumentType]),
	CONSTRAINT [FK_Documents_Documents] FOREIGN KEY ([ParentDocumentId]) REFERENCES [dbo].[Documents]([Id]),
	CONSTRAINT [FK_Documents_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer]([Id])
)
