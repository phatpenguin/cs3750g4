ALTER TABLE [dbo].[MenuItemMap]
	ADD CONSTRAINT [FK_MenuItemMap_Menu] 
	FOREIGN KEY (MenuId)
	REFERENCES Menu (Id)	
