ALTER TABLE [dbo].[RolePrivilegeMap]
	ADD CONSTRAINT [FK_RoleId_RolePrivilegeMap] 
	FOREIGN KEY (RoleId)
	REFERENCES [Role] (Id)