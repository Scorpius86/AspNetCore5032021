// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using Net5.AspNet.MVC.Infrastructure.Constants;
using System.Collections.Generic;
using System.Security.Claims;

namespace Net5.AspNet.MVC.IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources = new IdentityResource[]{
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "Net5.AspNet.MVC",
                DisplayName = "Net5.AspNet.MVC OIDC Profile",
                Description = "Your Net5 Aps.Net MVC OIC profile information (full name, careerstarted, role, permission)",
                Emphasize = true,
                UserClaims = { SecurityClaimType.GrantAccess,ClaimTypes.Role, JwtClaimTypes.Role, JwtClaimTypes.Name}
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },

                new Client
                {
                    ClientId = "Net5.AspNet.MVC.Client",
                    ClientName = "Net5.AspNet.MVC.Client",
                    ClientSecrets = { new Secret("o90IbCACXKUkunXoa18cODcLKnQTbjOo5ihEw9j58+8=") },
                    AllowedGrantTypes =  { GrantType.AuthorizationCode},
                    RequirePkce = true,
                    AllowedScopes = { "openid","profile","email", "Net5.AspNet.MVC" },
                    AccessTokenType = AccessTokenType.Reference,
                    RedirectUris = {"https://localhost:44342/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44342/signout-callback-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44342/signout-oidc" ,

                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = "https://localhost:44342/Account/SignOut"
                }
            };
    }
}