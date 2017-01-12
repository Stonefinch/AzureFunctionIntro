// note: more info https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference-csharp

// note: you can reference other csx files this way, but the experience is better if you keep all code in the same file
// #load "QueueMessage.csx"

// The following assemblies are automatically added by the Azure Functions hosting environment:
////#r "mscorlib"
////#r "System"
////#r "System.Core"
////#r "System.Xml"
////#r "System.Net.Http"
////#r "Microsoft.Azure.WebJobs"
////#r "Microsoft.Azure.WebJobs.Host"
////#r "Microsoft.Azure.WebJobs.Extensions"
////#r "System.Web.Http"
////#r "System.Net.Http.Formatting"

// In addition, the following assemblies are special cased and may be referenced by simplename (e.g. #r "AssemblyName"):
////#r "Newtonsoft.Json"
////#r "Microsoft.WindowsAzure.Storage"
////#r "Microsoft.ServiceBus"
////#r "Microsoft.AspNet.WebHooks.Receivers"
////#r "Microsoft.AspNEt.WebHooks.Common"
////#r "Microsoft.Azure.NotificationHubs"

// The following namespaces are automatically imported and are therefore optional:
////using System;
////using System.Collections.Generic;
////using System.IO;
////using System.Linq;
////using System.Net.Http;
////using System.Threading.Tasks;
////using Microsoft.Azure.WebJobs;
////using Microsoft.Azure.WebJobs.Host;

// if additional external dependencies are needed, you can included nuget packages using a project.json file
// or you can include external dlls under the /bin folder and reference them like `#r "MyAssembly.dll"`

using System;

using Dapper;
using System.Data.SqlClient;
using System.Configuration;

public static void Run(QueueMessage myQueueItem, TraceWriter log)
{
    log.Info($"Run start {DateTime.UtcNow}");

    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();

        connection.Execute("insert into dbo.AzureFunctionQueueTrigger (CreateDateTime, Id) values (@CreateDateTime, @Id)", new { CreateDateTime = DateTime.UtcNow, Id = myQueueItem.Id });
    }

    log.Info($"Run end {DateTime.UtcNow}");
}

public class QueueMessage
{
    public Guid Id { get; set; }
}
