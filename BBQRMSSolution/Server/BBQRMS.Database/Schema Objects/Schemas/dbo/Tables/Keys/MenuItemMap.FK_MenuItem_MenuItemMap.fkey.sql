ALTER TABLE [dbo].[MenuItemMap]
	ADD CONSTRAINT [FK_MenuItemMap_MenuItem] 
	FOREIGN KEY (MenuItemId)
	REFERENCES MenuItem (Id)	
