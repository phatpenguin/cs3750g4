ALTER TABLE [dbo].[RolePrivilegeMap]
	ADD CONSTRAINT [FK_PrivilegeId_RolePrivilegeMap] 
	FOREIGN KEY (PrivilegeId)
	REFERENCES Privilege (Id)