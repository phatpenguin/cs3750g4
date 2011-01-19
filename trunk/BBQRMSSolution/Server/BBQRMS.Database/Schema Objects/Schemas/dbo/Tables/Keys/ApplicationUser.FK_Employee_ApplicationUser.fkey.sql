ALTER TABLE [dbo].[ApplicationUser]
	ADD CONSTRAINT [FK_Employee_ApplicationUser] 
	FOREIGN KEY (employeeId)
	REFERENCES Employee (id)
	ON DELETE CASCADE

