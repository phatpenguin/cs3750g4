ALTER TABLE [dbo].[EmployeeTimeClock]
	ADD CONSTRAINT [FK_EmployeeTimeClock_Employee] 
	FOREIGN KEY (EmployeeId)
	REFERENCES Employee (Id)
	ON DELETE CASCADE

