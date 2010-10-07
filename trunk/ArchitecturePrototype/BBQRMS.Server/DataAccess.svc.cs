﻿using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace BBQRMS.Server
{
	public class DataAccess : DataService<DatabaseContainer>
		{
				// This method is called only once to initialize service-wide policies.
				public static void InitializeService(IDataServiceConfiguration config)
				{
						// TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
						// Examples:
						// config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
						// config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
					config.SetEntitySetAccessRule("*", EntitySetRights.All);
					config.UseVerboseErrors = true;
				}
		}
}
