CREATE TABLE [dbo].[EmployeeTimeClock]
(
	id			int identity,
	employeeId	int	NOT NULL,
	clockInTime	datetime NOT NULL,
	clockOutTime datetime NULL
)
