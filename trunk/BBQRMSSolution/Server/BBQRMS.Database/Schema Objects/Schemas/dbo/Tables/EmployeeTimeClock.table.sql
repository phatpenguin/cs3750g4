CREATE TABLE [dbo].[EmployeeTimeClock]
(
	Id			int identity,
	EmployeeId	int	NOT NULL,
	ClockInTimeUTC	smalldatetime NOT NULL,
	ClockOutTimeUTC smalldatetime NULL
)
