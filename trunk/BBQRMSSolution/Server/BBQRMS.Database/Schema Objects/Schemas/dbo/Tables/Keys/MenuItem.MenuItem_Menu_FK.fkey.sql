ALTER TABLE [dbo].[MenuItem]
	ADD CONSTRAINT [FK_MenuId_MenuItem] 
	FOREIGN KEY (MenuId)
	REFERENCES Menu (Id);	

