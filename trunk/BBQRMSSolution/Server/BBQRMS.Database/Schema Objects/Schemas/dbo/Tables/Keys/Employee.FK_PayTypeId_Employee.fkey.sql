ALTER TABLE [dbo].[Employee]
	ADD CONSTRAINT [FK_PayTypeId_Employee] 
	FOREIGN KEY (PayTypeId)
	REFERENCES EmployeePayType (Id)	

