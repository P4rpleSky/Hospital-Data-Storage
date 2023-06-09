using AutoMapper;
using Kosta_Task;
using Kosta_Task.DbContexts;
using Kosta_Task.Repository;
using Kosta_Task.Repository.Interfaces;
using Kosta_Task.Services;
using Kosta_Task.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

SD.BaseUrl = builder.Configuration["BaseUrl"];

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddHttpClient<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Department}/{action=DepartmentIndex}/{id?}");

app.Run();
