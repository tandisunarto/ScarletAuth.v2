using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace ScarletAuth.STS;

public class TestUsers
{
	public static List<TestUser> Users
	{
		get
		{
			var address = new
			{
				street_address = "One Hacker Way",
				locality = "Heidelberg",
				postal_code = 69118,
				country = "Germany"
			};
			
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "E74F36AC-6EFF-4A3C-BD26-931F88827B03",
					Username = "alice",
					Password = "Pass123$",
					Claims =
					{
						new Claim(JwtClaimTypes.Name, "Alice Smith"),
						new Claim(JwtClaimTypes.GivenName, "Alice"),
						new Claim(JwtClaimTypes.FamilyName, "Smith"),
						new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
						new Claim(JwtClaimTypes.WebSite, "http://tandi.com"),
						new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
					}
				},
				new TestUser
				{
					SubjectId = "1EC4DAE8-C60E-43AD-A8A1-2685AB74F89B",
					Username = "bob",
					Password = "Pass123$",
					Claims =
					{
						new Claim(JwtClaimTypes.Name, "Bob Smith"),
						new Claim(JwtClaimTypes.GivenName, "Bob"),
						new Claim(JwtClaimTypes.FamilyName, "Smith"),
						new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
						new Claim(JwtClaimTypes.WebSite, "http://tandi.com"),
						new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
					}
				},
				new TestUser
				{
					SubjectId = "62E1B38A-1EA9-40C8-9245-B6C564A0F112",
					Username = "admin",
					Password = "Pass123$",
					Claims =
					{
						new Claim(JwtClaimTypes.Name, "Administrator"),
						new Claim(JwtClaimTypes.GivenName, "Admin"),
						new Claim(JwtClaimTypes.FamilyName, "Admin"),
						new Claim(JwtClaimTypes.Email, "Admin@email.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
						new Claim(JwtClaimTypes.WebSite, "http://tandi.com"),
						new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
					}
				},
			};
		}
	}
}
