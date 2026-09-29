using ChronoTrigger.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// "ADICIONAR" FRONTEND

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("AllowAll", policy =>
    {//http default- Trocar caso necessário
        policy.WithOrigins("http://localhost:52482")
            .AllowAnyHeader()
            .AllowAnyMethod();


    }
        );


}


);

// ADICIONAR CONTROLLER
builder.Services.AddControllers();

// DB CONTEXT NO MYSQL - NOTA> LOGIN E SENHA ADMIN ESTÁ ABAIXO- QUANDO COLOCAR CREDENCIAIS, RETIRAR ISSO ABAIXO
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Port=3306;Database=activity_tracker;User=Admin;Password=AdminPassword;";

//get para pegar Mysql
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
);

var app = builder.Build();

app.UseCors("AllowAll");


//app.Https Redirect
app.UseAuthorization();
app.MapControllers();

app.Run();