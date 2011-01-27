ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_LocationId_Order] 
	FOREIGN KEY (LocationId)
	REFERENCES Location (Id)	

