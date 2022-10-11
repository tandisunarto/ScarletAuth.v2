using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace ScarletAuth.IDP.Data;

public class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new[]
			{
				new 
				{
					street_address = "One Hacker Way",
					locality = "Heidelberg",
					postal_code = 69118,
					country = "Germany"
				},
				new 
				{
					street_address = "Rockville Pike",
					locality = "Rockville",
					postal_code = 20852,
					country = "United States"
				}
			};
            
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "3eddbd04-3658-47ca-9515-99a3b3be4d88",
                    Username = "alice",
                    Password = "Pass123$",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "alice.smith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address[0]), IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser
                {
                    SubjectId = "a9c53aa3-24ac-4f24-aaf2-755cf6a2d1e3",
                    Username = "bob",
                    Password = "Pass123$",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "bob.smith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address[0]), IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
				new TestUser
                {
                    SubjectId = "cbbf48ae-5c1d-4835-82a0-ab8dc9695c04",
                    Username = "tandi",
                    Password = "Pass123$",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Tandi Sunarto"),
                        new Claim(JwtClaimTypes.GivenName, "Tandi"),
                        new Claim(JwtClaimTypes.FamilyName, "Sunarto"),
                        new Claim(JwtClaimTypes.Email, "tandi.sunarto@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://tandi.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address[1]), IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}