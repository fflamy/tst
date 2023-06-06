using ApiProject.Buisness;
using ApiProject.DAL;
using ApiProject.WebApi.Serializers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.ModelBinderProviders.Add(new CustomKeyValuePairBinderProvider()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(BusinessLayer).Assembly);
builder.Services.AddSwaggerGen(c =>
{
	c.SchemaFilter<KeyValuePairListSchemaFilter>();
});

builder.Services.AddMediatR(cfg =>
	 cfg.RegisterServicesFromAssembly(typeof(BusinessLayer).Assembly));
var conntectionistring = builder.Configuration.GetConnectionString("myDb1");
builder
	.Services
	.AddDbContext<ApiDbContext>(opt => opt.UseSqlite(conntectionistring));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	using var context = scope.ServiceProvider.GetService<ApiDbContext>();
	context!.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
