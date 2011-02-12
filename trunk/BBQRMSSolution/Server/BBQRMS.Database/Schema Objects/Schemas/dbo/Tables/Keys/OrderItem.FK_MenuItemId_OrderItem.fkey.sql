ALTER TABLE [dbo].[OrderItem]
	ADD CONSTRAINT [FK_OrderItem_MenuItem] 
	FOREIGN KEY (MenuItemId)
	REFERENCES "MenuItem" (Id)	