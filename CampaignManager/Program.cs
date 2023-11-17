using CampaignManager.Services;
using DB;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 
builder.Services.AddDbContext<CampaignManagerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var dbOptions = new DbContextOptionsBuilder<CampaignManagerContext>()
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .Options;

builder.Services.AddSingleton(ctx => new Func<CampaignManagerContext>(() => new CampaignManagerContext(dbOptions)));
builder.Services.AddTransient<ICampaignsService, CampaignsService>();
builder.Services.AddTransient<ITemplatesService, TemplatesService>();
builder.Services.AddTransient<ICampaignConditionsService, CampaignConditionsService>();
builder.Services.AddTransient<ISheduleComposeService, SheduleComposeService>();
builder.Services.AddTransient<ICampaignPickService, CampaignPickService>();


builder.Services.AddSingleton<IResultSaverService, ResultSaverService>();

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
