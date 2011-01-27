ALTER TABLE [dbo].[OrderItem]
	ADD CONSTRAINT [FK_MenuItemId_OrderItem] 
	FOREIGN KEY (MenuItemId)
	REFERENCES MenuItem (Id)	

