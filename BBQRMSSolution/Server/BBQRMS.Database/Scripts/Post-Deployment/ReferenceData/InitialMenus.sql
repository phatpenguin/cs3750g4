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