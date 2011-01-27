ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_TableSectionId_Order] 
	FOREIGN KEY (TableSectionId)
	REFERENCES [TableSection] (Id)	

