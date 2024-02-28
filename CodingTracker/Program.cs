using Dapper;
using System.Configuration;
using System.Collections.Specialized;

//var connectionString = ConfigurationManager.AppSettings.Get("connectionString") + ConfigurationManager.AppSettings.Get("databasePath");

var controller = new CodingController();
controller.DatabaseCreation();
controller.StartSession();
controller.GetRecords();