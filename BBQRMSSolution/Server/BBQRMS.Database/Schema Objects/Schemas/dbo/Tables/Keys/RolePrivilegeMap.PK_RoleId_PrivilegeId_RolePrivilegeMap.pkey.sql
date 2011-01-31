ALTER TABLE [dbo].[RolePrivilegeMap]
	ADD CONSTRAINT [PK_RoleId_PrivilegeId_RolePrivilegeMap]
	PRIMARY KEY (RoleId, PrivilegeId)