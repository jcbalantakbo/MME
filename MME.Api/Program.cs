using Microsoft.EntityFrameworkCore;
using MME.Application.Interfaces;
using MME.Application.Services;
using MME.Persistence.Context;
using MME.Persistence.Interfaces;
using MME.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IMobileBlacklistService, MobileBlacklistService>();
builder.Services.AddScoped<IEmailBlacklistService, EmailBlacklistService>();

// Register repositories
builder.Services.AddScoped<IMobileBlacklistRepository, MobileBlacklistRepository>();
builder.Services.AddScoped<IEmailDomainBlacklistRepository, EmailDomainBlacklistRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Register DbContext and configure connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services
builder.Services.AddScoped<ICalculateRepayments, RepaymentCalculator>();
builder.Services.AddScoped<IUniqueIdentifierGeneratorService, IdGeneratorService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

