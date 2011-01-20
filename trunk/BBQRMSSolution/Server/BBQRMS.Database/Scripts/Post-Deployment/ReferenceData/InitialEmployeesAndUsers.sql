-- =============================================
-- Script Template
-- ideas on how to manage 'reference data' or 'static data' with database projects and deployment.
-- http://blogs.msdn.com/b/bahill/archive/2009/04/01/maintaining-and-synchronizing-your-reference-data.aspx
-- http://leon.mvps.org/StaticData/
-- =============================================
/* This script will create a handful of initial users who will then be able to log in */

select top 0 *
into #Employee
from (select * from [dbo].[Employee]
union select top 0 * from [dbo].[Employee]) a

insert into #Employee
values 
(1, 'First', 'Name', '2011-01-19', null, null, null, null, null, null, null, 1, 7.25),
(2, 'Second', 'Name', '2010-01-19', null, null, null, null, null, null, null, 1, 9.45)

set identity_insert [dbo].[Employee] on

update [dbo].[Employee]
set firstName = Source.firstName,
lastName = Source.lastName,
hireDate = Source.hireDate,
-- other columns
payTypeId = Source.payTypeId,
payAmount = Source.payAmount
from [dbo].[Employee] Target inner join #Employee Source on Target.id = Source.id

insert into [dbo].[Employee] (id, firstName, lastName, hireDate, phone1, phone2, phone3, address1, address2, email1, email2, payTypeId, payAmount)
select Source.*
from [dbo].[Employee] Target right join #Employee Source on Target.id = Source.id
where Target.id is null

set identity_insert [dbo].[Employee] off

select top 0 *
into #ApplicationUser
from [dbo].[ApplicationUser]

insert into #ApplicationUser (idPart, personalPart, employeeId, displayName)
values
(1,11,1,'First Name'),
(2,22,2,'Second Name')

update [dbo].[ApplicationUser]
set personalPart = Source.idPart,
employeeId = Source.employeeId,
displayName = Source.displayName
from [dbo].[ApplicationUser] Target inner join #ApplicationUser Source on Target.idPart = Source.idPart

insert into [dbo].[ApplicationUser]
select Source.*
from [dbo].[ApplicationUser] Target right join #ApplicationUser Source on Target.idPart = Source.idPart
where Target.idPart is null