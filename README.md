# Asp.NetCoreApi_Identity_bug
explain bug in asp.net core WebApi with Entity framework core
when test with JwtBearerDefaults.AuthenticationScheme get this
![image](https://user-images.githubusercontent.com/20041519/224171999-c8cf04d8-7cf7-4c32-a9d6-06cf8a7817c8.png)

when test with
options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
work normal
![image](https://user-images.githubusercontent.com/20041519/224172334-3f6d1190-e014-435f-8f74-82e6dbc09a68.png)

and if test with JwtBearerDefaults.AuthenticationScheme but remove this
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

work normal
![image](https://user-images.githubusercontent.com/20041519/224172677-19989253-f695-486f-96d9-6c5a92e860de.png)
