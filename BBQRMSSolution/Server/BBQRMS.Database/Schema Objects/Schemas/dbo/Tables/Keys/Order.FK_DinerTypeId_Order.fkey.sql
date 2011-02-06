ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_DinerTypeId_Order] 
	FOREIGN KEY (DinerTypeId)
	REFERENCES DinerType (Id)	