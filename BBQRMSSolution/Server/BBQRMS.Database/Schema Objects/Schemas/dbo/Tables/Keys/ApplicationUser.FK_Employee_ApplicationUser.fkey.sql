ALTER TABLE [dbo].[ApplicationUser]
	ADD CONSTRAINT [FK_Employee_ApplicationUser] 
	FOREIGN KEY (EmployeeId)
	REFERENCES Employee (Id)
	ON DELETE CASCADE

