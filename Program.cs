var builder = WebApplication.CreateBuilder(args);

var apellido = builder.Configuration.GetValue<string>("apellido");

//Inicio de área de los servicios


//Fin de área de los servicios
var app = builder.Build();
//Inicio de área de los middleware

app.MapGet("/", () => $"Hello World! {apellido}");

//Fin de área de los middleware
app.Run();
