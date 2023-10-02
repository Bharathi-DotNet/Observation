using Enquiry.Web.Keys;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace Enquiry.Web.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAsymmetricAuthentication(this IServiceCollection services)
    {
        var issuerSigningCertificate = new SigningIssuerCertificate();
        RsaSecurityKey issuerSigningKey = issuerSigningCertificate.GetIssuerSigningKey();
        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultChallengeScheme = "JWT_OR_COOKIE";
            authOptions.DefaultScheme = "JWT_OR_COOKIE";
            //authOptions.RequireAuthenticatedSignIn = false;
        })
            .AddCookie("Cookies", options =>
            {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Home/Error";
                options.LogoutPath = "/Login";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidIssuer = "https://localhost:44365",
                    //ValidAudience = "https://localhost:44365",
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    LifetimeValidator = LifetimeValidator
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        //context.Token = context.Request.Cookies["eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiS0YwMDAyIiwicm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE2NTMzMjM0MzIsImV4cCI6MTY1MzkyODE4NSwiaWF0IjoxNjUzMzIzNDMyfQ.Aexx6FgXsh6AA5iDlNA-wmPN9nqoKeoYx5ZHd9PSp_jLfsSKpE9KO8jP-b6ZlcpSU_wHB0rd0MKaqkjkNg67YWwYovAh7Ev1wsZEHlrnprgtam_bkHOAUQ4ipawQjptuLrcUTrajHN5_KMXS95HM_Peml_Yb_CoriV9Nz9cF6mXiH0dFv1zDKHOZb_Upn9ul-Sb_u1UPsIIX-DttK7jm5KiA8Ki6pTcucdhEci312cgn0V5M11JUhCDHRw2nzo3dNRjxvLHkPBuce7qkfgm85UTP4GbthjRglb3nTm01XNGWcQbvDRu4I8Iu7pAYM1l_8im4yXAmm6pdxdzXK_Gpcg"];
                        return Task.CompletedTask;
                    }
                };
            }).AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
            {
                // runs on each request
                options.ForwardDefaultSelector = context =>
                {
                    // filter by auth type
                    string authorization = context.Request.Headers[HeaderNames.Authorization];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        return JwtBearerDefaults.AuthenticationScheme;

                    //context.Request.Headers.Add("Cookie", context.Request.Cookies.ToString());
                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            });

        return services;
    }

    private static bool LifetimeValidator(DateTime? notBefore,
            DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
    {
        return expires != null && expires > DateTime.UtcNow;
    }
}
