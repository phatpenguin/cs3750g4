CREATE TABLE [dbo].[EmployeeTimeClock]
(
	id			int identity,
	employeeId	int	NOT NULL,
	clockInTimeUTC	smalldatetime NOT NULL,
	clockOutTimeUTC smalldatetime NULL
)
