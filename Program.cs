using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.Security.Resources;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerSideApp.Services;
using ServerSideApp.Data;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using DevExpress.AspNetCore.Reporting.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDevExpressControls();
builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();

#region AddServices ApplicationCore
/*string filePath = "connection.txt";
string cleun = "oussama007htsoft";
string cledeux = "htsoftoussama007";
string fileContent = File.ReadAllText(filePath);
Console.WriteLine(fileContent);
string[] table = fileContent.Split(";");
string username = CommandeClientCleanArch.WebApi.Controllers.Crypt.Decrypt(table[0], cleun, cledeux);
string password = CommandeClientCleanArch.WebApi.Controllers.Crypt.Decrypt(table[1], cleun, cledeux);
string host = table[2];
string bd = table[3];
string port = table[4];
string connectionString = " server = " + host + "; port = " + port + "; database = " + bd + "; UID = " + username + "; PASSWORD = " + password + "; persistsecurityinfo = True;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 19)))); //mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend

builder.Services.AddScoped<ITransaction, Transaction>();

builder.Services.AddScoped(typeof(IAppLogger), typeof(LoggerAdapter));
builder.Services.AddScoped<IWebUserServices, WebUserServices>();

//services.AddScoped<IParametersBySociete, ParametersBySociete>(); 
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));//each time new value

builder.Services.AddScoped<IProcessusServices, ProcessusServices>();*/
#endregion

builder.Services.Configure<LoggerOptions>(logOptions => {
    logOptions.LogMachineName = true;
    logOptions.Prefix = "START: ";
    logOptions.LogTimeStamp = true;
    logOptions.Suffix = " :END";
});
// Uncomment the following line if you run the Custom Export example.
builder.Services.AddSingleton<WebDocumentViewerOperationLogger, MyOperationLogger>();
builder.Services.AddMvc();
builder.Services.ConfigureReportingServices(configurator => {
    if(builder.Environment.IsDevelopment()) {
        configurator.UseDevelopmentMode();
    }
    configurator.ConfigureReportDesigner(designerConfigurator => {
    });
    configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
        viewerConfigurator.UseCachedReportSourceBuilder();
        DeserializationSettings.RegisterTrustedClass(typeof(CommandeClientCleanArch.Reporting.Data.Dsprocessus));
        DeserializationSettings.RegisterTrustedAssembly(typeof(CommandeClientCleanArch.Reporting.Data.Dsprocessus).Assembly);
        //viewerConfigurator.RegisterConnectionProviderFactory<CustomSqlDataConnectionProviderFactory>();
    });
});
//builder.Services.AddDbContext<ReportDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ReportsDataConnectionString")));

/*
  services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyCorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                //a rev�rifier
                //options.AddPolicy("AllowAngularDevClient", builder => builder
                //.WithOrigins("http://localhost:4200")
                //.AllowAnyHeader()
                //.AllowAnyMethod());
            });
 */
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyCorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    //a rev�rifier
    //options.AddPolicy("AllowAngularDevClient", builder => builder
    //.WithOrigins("http://localhost:4200")
    //.AllowAnyHeader()
    //.AllowAnyMethod());
});


var app = builder.Build();
//using(var scope = app.Services.CreateScope()) {    
//    var services = scope.ServiceProvider;    
//    services.GetService<ReportDbContext>().InitializeDatabase();
//}
var contentDirectoryAllowRule = DirectoryAccessRule.Allow(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "Content")).FullName);
AccessSettings.ReportingSpecificResources.TrySetRules(contentDirectoryAllowRule, UrlAccessRule.Allow());
app.UseDevExpressControls();
System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
if(app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
} else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAnyCorsPolicy");

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();