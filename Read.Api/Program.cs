using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Read.Api.Auth;
using Read.Api.Mapping;
using Read.Api.Swagger;
using Read.Application;
using Read.Application.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var config = builder.Configuration;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        ValidateIssuer = true,
        ValidateAudience = true
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AuthConstants.AdminUserPolicyName, p => p.AddRequirements(new AdminAuthRequirement(config["ApiKey"]!)))
    .AddPolicy(AuthConstants.TrustedMemberPolicyName, p => p.RequireAssertion(c =>
        c.User.HasClaim(m => m is { Type: AuthConstants.AdminUserClaimName, Value: "true" }) ||
        c.User.HasClaim(m => m is { Type: AuthConstants.TrustedMemberClaimName, Value: "true" })));

builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1.0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
}).AddMvc().AddApiExplorer();

builder.Services.AddOutputCache(x =>
{
    x.AddBasePolicy(c => c.Cache());
    x.AddPolicy("BookCache", c =>
        c.Cache()
            .Expire(TimeSpan.FromMinutes(1))
            .Tag("books"));
});

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(x => x.OperationFilter<SwaggerDefaultValues>());

builder.Services.AddApplication(config);

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.Jwt));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.MapHealthChecks("_health");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();
app.UseOutputCache();

app.UseMiddleware<ValidationMappingMiddleware>();
app.MapControllers();