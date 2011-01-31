-- =============================================
-- Script Template
-- ideas on how to manage 'reference data' or 'static data' with database projects and deployment.
-- http://blogs.msdn.com/b/bahill/archive/2009/04/01/maintaining-and-synchronizing-your-reference-data.aspx
-- http://leon.mvps.org/StaticData/
-- =============================================
/* This script will create a handful of initial users who will then be able to log in */

----------------------------------
-- EmployeePayType
----------------------------------
select top 0 *
into #EmployeePayType
from (select * from [dbo].[EmployeePayType]
union select top 0 * from [dbo].[EmployeePayType]) a

insert into #EmployeePayType
values 
(1, 'Hourly'),
(2, 'Salary')


set identity_insert [dbo].[EmployeePayType] on

update [dbo].[EmployeePayType]
set Descr = Source.Descr
from [dbo].[EmployeePayType] Target inner join #EmployeePayType Source on Target.Id = Source.Id

insert into [dbo].[EmployeePayType] (Id, Descr)
select Source.*
from [dbo].[EmployeePayType] Target right join #EmployeePayType Source on Target.Id = Source.Id
where Target.id is null

set identity_insert [dbo].[EmployeePayType] off

----------------------------------
-- Employee
----------------------------------
select top 0 *
into #Employee
from (select * from [dbo].[Employee]
union select top 0 * from [dbo].[Employee]) a

insert into #Employee (Id, LastName, FirstName, HireDate, Phone1, Phone2, Phone3, Address1, Address2, Email1, Email2, PayTypeId, PayAmount)
values 
(1, 'First', 'User', '2011-01-19', '(801) 867-2103', '(801) 867-2102', '(801) 240-2542', '1600 Pensilvania Ave.', 'Washington, DC 10001', 'noreply@spamyou.net', null, 1, 7.25),
(2, 'Second', 'User', '2010-01-19', null, null, null, null, null, null, null, 1, 9.45),
(3, 'Third', 'User', '2010-01-19', null, null, null, null, null, null, null, 1, 9.45)

set identity_insert [dbo].[Employee] on

update [dbo].[Employee]
set firstName = Source.firstName,
lastName = Source.lastName,
hireDate = Source.hireDate,
phone1 = Source.phone1, 
phone2 = Source.phone2, 
phone3 = Source.phone3,
address1 = Source.address1,
address2 = Source.address2,
email1 = Source.email1,
email2 = Source.email2,
payTypeId = Source.payTypeId,
payAmount = Source.payAmount
from [dbo].[Employee] Target inner join #Employee Source on Target.id = Source.id

insert into [dbo].[Employee] (id, firstName, lastName, hireDate, phone1, phone2, phone3, address1, address2, email1, email2, payTypeId, payAmount)
select Source.*
from [dbo].[Employee] Target right join #Employee Source on Target.id = Source.id
where Target.id is null

set identity_insert [dbo].[Employee] off

----------------------------------
-- EmployeeRoleMap
----------------------------------
delete from EmployeeRoleMap where EmployeeId in (select Id from #Employee)
insert into [dbo].[EmployeeRoleMap] (EmployeeId, RoleId)
values
(1,1), -- First user - Admin
(2,2), -- Second user - Cashier
(3,3) -- Third user - Cook


----------------------------------
-- ApplicationUser
----------------------------------
select top 0 *
into #ApplicationUser
from [dbo].[ApplicationUser]

insert into #ApplicationUser (idPart, personalPart, employeeId, displayName)
values
('1','11',1,'First User'),
('2','22',2,'Second User'),
('3','33',3,'Third User')

update [dbo].[ApplicationUser]
set idPart = Source.idPart,
personalPart = Source.personalPart,
employeeId = Source.employeeId,
displayName = Source.displayName
from [dbo].[ApplicationUser] Target inner join #ApplicationUser Source on Target.idPart = Source.idPart

insert into [dbo].[ApplicationUser]
select Source.*
from [dbo].[ApplicationUser] Target right join #ApplicationUser Source on Target.idPart = Source.idPart
where Target.idPart is null