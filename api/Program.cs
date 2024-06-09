
var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();

builder.AddDataContext();

builder.AddDocumentation();

builder.AddIdentity();

builder.AddServices();

var app = builder.Build();

if(app.Environment.IsDevelopment()) 
    app.ConfigureDevEnvironment()
;

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapController();

app.UseHttpsRedirection();

app.Run();
