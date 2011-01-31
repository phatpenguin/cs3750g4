----------------------------------
-- Privilege
----------------------------------
select top 0 *
into #Privilege
from (select * from [dbo].[Privilege]
union select top 0 * from [dbo].[Privilege]) a

insert into #Privilege (Id, Description)
values
(1, 'Take Orders'),
(2, 'Cashier'),
(3, 'Use Cooks Screen'),
(4, 'Use Quick Inventory'),
(5, 'Run Reports'),
(6, 'Manage Employees'),
(7, 'Manage Inventory'),
(8, 'Manage Menus')

delete from [dbo].[RolePrivilegeMap] where PrivilegeId not in (select Id from #Privilege)
delete from [dbo].[Privilege] where Id not in (select Id from #Privilege)

set identity_insert [dbo].[Privilege] on

update [dbo].[Privilege]
set Description = Source.Description
from [dbo].[Privilege] Target inner join #Privilege Source on Target.Id = Source.Id

insert into [dbo].[Privilege] (Id, Description)
select Source.*
from [dbo].[Privilege] Target right join #Privilege Source on Target.Id = Source.Id
where Target.id is null

set identity_insert [dbo].[Privilege] off

----------------------------------
-- Role
----------------------------------
select top 0 *
into #Role
from (select * from [dbo].[Role]
union select top 0 * from [dbo].[Role]) a

insert into #Role (Id, Description)
values
(1,'Admin'),
(2,'Cashier'),
(3,'Cook'),
(4,'Server')

set identity_insert [dbo].[Role] on

update [dbo].[Role]
set Description = Source.Description
from [dbo].[Role] Target inner join #Role Source on Target.Id = Source.Id

insert into [dbo].[Role] (Id, Description)
select Source.*
from [dbo].[Role] Target right join #Role Source on Target.Id = Source.Id
where Target.id is null

set identity_insert [dbo].[Role] off

----------------------------------
-- RolePrivilegeMap
----------------------------------
-- we're going to completely replace the privileges assigned to roles 1-4.
delete from RolePrivilegeMap where RoleId between 1 and 4

insert into RolePrivilegeMap (RoleId, PrivilegeId)
values
(1,1), -- Admin - take orders
(1,2), -- Admin - cashier
(1,3), -- Admin - cooks screen
(1,4), -- Admin - quick inventory
(1,5), -- Admin - reporting
(1,6), -- Admin - manage employees
(1,7), -- Admin - manage inventory
(1,8), -- Admin - manage menus
(2,1), -- Cashier - take orders
(2,2), -- Cashier - cashier
(3,3), -- Cook - cooks screen
(3,4), -- Cook - quick inventory
(4,1) -- Server - take orders