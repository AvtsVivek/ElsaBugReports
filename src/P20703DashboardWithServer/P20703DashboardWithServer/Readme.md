This app is built from [sample here](https://elsa-workflows.github.io/elsa-core/docs/next/quickstarts/quickstarts-aspnetcore-server-dashboard-and-api-endpoints)

But I used Ms Sql instead of Sqlite

Note 

1. When running from visual studio 2019, select the profile to be IIS Express instead of P20703DashboardWithServer.
	This ensures that the launch application url will be http://localhost:26284. 
	This is important because, if you open appsettings.json you will see the following

	  "Elsa": {
    "Server": {
      "BaseUrl": "http://localhost:26284"
        //, "BasePath": "/workflows"
    }
  }

	So here the base url is set that way

2. Note the basePath in the above elsa section is commented out. If that is not commented, then when you execute the workflow, 
	The url should be /workflows/hello-world instead of /hello-world.