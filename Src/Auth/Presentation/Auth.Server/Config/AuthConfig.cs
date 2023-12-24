using Auth.Common.Constants;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace Auth.Server.Config {
    public static class AuthConfig {
        public static IEnumerable<IdentityResource> IdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        public static IEnumerable<Client> Clients() => new List<Client> {
            new Client
            {
                ClientId = ClientConstants.ClientId,
                ClientSecrets = new []{ new Secret(ClientConstants.ClientSecret.Sha512())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "AccountApi.scope" }
            },
            new Client
            {
                ClientId = ClientConstants.GameManagerClientId,
                ClientSecrets =new []{ new Secret(ClientConstants.ClientSecret.Sha512())},
                AllowedGrantTypes=GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,"GameManager.scope"}
            }

        };
        public static IEnumerable<ApiScope> ApiScopes() => new List<ApiScope>
        {
            new ApiScope("AccountApi.scope","Account Api"),
            new ApiScope("GameManager.scope","GameManager Api")

        };
        public static IEnumerable<ApiResource> ApiResources() => new List<ApiResource>
        {
            new ApiResource("AccountApi","Account Api")
            {
                Scopes={"AccountApi.scope"}
            },
            new ApiResource("GameManagerApi","GameManager Api")
            {
                Scopes={"GameManager.scope"}
            }
        };
        public static List<TestUser> TestUsers() => new List<TestUser> {
        new TestUser
            {
                SubjectId ="testsubject1",
                Username="hermoni",
                Password="hermoniPass",
                Claims=new List<Claim>
                {
                    //orher365@gmail.com
                    //orwwwe@gmail.com
                    new Claim("given_name","hermoni"),
                    new Claim("last_name","hermoni")
                }
        },
        new TestUser
            {
                SubjectId ="testsubject2",
                Username="hermoni2",
                Password="hermoniPass2",
                Claims=new List<Claim>
                {
                    new Claim("given_name","hermoni2"),
                    new Claim("last_name","mr")
                }
            }
        };

    }
}
