-- =============================================
-- Script Template
-- ideas on how to manage 'reference data' or 'static data' with database projects and deployment.
-- http://blogs.msdn.com/b/bahill/archive/2009/04/01/maintaining-and-synchronizing-your-reference-data.aspx
-- http://leon.mvps.org/StaticData/
-- =============================================
/* This script will create a few default menu items for testing */

select top 0 *
into #Menu
from (select * from [dbo].[Menu]
union select top 0 * from [dbo].[Menu]) a

insert into #Menu
values 
(1, 'Drinks'),
(2, 'Entrees'),
(3, 'Sides')

set identity_insert [dbo].[Menu] on

update [dbo].[Menu]
set name = Source.name

from [dbo].[Menu] Target inner join #Menu Source on Target.id = Source.id

insert into [dbo].[Menu] (id, name)
select Source.*
from [dbo].[Menu] Target right join #Menu Source on Target.id = Source.id
where Target.id is null

set identity_insert [dbo].[Menu] off

select top 0 *
into #MenuItem
from (select * from [dbo].[MenuItem]
union select top 0 * from [dbo].[MenuItem]) a

insert into #MenuItem
values 
(1, 1.25, 'Soda', 'This is a drink'),
(2, 5.99, 'Ribs', 'This is a entree'),
(3, 3.25, 'Sides', 'This is a side')

set identity_insert [dbo].[MenuItem] on

update [dbo].[MenuItem]
set name = Source.name

from [dbo].[MenuItem] Target inner join #MenuItem Source on Target.id = Source.id

insert into [dbo].[MenuItem] (Id, Price, Name, Description)
select Source.*
from [dbo].[MenuItem] Target right join #MenuItem Source on Target.id = Source.id
where Target.id is null

set identity_insert [dbo].[MenuItem] off

select top 0 *
into #MenuItemMap
from (select * from [dbo].[MenuItemMap]
union select top 0 * from [dbo].[MenuItemMap]) a

delete from MenuItemMap

insert into #MenuItemMap
values 
(1, 1),
(2, 2),
(3, 3)

insert into [dbo].[MenuItemMap] (MenuID, MenuItemID)
select Source.*
from #MenuItemMap Source