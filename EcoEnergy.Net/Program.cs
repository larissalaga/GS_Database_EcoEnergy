using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using EcoEnergy.Mappings;
using EcoEnergy.Data;
using EcoEnergy.Repositories.Interfaces;
using EcoEnergy.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);


// Registrando o AutoMapper com o perfil de mapeamento
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // Ensures caching for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set timeout for session data if needed
    options.Cookie.HttpOnly = true; // Makes the session cookie inaccessible to client-side scripts
    options.Cookie.IsEssential = true; // Ensures the session is always available
});
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
builder.Services.AddScoped<ISimulacaoRepository, SimulacaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
builder.Services.AddScoped<IProceduresRepository, ProceduresRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "proceduresRoute",
    pattern: "Procedures/MenuProcedures",
    defaults: new { controller = "Procedures", action = "MenuProcedures" }
);

app.MapControllerRoute(
    name: "proceduresRoute",
    pattern: "Procedures/EmpresaView",
    defaults: new { controller = "Procedures", action = "EmpresaView" }
);
app.MapControllerRoute(
    name: "proceduresRoute",
    pattern: "Procedures/EnderecoView",
    defaults: new { controller = "Procedures", action = "EnderecoView" }
);
app.MapControllerRoute(
    name: "proceduresRoute",
    pattern: "Procedures/OrcamentoView",
    defaults: new { controller = "Procedures", action = "OrcamentoView" }
);
app.MapControllerRoute(
    name: "proceduresRoute",
    pattern: "Procedures/SimulacaoView",
    defaults: new { controller = "Procedures", action = "SimulacaoView" }
);
app.MapControllerRoute(
    name: "proceduresRoute",
    pattern: "Procedures/UsuarioView",
    defaults: new { controller = "Procedures", action = "UsuarioView" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
