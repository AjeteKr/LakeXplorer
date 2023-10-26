using LakeXplorer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LakeXplorer.Repository;
using LakeXplorer.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Add scoped repository services
builder.Services.AddSingleton(new TokenService(60)); // Add a singleton TokenService with a token expiration of 60 minutes


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));// Configure the database context using the connection string


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // Allow non-HTTPS requests
        options.SaveToken = true; // Save the JWT token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Validate the issuer signing key
            IssuerSigningKey = TokenService.SigningKey, // Use the signing key from TokenService
            ValidateIssuer = false, // Do not validate the issuer
            ValidateAudience = false // Do not validate the audience
        }; 
    }); // Configure JWT authentication

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Enable authentication
app.UseAuthorization();// Enable authorization
app.MapControllers();
app.Run();
