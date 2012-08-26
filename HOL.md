<a name="Title" />
# Building Windows 8 Applications using Windows Azure Web Sites #

---
<a name="Overview" />
## Overview ##

Apps are at the center of the Windows 8 experience. They’re alive with activity and vibrant content. Users are immersed in your full-screen, Windows 8 Style UI apps, where they can focus on their content, rather than on the operating system.

In this hands-on lab you will learn how to combine the fluency of Windows 8 applications with the power of Windows Azure: From a Windows Windows 8 Style UI application, you will consume an ASP.NET MVC 4 Web API service published in Windows Azure Web Sites, and store your data in a Windows Azure SQL Database. In addition, you will learn how to configure the Windows Push Notification Services (WNS) in your app to send toast notifications from your service to all the registered clients.

<a name="Objectives" />
### Objectives ###

In this hands-on lab, you will learn how to use Visual Studio 2012 to:

* Create an ASP.NET MVC 4 Web API service
* Publish the service to Windows Azure Web Sites
* Create a Windows 8 Style UI application that consumes the Web API service
* Add Push Notifications to the Windows 8 Style UI application by using the Windows Azure Toolkit for Windows 8

<a name="Prerequisites" />
### Prerequisites ###

The following is required to complete this hands-on lab:

- [Microsoft Visual Studio 2012](http://www.microsoft.com/visualstudio/11/en-us)
- A Windows Azure subscription with the Web Sites Preview enabled - [sign up for a free trial](http://aka.ms/WATK-FreeTrial)

>**Note:** This lab was designed to use Windows 8 Operating System.

<a name="Setup"/>
### Setup ###

In order to execute the exercises in this hands-on lab you need to set up your environment.

1. Open a Windows Explorer window and browse to the lab's **Source** folder.

1. Right-click the **Setup.cmd** file and click **Run as administrator**. This will launch the setup process that will configure your environment and install the Visual Studio code snippets for this lab.

>**Note:** Make sure you have checked all the dependencies for this lab before running the setup. 

### Using the Code Snippets ###

Throughout the lab document, you will be instructed to insert code blocks. For your convenience, most of that code is provided as Visual Studio Code Snippets, which you can use from within Visual Studio 2012 to avoid having to add it manually.

---
<a name="Exercises" />
## Exercises ##

This hands-on lab includes the following exercises:

1. [Building and Consuming an ASP.NET Web API from a Windows 8 Style UI App](#Exercise1)
1. [Basic Data Binding and Data Access Using Windows Azure SQL Databases and Entity Framework Code First](#Exercise2)
1. [Adding Push Notification Support to your Windows 8 Style UI Application](#Exercise3)

> **Note:** Each exercise is accompanied by a starting solution located in the Begin folder of the exercise that allows you to follow each exercise independently of the others. Please be aware that the code snippets that are added during an exercise are missing from these starting solutions and that they will not necessarily work until you complete the exercise.
>
>Inside the source code for an exercise, you will also find an End folder containing a Visual Studio solution with the code that results from completing the steps in the corresponding exercise. You can use these solutions as guidance if you need additional help as you work through this hands-on lab.

<a name="Exercise1" />
### Exercise 1: Building and Consuming an ASP.NET Web API from a Windows 8 Style UI App ###

ASP.NET Web API is a new framework from MVC 4 that facilitates to build and consume HTTP services for a wide range of clients.

In this exercise you will learn the basics of consuming an ASP.NET MVC 4 Web API REST service - hosted in Windows Azure Web Sites - from a Windows 8 Style UI application. 

For that purpose, you will first create a new Azure Web Site in the portal to host the service. Then, you will create a new ASP.NET MVC 4 Web API project and publish it in Windows Azure from Visual Studio. Once the default service is published, you will create a basic Windows 8 Style UI client application with a simple list to retrieve the service values.

<a name="Ex1Task1" />
#### Task 1 – Creating a New Web Site Hosted in Windows Azure ####

1. Go to the [Windows Azure Management portal](https://manage.windowsazure.com) and sign in using your **Microsoft Account** credentials associated with your subscription.

	![Log in into Windows Azure portal](images/log-in-into-windows-azure-portal.png?raw=true "Log in into Windows Azure portal")

	_Log in into Windows Azure portal_

1. Click **New** on the command bar.

	![Creating a new Web Site](images/creating-a-new-web-site.png?raw=true "Creating a new Web Site")

	_Creating a new Web site_

1. Click **Web Site** and then **Quick Create**.  Provide an available URL (e.g. _customers-service_) for the new web site and click **Create Web Site**.

	> **Note:** A Windows Azure Web Site is the host for a web application running in the cloud that you can control and manage. The Quick Create option allows you to deploy a completed web application to the Windows Azure Web Sites from outside the portal. It does not include steps for setting up a database.

	![Creating a new Web Site using Quick Create ](images/creating-a-new-web-site-using-quick-create-op.png?raw=true "Creating a new Web Site using Quick Create")

	_Creating a new web site using Quick Create_

1. Wait until the new web site is created.

	![Creating new web site status](images/creating-new-web-site-status.png?raw=true "Creating new web site status")

	_Creating a new web site - Status_

1. Once the web site is created click the link under the **URL** column to check that it is working.

	![Browsing to the new web site](images/browsing-to-new-site.png?raw=true "Browsing to the new web site")

	_Browsing to the new web site_

	![Web site running](images/web-site-running.png?raw=true "Web site running")

	_Web site running_

1. Go back to the portal and click the name of the web site under the **Name** column to display the management pages for the web site.

	![Opening the web site management pages](images/selecting-the-dashboard-tab.png?raw=true "Opening the web site management pages")

	_Opening the web site dashboard_

1. If this is the first time you access to the portal you might be redirected to the **Quickstart** page. Click **Dashboard** in the menu to continue.

1. In the **Dashboard** page, under the **quick glance** section, click the **Download publish profile** link and save the file to a known location. You will use this settings later to publish the web site from Visual Studio.

	> **Note:** The _publish profile_ contains all of the information required to publish a web application to a Windows Azure website for each enabled publication method. The publish profile contains the URLs, user credentials and database strings required to connect to and authenticate against each of the endpoints for which a publication method is enabled. **Microsoft Visual Studio** supports reading publish profiles to automate the publishing configuration for web applications to Windows Azure Web Sites.

	![Downloading the publish profile](images/download-publish-profile.png?raw=true "Downloading the publish profile")

	_Downloading the publish profile_

<a name="Ex1Task2" />
#### Task 2 – Creating an MVC 4 Web API Service ####

In this task you will create a new MVC 4 Web API project and explore its components. 

>**Note:** You can learn more about ASP.NET Web API  [here](http://www.asp.net/web-api).

1. Open Visual Studio 2012 and select **File | New | Project** to start a new solution.

	![New Project](images/new-project.png?raw=true "New Project")

	_Creating a New Project_

1. In the **New Project** dialog, select **ASP.NET MVC 4 Web Application** under the **Visual C# | Web** tab. 

	Make sure the **.NET Framework 4** is selected, name it _WebApi_, and click **OK**.

	>**Note:** At the time this lab was written, Windows Azure Web Sites did not support .NET Framework 4.5.

	![New MVC 4 Project](images/new-mvc4-project.png?raw=true "New MVC 4 Project")

	_New MVC 4 Project_

1. In the new **ASP.NET MVC 4 Web Application** dialog, select **Web API** and make sure that **Razor** is selected as the view engine.

	![New ASP.NET MVC 4 Web API project](images/new-aspnet-mvc4-webapi-project.png?raw=true "New ASP.NET MVC 4 Web API project")

	_New ASP.NET MVC 4 Web API project_

1. You will now explore the structure of an ASP.NET Web API project. Notice that the structure of a Web API project is similar to an MVC 4 Web Application.

	![ASP.NET Web API Project](images/aspnet-webapi-project.png?raw=true "ASP.NET Web API Project")

	_ASP.NET Web API Project_

	1. **Controllers:**  A controller is an object that handles HTTP requests. If you have worked with ASP.NET MVC, you will notice that they work similarly in Web API, but controllers in Web API derive from the ApiController instead of Controller Class. The first major difference is that actions on Web API controllers return data instead of views.
		The New Project wizard created two other controllers for you when it created the project: Home and Values. 
	
		-The **Home** controller is responsible for serving HTML pages for the site, and is not directly related to Web API. 

		-The **Values** Controller is an example of a Web API controller. 

	1. **Models**: In this folder you will place the classes that represent the data in your application. ASP.NET Web API can automatically serialize your model to JSON, XML, or some other format, and then write the serialized data into the body of the HTTP response message. 

	1. **Routing:** To determine which action to invoke, the framework uses a routing table configured in App_Start/RouteConfig.cs. The project template creates a default HTTP route named "DefaultApi".
		
		````C#
		routes.MapHttpRoute(
			 name: "DefaultApi",
			 routeTemplate: "api/{controller}/{id}",
			 defaults: new { id = RouteParameter.Optional }
		);
		````

		When the Web API framework receives an HTTP request, it tries to match the URI against one of the route templates in the routing table. Once a matching route is found, Web API selects the controller and the action. For instance, these URIs match the default route:

		-/api/values/

		-/api/values/1
			
			
		>**Note:**The reason for using an 'api' prefix in the route is to prevent collisions with ASP.NET MVC routing, which manages views in the same namespace. That way, you can have a "values" view and a "values" action method at the same time. However, you can change the default prefix and use your own routes. 

		
1. Press **F5** to run the solution or, alternatively, click the **Start** button located on the toolbar to run the solution. The Web API template home page will open.

	> **Note:** If the web application is not displayed after the deployment, try refreshing the browser a couple of times.

1. In the browser, go to **/api/values** to retrieve the JSON output of the sample service. 

	In the browser, you will be prompted to download a file. Click **Open**. If prompted, choose to open the file with a text editor.
	
	![Retrieving the default  values](images/retrieving-the-default-webapi-values.png?raw=true "Retrieving the default values")

	_Retrieving the default values_

<a name="Ex1Task3" />
#### Task 3 – Publishing the Web API Service to Windows Azure Web Sites ####

1. In the Solution Explorer, right-click the project node and select **Publish** to open the Publish Web wizard.

	![Publishing the service](images/publishing-the-service.png?raw=true "Publishing the service")

	_Publishing the service_

1. In the **Profile** page, click the **Import** button and select your publish profile file, downloaded previously. Click **Next**.

	![Publishing profile selection](images/publising-profile-profile-selection.png?raw=true)
	
	_Selecting a publishing profile_

1. In the **Connection page**, leave the imported values and click **Next**.

	![Publishing profile imported](images/publishing-profile-imported.png?raw=true "Publishing profile imported")

	_Publish profile imported_

1. In the **Settings** page, leave the default values and click **Next**.

	![Publish Settings page](images/publish-settings-page.png?raw=true "Publish Settings page")

	_Publish Settings page_

1. In the **Preview** page, click **Publish**.

	>**Note:** If this is the first time you deploy a web site, you will be prompted to accept a certificate. After the message appears, click **Accept**. 
	>
	> ![Publishing certificate error](images/publishing-certificate-error.png?raw=true "Publishing certificate error")
	>

	![Publishing a web site](images/publishing-a-web-site.png?raw=true "Publishing a web site")
	
	_Publishing a web site_

1. When the process is completed the published web site will be opened in your default web browser. Go to **/api/values** to retrieve the default values and test that the Web API service is working successfully.

	![Default web service published](images/default-web-service-published.png?raw=true "Default web service published")

	_Default web service - Published_

<a name="Ex1Task4" />
#### Task 4 – Creating a Windows 8 Style UI Client Application ####

In this task you will create a blank Windows 8 Style UI application that will consume the service you have already running.

1. Switch back to Visual Studio. Right-click the solution node on the Solution Explorer and select **Add | New Project**.

1. In the **New Project** dialog, select the **Blank** application under the **Visual C# | Windows Windows 8 Style UI** applications. Name it _MetroStyleClient_ and click **OK**.

	Make sure the selected framework is **.NET Framework 4.5**.

	![Add a new Windows 8 Style UI application basic client project](images/add-new-metro-app-basic-client-project.png?raw=true "Add new Windows 8 Style UI application basic client project")

	_Add a new Windows 8 Style UI application basic client project_

1. In the Windows 8 Style UI application, open **MainPage.xaml.cs** and add a reference to Windows.Data.Json.

	<!-- mark:1 -->
	````C#
	using Windows.Data.Json;
	````

1. In **MainPage.xaml.cs**, add the following method to perform an asynchronous call to the Web API service.

	GetItem() instantiates an HttpClient object, which  sends a GET message to the service URL and retrieves a response asynchronously. Then, the response is deserialized and read by the JsonArray object before generating the value list. 
	
	> **Note:** If you want to read more about async methods you can check out [this article](http://msdn.microsoft.com/en-US/vstudio/async). 

	(Code Snippet - _Building Windows 8 Apps - Ex1 - GetItems_)

	````C#
	public async void GetItems()
	{
		 var serviceURI = "[YOUR-WINDOWS-AZURE-SERVICE-URI]/api/values";

		 using (var client = new System.Net.Http.HttpClient())
		 using (var response = await client.GetAsync(serviceURI))
		 {
			  if (response.IsSuccessStatusCode)
			  {
					var data = await response.Content.ReadAsStringAsync();
					var values = JsonArray.Parse(data);

					var valueList = from v in values
										 select new
										 {
											  Name = v.GetString()
										 };

					this.listValues.ItemsSource = valueList;
			  }
		 }
	}
	````

1. Replace the value of the placeholder **[YOUR-WINDOWS-AZURE-SERVICE-URL]** with your Windows Azure published web site URL. You can check that value in the web site's dashboard.

	>**Note:** If you want to test the service locally, start the service project, check its URL and its port (e.g. http://localhost:3565/) and use that value.

1. Then, in the **OnNavigateTo** method, add a call to the GetItem method and press **CTRL+S** to save.

	<!-- mark:3-4 -->
	````C#
	protected override void OnNavigatedTo(NavigationEventArgs e)
	{
		this.GetItems();
	}
	````

1. Open **MainPage.xaml** and add the following controls in the grid to display the service values.

	You will add a listbox that will bind against a values list.

	<!-- mark:2-9 -->
	````XML
	<Grid Background="{StaticResource ApplicationPageBackgroundThemeBrush}">        
	  <ListBox x:Name="listValues" HorizontalAlignment="Left" Width="250" ScrollViewer.VerticalScrollBarVisibility="Visible">
			<ListBox.ItemTemplate>
				 <DataTemplate>
					  <TextBlock Text="{Binding Name}" Width="200" Height="25" FontSize="18" />         
				 </DataTemplate>
			</ListBox.ItemTemplate>
	  </ListBox>
	  <TextBlock HorizontalAlignment="Left" Margin="265,10,0,0" TextWrapping="Wrap" Text="Web API default service values" VerticalAlignment="Top" FontSize="25"/>
	</Grid>
	````

1. Make sure the Windows 8 Style UI application is the start up project and press **F5** to run the solution. 

	You will see that the values retrieved from the service are listed in the List Box

	![Basic service output](images/basic-service-output.png?raw=true "basic service output")

	_Service output_

---

<a name="Exercise2" />
### Exercise 2: Basic Data Binding and Data Access Using Windows Azure SQL Databases and Entity Framework Code First ###

In this exercise, you will learn how to bind your Windows 8 Style UI application to an ASP.NET Web API service which is using Code First to generate the database from the model in SQL Database.

You will start the exercise provisioning a new SQL Database. Then, you will create a new ASP.NET Web API service and use Entity Framework Scaffolding with Code First to generate the service methods and a database in SQL Database.
Finally, you will explore and customize your Windows 8 Style UI application to consume the service and show a customer list.

**About Entity Framework Code First**

Entity Framework (EF) is an object-relational mapper (ORM) that enables you to create data access applications by programming with a conceptual application model instead of programming directly using a relational storage schema.

The Entity Framework Code First modeling workflow allows you to use your own domain classes to represent the model that EF relies on when performing querying, change tracking and when updating functions. Using the Code First development workflow, you do not need to begin your application by creating a database or specifying schema!. Instead, you can begin by writing standard .NET classes that define the most appropriate domain model objects for your application, and Entity Framework will create the database for you.

>**Note:** You can learn more about Entity Framework [here](http://www.asp.net/entity-framework).

<a name="Ex2Task1" />
#### Task 1 - Creating a SQL Database Server ####

>**Note:** If you already have a SQL Database server provisioned in your Windows Azure account, you can skip this task. 

In this task you will provision a SQL Database server in Windows Azure that will store your service data.

1. Switch to Windows Azure Management portal and click **SQL Databases** on the left pane.

1. Click the **Servers** link.

1. Click **Add** at the bottom of the page to start creating a SQL Database server.

	![Creating a New SQL Database server](images/creating-a-new-sql-database-server.png?raw=true "Creating a New SQL Database server")

	_Creating a New SQL Database server_

1. In the **Server Settings** dialog, enter a login name (e.g. User), a password and a region. Leave the default option checked and click the **OK** confirmation button to start creating the server.

	![Server settings dialog](images/server-settings-dialog.png?raw=true "Server settings dialog")

	_Server settings dialog_

1. Once the server is created, enter to the server **Dashboard** and copy the server URL from the **Manage URL** value. You will use it later in this lab to configure the Web API service data source.

	![servers-dashboard](images/servers-dashboard.png?raw=true)

	_Server dashboard_

<a name="Ex2Task2" />
#### Task 2 - Creating an ASP.NET Web API Service with Entity Framework Code First and Scaffolding ####

In this task you will create a new ASP.NET Web API service with Entity Framework Scaffolding and Code First. At the end of this task, you will have a basic  API service for performing CRUD operations (Create, Read, Update and Delete) implemented and published in Windows Azure Web Sites.

1. In Visual Studio, open **CustomerManager.sln** located in the **source/Ex2-DataAccess/Begin** folder of this lab.

	This solution already contains a Windows 8 Style UI application; you will now add a Web API service used by the application to retrieve the data.

1. Right-click the Solution Explorer and select **Add | New Project**. 

	![Add New Service project](images/add-new-service-project.png?raw=true "Add New Service project")

	_Adding a new project_

1. In the **Add New Project** dialog, select the **ASP.NET MVC 4 Web Application** located under the **Visual C# | Web** node, and name it _CustomerManager.WebApi_. Make sure **.NET Framework 4** is selected.

	![Adding a new Web API project](images/adding-a-new-webapi-project.png?raw=true "Adding a new Web API project")

	_Adding a new Web API project_

1. In the **New ASP.NET MVC 4 Project** dialog, select the **Web API** template.

	![New ASP.NET MVC 4 Web API project](images/new-aspnet-mvc4-webapi-project2.png?raw=true "New ASP.NET MVC 4 WebApi project")

	_New ASP.NET MVC 4 Web API project_

1. Once the project is created, add a reference to _System.Runtime.Serialization_. In Solution Explorer, right-click the references folder and select **Add Reference**.

	![Adding a new reference](images/adding-a-new-reference.png?raw=true "Adding a new reference")

	_Adding a new reference_

1. Select the **Assemblies** section and search for _System.Runtime.Serialization_ and select it. Then click **OK**.

	![Adding a reference ](images/adding-a-reference.png?raw=true "Adding a reference ")

	_Adding a reference to System.Runtime.Serialization_

1. Right-click the **Models** folder in the **Solution Explorer** and select **Add | Class**. Name it **Customer.cs**.

	![Adding a model class](images/add-model-class.png?raw=true "Add model class")

	_Adding a model class_

	> **Note:** You will start by creating a Customer model class, and your CRUD operations in the service will be automatically created using scaffolding features.

1. Replace the Customer.cs class content with the following code. Press **CTRL+S** to save the changes.

	(Code Snippet - _Building Windows 8 Apps - Ex2 - Customer class_)
	<!-- mark:1-36 -->
	````C#
	namespace CustomerManager.Models
	{
		 using System.Collections.Generic;
		 using System.ComponentModel.DataAnnotations;
		 using System.Linq;
		 using System;
		 using System.Runtime.Serialization;

		 [DataContract]
		 public class Customer
		 {
			  [DataMember]
			  public int CustomerId { get; set; }

			  [DataMember]
			  public string Name { get; set; }

			  [DataMember]
			  public string Phone { get; set; }

			  [DataMember]
			  public string Address { get; set; }

			  [DataMember]
			  public string Company { get; set; }

			  [DataMember]
			  public string Title { get; set; }

			  [DataMember]
			  public string Email { get; set; }

			  [DataMember]
			  public string Image { get; set; }
		 }
	}
	````
	
	>**Note:** You are adding **DataContract** and **DataMember** annotations to the class and its attributes because the Windows 8 Style UI client application is using DataContractJSONSerializer object to read and serialize the customers to JSON. 

1. In Solution Explorer, right-click the WebApi project and select **Build**.

	> **Note:** If you build the entire solution you will get errors as the Metro app client is still incomplete.

1. In Solution Explorer, right-click the **Controllers** folder of the Web API project and select **Add | Controller** to open the **Add Controller** dialog.

1. Select **CustomersController** as the controller name. In the **Scaffolding options**, select the **API controller with read/write actions, using Entity Framework** Template, and **Customer** as the Model class.

	![Adding a controller with Scaffolding](images/adding-a-controller-with-scaffolding.png?raw=true "Adding a controller with Scaffolding")

	_Adding a controller with Scaffolding_

	> **Note:** If you do not have model classes to select you need to build the project.

1. In the Data context class, select **New data context**. Name the new data context _CustomerContext_.

	![New customers context](images/new-customers-context.png?raw=true "New customers context")

	_New customers context_

1. Click **Add** to add the controller. By using the 'API controller with read/write actions and Entity Framework' template, the CRUD operations for customers will be automatically generated in the Web API service.

1. Once the controller with scaffolding is created, open CustomersController. Notice that the following CRUD actions were added:
	
	-DeleteCustomers(int id)

	-GetCustomer(int id)

	-GetCustomers(int id)

	-PostCustomer(Customer customer)
	
	-PutCustomer(int id, Customer customer)

	Notice that each operation has the HTTP verb as a prefix in the name (Delete, Get, Post, etc.).

1. Now, you will add a database initializer method in your database context to populate the database with initial data. Add the following **CustomerContextInitializer** class in CustomerContext.cs file after CustomerContext class, and save the changes.

	(Code Snippet - _Building Windows 8 Apps - Ex2 - Context Initializer_)
	<!-- mark:15-20 -->
	````C#
	using System.Data.Entity;
	using CustomerManager.Models;

	namespace CustomerManager.WebApi.Models
	{
		 public class CustomerContext : DbContext
		 {
			  public CustomerContext() : base("name=CustomerContext")
			  {
			  }

			  public DbSet<Customer> Customers { get; set; }
		 }

		 public class CustomerContextInitializer : DropCreateDatabaseIfModelChanges<CustomerContext>
		 {
			  protected override void Seed(CustomerContext context)
			  {
			  }
		 }
	}
	````

	>**Note:** Code First allows us to insert into our database by using a database initializer and overriding the Seed method. In this case the class inherits from **DropCreateDatabaseIfModelChanges\<TContext\>**, where TContext is CustomerContext.
	>
	> The **DropCreateDatabaseIfModelChanges\<TContext\>** class is an implementation of **IDatabaseInitializer\<TContext\>** that will delete, recreate, and optionally reseed the database with data only if the model has changed since the database was created. This is achieved by writing a hash of the store model to the database when it is created and then comparing that hash with one generated from the current model.
	> 
	> Alternatively, you can use **CreateDatabaseIfNotExists\<TContext>**, which recreates and optionally re-seeds the database with data only if the database does not exist, or **DropCreateDatabaseAlways\<TContext>**, which always recreates and optionally re-seeds the database with data the first time that a context is used in the application domain.
	

1. In the CustomerContextInitializer **Seed** method, add the following customers to the context to populate the database with customers.

	(Code Snippet - _Building Windows 8 Apps - Ex2 - Context Initializer Seed_)
	<!-- mark:3-102 -->
	````C#
	protected override void Seed(CustomerContext context)
	{
		context.Customers.Add(new Customer
		{
			 Name = "Catherine Abel", Email = "catherine.abel@vannuys.com",
			 Company = "Van Nuys", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA, 98052",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Kim Branch", Email = "kim.branch@contoso.com",
			 Company = "Contoso", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA, 98052",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Frances Adams", Email = "frances.adams@contoso.com",
			 Company = "Contoso", Phone = "541 555 0100",
			 Address = " 1 Microsoft Way, Redmond, WA, 98052",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Mark Harrington", Email = "mark.harrington@datum.com",
			 Company = "A. Datum Corporation", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA, 98052",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Keith Harris", Email = "keith.harris@adventureworks.com", 
			 Company = "Adventure Works", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA, 98052",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Roger Harui", Email = "roger.harui@baldwinmuseum.com",
			 Company = "Baldwin Museum of Art", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA, 98052",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Pilar Pinilla", Email = "pilar.pinilla@blueyonderairlines.com",
			 Company = "Blue Yonder Airlines", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Kari Hensien", Email = "kari.hensien@citypowerlight.com",
			 Company = "City Power & Light", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Johny Porter", Email = "johny.porter@cohowinery.com",
			 Company = "Coho Winery", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "Peter Brehm", Email = "peter.brehm@cohowinery.com",
			 Company = "Coho Winery", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.Customers.Add(new Customer
		{
			 Name = "John Smith", Email = "john.smith@contoso.com",
			 Company = "Contoso", Phone = "541 555 0100",
			 Address = "1 Microsoft Way, Redmond, WA",
			 Image = "Assets/CustomerPlaceholder.png",
			 Title = "Sales"
		});

		context.SaveChanges();
	}
	````

1. Open **Global.asax.cs** and add a reference to **CustomerManager.WebApi.Models**

	<!-- mark:1 -->
	````C#
	using CustomerManager.WebApi.Models;
	````

1. Add the database initializer in the **Application_Start** method in Global.asax.cs.

	<!-- mark:5 -->
	````C#
	protected void Application_Start()
	{
		...
	
		Database.SetInitializer<CustomerContext>(new CustomerContextInitializer());
	}
	````

<a name="Ex2Task3" />
#### Task 3 - Publishing the Customers Web API Service to Windows Azure ####

In this task you will first replace the connection string to use a SQL Database, and then publish the updated Web API service in Windows Azure Web Sites.

1. 	Open **Web.Config** and locate the **connectionStrings** section under the **configuration** section.

	Before publishing the service in Windows Azure Web Sites, you will change the connection string and use the SQL Database you have created in the first task of this exercise.

	You will now replace the default CustomerContext connection string, which is using LocalDB, to target your SQL Database server. To do this, replace the **connectionString** value of the **CustomerContext** connection string with the following value. Replace the placeholders as follows: 

	>**Note**: LocalDB is a version of SQL Server Express, installed by default with Visual Studio 2012 and created specifically for developers. It is very easy to install and requires no management, yet it offers the same T-SQL language, programming surface and client-side providers as the regular SQL Server Express. 

	-**Server  URL:** Complete this value with your server URL. For example: eswngivxru.windows.database.net

	-**Server Name**: This is your server name. For example: eswngivxru

	-**Server Admin User:** Use your server's administrator login.

	-**Password:** Use your server's administrator password.

	-**Database:** Make sure the Initial Catalog value does **NOT** match the name of any existing database in your server. Entity Framework Code First will create the database for you.

	````XML
Server=tcp:[SERVER_URL],1433;Database=CustomersDB;User ID=[SERVER_ADMIN_LOGIN];Password=[SERVER_ADMIN_PASSWORD];Trusted_Connection=False;Encrypt=True;Connection Timeout=30;
	````	
	
	For example:

	`````XML
	<add name="CustomerContext" connectionString="Server=tcp:eswngivxru.windows.database.net,1433;Database=CustomersDB;User ID=User@eswngivxru;Password=...;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"
      providerName="System.Data.SqlClient" />
	```

	>**Note:** You can get your **Server URL** from the SQL Database Server **Dashboard** in the Windows Azure Management portal. For simplicity purposes you are using you server administrator user to connect to the database, however in a production scenario it is recommended that you create another SQL Server user.
	>
	> ![Server's dashboard](images/servers-dashboard.png?raw=true "Server's dashboard")
	
1. Press **CTRL+S** to save the changes.

1. Now that the connection string is targeting your SQL Database, you will publish the service in Windows Azure Web Sites. 

1. Follow the steps in [Task 1 from Exercise 1](#Ex1Task1) to create a new Windows Azure Web Site. You can delete the one created in exercise 1 if you want. Also download its publish profile.

1. Back in Visual Studio, right-click the Web API service project in the Solution Explorer, and select **Publish**.

1. Click **Import**, and import the web site publish profile.

1. Click **Publish** to publish the web service, and wait until the process is completed.

1. In the browser opened, go to **/api/customers** to retrieve the full list of customers.

	>**Note:** Entity Framework will create the database the first time you run the application. You can also access the database tables in Windows Azure portal and check if the data was added.

<a name="Ex2Task4" />
#### Task 4 - Exploring the Windows 8 Style UI Application ####

In this task you will explore the Customer client application, built using a Windows 8 Style UI application Grid Template. You will perform a brief lap around and learn about the main components of a Metro Grid application. 

1. In Visual Studio, expand the **CustomerManager.Metro** project in the Solution Explorer. This is a client Windows 8 Style UI application that displays customers. It is based on the Visual Studio Grid template.

	>**Note:** The Grid application is one of the Visual Studio 2012 available templates for Windows 8 Style UI applications, which contains three pages. The first page displays a group of items in a grid layout. When a group is clicked, the second page shows the details of the selected group. Finally, when an item is selected, the third page shows the item details.

	In this solution you will find a simplified Grid template, which only contains group and detail pages with a custom data model for customers.

	![CustomerManager Windows 8 Style UI Application](images/customermanager-metro-app.png?raw=true "CustomerManager Windows 8 Style UI Application")

	_CustomerManager Windows 8 Style UI Application_

	The main application pages are the following ones:

	-_GroupedCustomersPage:_ Shows the customers in a grid layout

	-_CustomerDetailPage:_ Shows customer's details

	-_NewCustomerPage:_ Adds a new customer


1. Expand the **ViewModels** folder and open **GroupedCustomersViewModel.cs**. 

	The XAML pages of this solution are bound against ViewModels classes that retrieve and prepare the necessary data that will be displayed.
	
	GroupedCustomersViewModel contains an ObservableCollection of CustomerViewModel and a method to retrieve the customers asynchronously (in the next exercise you will complete the CustomersWebApiClient call).


	````C#
    public class GroupedCustomersViewModel : BindableBase
    {
        public ObservableCollection<CustomerViewModel> CustomersList { get; set; }

        public GroupedCustomersViewModel()
        {
            this.CustomersList = new ObservableCollection<CustomerViewModel>();

            this.GetCustomers();
        }

        private async void GetCustomers()
        { 
            IEnumerable<Customer> customers = await CustomersWebApiClient.GetCustomers();

            foreach (var customer in customers)
            {
                this.CustomersList.Add(new CustomerViewModel(customer));                
            }        
        }
    }
	````

	> **Note:** The Windows Runtime now supports using ObservableCollection to set up dynamic bindings so that insertions or deletions in the collection update the UI automatically.

	The GroupedCustomersPage.cs code-behind declares, initializes and binds the view model as follows.

	````C#
	...

	private GroupedCustomersViewModel ViewModel { get; set; }

	public GroupedCustomersPage()
	{            
		this.InitializeComponent();
		this.ViewModel = new GroupedCustomersViewModel();
	}

	... 

	protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
	{            
		this.DataContext = this.ViewModel;
	}

	...
	````

	In the XAML code of this page, each collection is bound to the ViewModel through a CollectionViewSource, that points to the customers list from the ViewModel.

	````XML
	<Page.Resources>
	  <CollectionViewSource
			x:Name="groupedItemsViewSource"
			Source="{Binding CustomersList}"            
			IsSourceGrouped="false" />
	</Page.Resources>

	...	
	````

	Then, each of the page elements (lists, grids, etc.) use the defined collection view source and bind to specific properties.

	<!-- mark:8,17 -->
	````XML
	<GridView
            x:Name="itemGridView"
            AutomationProperties.AutomationId="ItemGridView"
            AutomationProperties.Name="Grouped Items"
            Grid.Row="1"
            Margin="0,-3,0,0"
            Padding="116,0,40,46"
            ItemsSource="{Binding Source={StaticResource groupedItemsViewSource}}"            
            SelectionMode="None"
            IsItemClickEnabled="True"
            ItemClick="CustomerItem_Click">

            <GridView.ItemTemplate>
                <DataTemplate>
                    <Grid HorizontalAlignment="Center" VerticalAlignment="Center" Width="300" Height="225"  Margin="0,0,0,0">
                        <Border Background="{StaticResource ListViewItemPlaceholderBackgroundThemeBrush}">
                            <Image Source="{Binding Image}" Stretch="None" />
	````

<a name="Ex2Task5" />
#### Task 5 - Integrating the Web API Service with the Windows 8 Style UI Application ####

In this task you will bind your Windows 8 Style UI Application against your customer's model retrieving data from the Web API service. You will start by configuring the binding, and then you will modify the application to call the service asynchronously and display the customers.

1. Right-click the **DataModel** project folder in the solution explorer and select **Add | Existing Item**. 

1. Browse to the **CustomerManager.WebApi** project, open the **Models** folder and select **Customer.cs**. Click the arrow next to the **Add** button and click **Add as link**.

	![Adding Customer.cs model class as a link](images/add-customercs-as-an-existing-item.png?raw=true "Adding Customer.cs model class as a link")

	_Adding Customer.cs model class as a link_

	By adding the Customer class as a link, your application will be using the same model class from the Web API service. This class will serve as data contract between the Web API service and the Metro app.

1. Right-click the **DataModel** project folder in the Solution Explorer and select **Add | Class**. Name it _CustomersWebApiClient.cs_. 

	This class will contain the methods that retrieve the customers from the Web API service you have already published on Windows Azure.

	![Adding a new class](images/adding-a-new-class.png?raw=true "Adding a new class")

	_Adding a new class_

1. 	Open the **CustomersWebApiClient.cs** class and add the following using directives. 

	(Code Snippet - _Building Windows 8 Apps - Ex2 - CustomersWebApiClient namespace_)

	<!-- mark:1-4 -->
	````C#
	using System.IO;
	using System.Net.Http;
	using System.Runtime.Serialization.Json;
	using CustomerManager.Models;
	````

	>**Note:** CustomerManager.Models references your service models, and it is necessary to use the customer class you have added as a link.
	
1. In the CustomersWebApiClient class, add the following **GetCustomers()** method.

	(Code Snippet - _Building Windows 8 Apps - Ex2 - CustomersWebApiClient GetCustomers Method_)

	<!-- mark:1-18 -->
	````C#
	public static async Task<IEnumerable<Customer>> GetCustomers()
	{            
		object serviceUrl;
		App.Current.Resources.TryGetValue("ServiceUrl", out serviceUrl);

		using (HttpClient client = new HttpClient())
		{
			 HttpResponseMessage response = await client.GetAsync(serviceUrl as string);

			 response.EnsureSuccessStatusCode();

			 using (var stream = await response.Content.ReadAsStreamAsync())
			 {                    
				  DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(IEnumerable<Customer>));
				  return serializer.ReadObject(stream) as IEnumerable<Customer>;                                       
			 }                
		}            
	}
	````

	This method performs an asynchronous call to the Web API service. After the data is retrieved, the method uses the [DataContractJSonSerializer](http://msdn.microsoft.com/en-us/library/system.runtime.serialization.json.datacontractjsonserializer(v=vs.110\).aspx) to read the array of customers, using the Customers data contract defined.

	The Web API service URL is retrieved from a resources dictionary in App.xaml.

1. Now add the **CreateCustomer** method to post new customers to the service.

	(Code Snippet - _Building Windows 8 Apps - Ex2 - CustomersWebApiClient CreateCustomer Method_)

	<!-- mark:1-21 -->
	````C#
	public static async void CreateCustomer(Customer customer)
	{
		object serviceUrl;
		App.Current.Resources.TryGetValue("ServiceUrl", out serviceUrl);

		using (HttpClient client = new HttpClient())
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Customer));
			
			using (MemoryStream stream = new MemoryStream())
			{                    
				serializer.WriteObject(stream, customer);                    
				stream.Seek(0, SeekOrigin.Begin);
				
				var json = new StreamReader(stream).ReadToEnd();

				var response = await client.PostAsync(serviceUrl as string, new StringContent(json, Encoding.UTF8, "application/json"));
				response.EnsureSuccessStatusCode();
			}
		}
	}
	````

	This method executes an asynchronous post to the Web API service, sending the customer serialized in JSON. 

1. Finally, you will configure the ServiceUrl value using your Web API service URL. Open **App.xaml** and locate the **ServiceURL** key. Change the key value using your service site URL.

	````XML
	<x:String x:Key="ServiceUrl">[YOUR-SERVICE-SITE-URL]/api/customers</x:String>
	````

	>**Note:** You can find your service URL in the  dashboard of your Windows Azure Web Sites.

<a name="Ex2Task6" />
#### Task 6 - A Lap Around the Customer Manager Application ####

1. Press **CTRL+SHIFT+B** to build the solution. Make sure the Windows 8 Style UI application project is selected as the startup project.

1. Press **F5** to run the solution.

	![Customer Manager Grid](images/customer-manager-grid.png?raw=true "Customer Manager Grid")

	_Customer Manager - Grid_

1. Click on a customer to open the **Customer Details** page. Notice that you can click the arrow on the left side of the screen to browse through the customers. 

	Then, click the upper left arrow to go back.

	![Customer Details](images/customer-details.png?raw=true "Customer Details")

	_Customer Details_

1. Back in the Home page, right-click to bring the application bar up and select **Add** to go to the new customer page.

	![Add button](images/add-button.png?raw=true "Add button")

	_Add button_

1. Complete the new customer's data and click **Create**.

	![New Customer](images/new-customer.png?raw=true "New Customer")

	_New Customer_

1. Back in the Home page, you will see the new customer added.

	>**Note:** If you cannot see the new customer added, go to the details page and come back to the home page. As the CreateCustomer method is asynchronous, to avoid blocking the UI, the GetCustomers method might execute before the new customer is posted to the service.

---

<a name="Exercise3" />
### Exercise 3: Adding Push Notification Support to your Windows 8 Style UI Application ###

The Windows Push Notification Services (WNS) enables third-party developers to send toast, tile, and badge updates from their own cloud service. This provides a mechanism to deliver new updates to your users in a power-efficient and dependable way.

The process of sending a notification requires few steps:

1. **Request a channel.** Utilize the WinRT API to request a Channel Uri from WNS. The Channel Uri will be the unique identifier you use to send notifications to an application instance.

1. **Register the channel with your Windows Azure cloud services.** Once you have your channel you can then store your channel and associate it with any application specific data (e.g user profiles and such) until your services decide that it’s time to send a notification to the given channel.

1. **Authenticate against WNS.** To send notifications to your channel URI you are first required to Authenticate against WNS using OAuth2 to retrieve a token to be used for each subsequent notification that you push to WNS.

1. **Push notification to channel recipient.** Once you have your channel, notification payload and WNS access token you can then perform an HttpWebRequest to post your notification to WNS for delivery to your client.

	![WNS Flow Diagram](images/wns-flow-diagram.png?raw=true "WNS Flow Diagram")

	_WNS Flow Diagram_

In this exercise you will learn how to send a toast notification from the Web API service (cloud service) to the registered clients (Windows 8 Style UI applications) whenever a new customer is added.

A toast notification is a transient message to the user that contains relevant, time-sensitive information and provides quick access to related content in an app. It can appear whether you are in another app, the Start screen, the lock screen, or on the desktop. Toasts should be viewed as an invitation to return to your app to follow up on something of interest.

<a name="Ex3Task1" />
#### Task 1 - Registering the Customer Manager Application for Push Notifications####

Before you can send notifications through WNS, you must register your application with the Windows developer center that supports the end-to-end process for submitting, certifying, and managing applications for sale in the Windows Store. When you register your application with the Dashboard, you are given credentials — a Package security identifier (SID) and a secret key — which your cloud service will use to authenticate itself with WNS.

In this task you will obtain the information that will be needed to enable your application to communicate with WNS and Live Connect.

1. In Visual Studio, continue working with the solution obtained from the previous exercise. If you did not executed the previous exsercise you can open **CustomerManager.sln** located in the **Source/Ex3-Notifications/Begin** folder of this lab.

1. If you opened *CustomerManager.sln** located in the **Source/Ex3-Notifications/Begin** folder of this lab, open **Web.config** and configure the **CustomerContext** connection string to point to a Windows Azure SQL Database. You can use the connection string below replacing the placeholders. [Exercise 2 - Task 3](#Ex2Task3) instructs how to do this.

	````XML
	<add name="CustomerContext" connectionString="Server=tcp:[SERVER_URL],1433;Database=CustomersDB;User ID=[SERVER_ADMIN_LOGIN];Password=[SERVER_ADMIN_PASSWORD];Trusted_Connection=False;Encrypt=True;Connection Timeout=30;" providerName="System.Data.SqlClient" />
	````

1. Build the solution.

1. Open the Package Manager Console and execute the following commands to revert Entity Framework to a previous version. MVC 4 is using Entity Framework 5 Beta and the Windows Azure Toolkit for Windows 8 is designed for version 4.3.1. 

	````PowerShell
	Uninstall-Package EntityFramework
	Install-Package EntityFramework -version 4.3.1
	````
	
1. Now you need to remove an Entity Framework custom configuration section in Web.config. Open file and remove the `<entityFramework>` section, near the end of the file.

	````XML
  <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="v11.0" />
      </parameters>
    </defaultConnectionFactory>
  </entityFramework>
	````

1. In the Solution Explorer, open **Package.appxmanifest** from the **CustomerManager.Metro** project.

    > **Note:** The package manifest is an XML document that contains the info the system needs to deploy, display, or update a Windows 8 Style UI app. This info includes package identity, package dependencies, required capabilities, visual elements, and extensibility points. Every application package must include one package manifest.

1. Open the **Packaging** tab and take note of the **Package display name** and **Publisher**.

    ![Packaging tab](images/packaging-tab.png?raw=true "Packaging tab")

    _Packaging tab_

1. Go to <http://manage.dev.live.com/build>.

1. Follow the steps to register your application:

    1. Enter your **Package Display Name** and **Publisher display name**.

		> **Note:** Make sure the Publisher value you provide matches exactly (case included) the Publisher value from Package.appxmanifest.


    1. Click **I accept**.

        ![Register application step 2](images/register-application-step-2.png?raw=true)

        _Registering your application_

    1. Take note of the values from step 3.

        ![Register application step 3](images/register-application-step-3.png?raw=true)

        _Application registered_

> **Note:** To send notifications to this application, your cloud service must use these credentials exactly. You cannot use another cloud service credentials to send notifications to this application, and you cannot use these credentials to send notifications to another app.


<a name="Ex3Task2" />
#### Task 2 - Enabling Push Notifications####

In this task you will configure your application  to be capable of raising toast notifications. Then, you will install NuGet packages with assets to simplify the code required for sending and receiving push notifications.

1. Go back to Visual Studio and paste the **Package name** value in the Packaging tab of the application manifest.
    ![Package name](images/package-name.png?raw=true "Package name")

    _Package name_

1. Select the **Application.UI** tab.

1. Find the **Notifications** section and set **Yes** for **Toast capable**.

    ![Enabling toast notifications](images/enabling-toast-notifications.png?raw=true "Enabling toast notifications")

    _Enabling toast notifications_

1. Switch to the **Capabilities** tab and mark the following capabilities:
    - Home or Work Networking
    - Internet (Client & Server)
    - Internet (Client)

    ![Enabling network capabilities](images/enabling-network-capabilities.png?raw=true "Enabling network capabilities")

    _Enabling network capabilities_

1. Open the **Package Manager Console** from the **Tools | Library Package Manager** menu.

1. In **Default project** select **CustomerManager.Metro**.

1. Execute the following command to install **Windows8.Notifications** package in the Windows 8 Style UI application.

    ````PowerShell
    Install-Package Windows8.Notifications
    ````

    > **Note:** Windows Push Notification Client Recipe (**Windows8.Notifications**) provides a client object to allow open a notification channel from a device, and register it with a notification service at a particular endpoint.

1. In the Package Manager Console, change the default project to **CustomerManager.WebApi**.

1. Execute the following command to install the packages required for the server project.

    ````PowerShell
    Install-Package WnsRecipe
    Install-Package WindowsAzure.Notifications.Sql
    ````

    > **Note:** The WindowsAzure.Notifications.Sql package depends on **WindowsAzure.Notifications** which allows client devices to register (and unregister) for receiving push notifications messages. 
    > The **WindowsAzure.Notifications.Sql** package provides storage in a SQL Database for the Push Notification Registration Cloud Service.
    >
    > The Windows Push Notification Service Recipe (**WnsRecipe**) is a push notification server-side helper library that provides an easy way to send all three types of push notification messages supported by Windows Push Notification Services (WNS): Tile, Toast, and Badge.

These NuGet packages are also available in the [Windows Azure Toolkit for Windows 8](http://watwindows8.codeplex.com/ "Windows Azure Toolkit for Windows 8").
In this toolkit you can also find additional samples and documentation about push notifications. In particular, you may found useful the following resources:

 - [Windows Azure Toolkit for Windows 8 Content](http://watwindows8.codeplex.com/wikipage?title=Project%20Templates%2c%20Samples%20and%20Libraries%20Source%20Code&referringTitle=Documentation)
 - [Raw Notifications Sample – C# and JavaScript](http://watwindows8.codeplex.com/wikipage?title=Raw%20Notifications%20Sample)
 - [Notifications Samples – C# and JavaScript](http://watwindows8.codeplex.com/wikipage?title=Notifications%20Sample%20%E2%80%93%20C%23%20and%20JavaScript)
 - [Push Notification Worker Sample](http://watwindows8.codeplex.com/wikipage?title=Push%20Notification%20Worker%20Sample&referringTitle=Documentation)

<a name="Ex3Task3" />
#### Task 3 - Sending Push Notifications ####

To send a notification, the cloud service must be authenticated through WNS. The first step in this process occurs when you register your application with the Windows Store Dashboard. During the registration process, your application is given a Package security identifier (SID) and a secret key. This information is used by your cloud service to authenticate with WNS.

The WNS authentication scheme is implemented using the client credentials profile from the [OAuth 2.0](http://go.microsoft.com/fwlink/?linkid=226787) protocol. The cloud service authenticates with WNS by providing its credentials (Package SID and secret key). In return, it receives an access token. This access token allows a cloud service to send a notification. The token is required with every notification request sent to the WNS.

1. Open the **CustomersController.cs** file from the CustomerManager.WebApi project and add the following using directives.
    
	(Code Snippet - _Building Windows 8 Apps - Ex3 - Send Notifications Namespaces_)

    <!-- mark:1-4 -->
    ````C#
	using System.Configuration;
	using NotificationsExtensions;
	using NotificationsExtensions.ToastContent;
	using CustomerManager.WebApi.CloudServices.Notifications;
    ````

1. Add the following private method to send a toast notification about the new customers.

	(Code Snippet - _Building Windows 8 Apps - Ex3 - SendNotification_)

    <!-- mark:1-17 -->
    ````C#
    private void SendNotification(Customer customer)
    {
        var clientId = ConfigurationManager.AppSettings["ClientId"];
        var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        var tokenProvider = new WnsAccessTokenProvider(clientId, clientSecret);
        var notification = ToastContentFactory.CreateToastText02();

        notification.TextHeading.Text = "New customer added!";
        notification.TextBodyWrap.Text = customer.Name;

        var provider = NotificationServiceContext.Current.Configuration.StorageProvider;

        foreach (var endpoint in provider.All())
        {
            var result = notification.Send(new Uri(endpoint.ChannelUri), tokenProvider);
        }
    }
    ````

    > **Note:** A channel is a unique address that represents a single user on a single device for a single application or secondary tile. Using the channel URI, the cloud service can send a notification whenever it has an update for the user. With the **NotificationServiceContext** we can get the full list of the client endpoints registered with the cloud service.

1. Find the **PostCustomer** function and add a call to **SendNotification** method.

    <!-- mark:9 -->
    ````C#
	// POST api/Customers
	public HttpResponseMessage PostCustomer(Customer customer)
	{
		if (ModelState.IsValid)
		{
			db.Customers.Add(customer);
			db.SaveChanges();

			this.SendNotification(customer);

			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customer);
			response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customer.CustomerId }));
			return response;
		}
		else
		{
			return Request.CreateResponse(HttpStatusCode.BadRequest);
		}
	}
    ````
 
1. Open the **Web.config** file and add the following settings in the `appSettings` section. Replace the placeholders using the values from the Windows Developer Center obtained on the first task.

    <!-- mark:2-3 -->
    ````XML
      ...
      <add key="ClientId" value="[Package Security Identifier (SID)]"/>
      <add key="ClientSecret" value="[Client secret]"/>
    </appSettings>
    ````

    > **Note:** For demo purposes we simply store these values in the Web.config file, but the Package security identifier SID and client secret should be securely stored. Disclosure or theft of this information could enable an attacker to send notifications to your users without your permission or knowledge.

1. Open **App_Start\NotificationsServiceSql.cs** and replace the code in the **PostStart** method with the following code.
    
	(Code Snippet - _Building Windows 8 Apps - Ex3 - Set Connection String_)

    <!-- mark:7 -->
    ````C#
	public static void PostStart()
	{
		// Configure the SQL database as the storage for the Push Notifications Registration Service.
		NotificationServiceContext.Current.Configure(
			c =>
			{
				var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
				c.StorageProvider = new SqlEndpointRepository(connectionString);
			});
	}
    ````

1. Open **Web.config** and configure the **DefaultConnection** connection string to point to a Windows Azure SQL Database. You can use the connection string below replacing the placeholders. [Exercise 2 - Task 3](#Ex2Task3) instructs how to do this.

	````XML
	<add name="DefaultConnection" connectionString="Server=tcp:[SERVER_URL],1433;Database=CustomersDB.Notifications;User ID=[SERVER_ADMIN_LOGIN];Password=[SERVER_ADMIN_PASSWORD];Trusted_Connection=False;Encrypt=True;Connection Timeout=30;" providerName="System.Data.SqlClient" />
	````

	> **Note:** For simplicity purposes the service is using two different databases for storing application data (customers) and notifications. In a production scenario you might want to use one database by merging the two different Entity Framework contexts.

1. Open **App_Start\NotificationsService.cs** and in the **PostStart** method, comment the line that configures the storage provider as shown below. As you are using the SQL version of the WindowsAzure.Notifications NuGet, which stores notifications data in a SQL database, you should remove this line that configures Windows Azure Storage.

	<!-- mark:12 -->
	````C#	    
	public static void PreStart()
	{
		NotificationServiceContext.Current.Configure(
			 c =>
			 {
				  ...

				  // TODO: Specify a rule for authorizing users when registring (register, unregister)
				  c.AuthorizeRegistrationRequest = AuthorizeUserRequest;

				  // TODO: Replace with your own Windows Azure Storage account name and key, or read it from a configuration file
				  //c.StorageProvider = new WindowsAzureEndpointRepository(CloudStorageAccount.DevelopmentStorageAccount);

				  ...
			 });
	}
	````


1. Publish the Customers Web API service in Windows Azure. To do this, follow the steps in [Exercise 2, Task 3](#Ex2Task3).

<a name="Ex3Task4" />
#### Task 4 - Registering the Notifications Client ####

When an application that is capable of receiving push notifications runs, it must first request a notification channel.
After the application has successfully created a channel URI, it sends it to its cloud service, together with any app-specific metadata that should be associated with this URI.

In this task you will use the class library provided by the **Windows8.Notifications** package to request the channel and register your application with the service when it is launched and unregister it when it is suspended. 

1. Open **App.xaml.cs** from the **CustomerManager.Metro** project and add the following using directive.
    
    ````C#
    using Windows8.Notifications;
    ````

1. Add the following members to the App class. Update the **ServiceEnpointsUrl** value according to the location of the deployed Web API project.

	(Code Snippet - _Building Windows 8 Apps - Ex3 - App Members_)

    <!-- mark:1-5 -->
    ````C#
	private const string ServiceEnpointsUrl = "[YOUR_WEB_API_URL]/endpoints";
	private const string ApplicationId = "CustomerManager";
	private const string DeviceId = "deviceId";

	private NotificationClient notificationClient;
    ````

1. Initialize the **notificationClient** member in the constructor.

	(Code Snippet - _Building Windows 8 Apps - Ex3 - NotificationClient Initialization_)

    <!-- mark:3 -->
    ````C#
	public App()
	{
		this.notificationClient = new NotificationClient(ApplicationId, DeviceId, ServiceEnpointsUrl);
		this.InitializeComponent();
		this.Suspending += OnSuspending;
	}
    ````

1. Call the **Register** function from the **notificationClient** member in the **OnLaunched** event.

	(Code Snippet - _Building Windows 8 Apps - Ex3 - Register Notification Client_)

    <!-- mark:5 -->
    ````C#
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        ...

        await this.notificationClient.Register();
    }
    ````

1. Call the **Unregister** function from the **notificationClient** member in the **OnSuspending** event.

	(Code Snippet - _Building Windows 8 Apps - Ex3 - Unregister Notification Client_)

    <!-- mark:7 -->
    ````C#
    private async void OnSuspending(object sender, SuspendingEventArgs e)
    {
        var deferral = e.SuspendingOperation.GetDeferral();
        await SuspensionManager.SaveAsync();
        deferral.Complete();

        await this.notificationClient.Unregister();
    }
    ````

1. Open **App.xaml** and locate the **ServiceURL** key. Make sure the **ServiceUrl** key value has the URL of the Web API service URL deployed.
	
	````XML
	<x:String x:Key="ServiceUrl">[YOUR-SERVICE-SITE-URL]/api/customers</x:String>
	````

1. Run the Windows 8 Style UI application.

1. Create a new customer and notice the toast notification.

    ![Toast notification](images/toast-notification.png?raw=true "Toast notification")

    _Toast notification_

---
