﻿using IdentityModel.Client;
using Microsoft.Extensions.Options;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.API.Test.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PhaseCredit.API.Test.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;

        public TokenService(ILogger<TokenService> logger, IOptions<IdentityServerSettings> identityServerSettings)
        {
            _logger = logger;
            _identityServerSettings = identityServerSettings;

            using var httpClient = new HttpClient();
            _discoveryDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscoveryUrl).Result;
            if (_discoveryDocument.IsError)
            {
                logger.LogError($"Unable to get discovery document. Error is: {_discoveryDocument.Error}");
                throw new Exception("Unable to get discovery document", _discoveryDocument.Exception);
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            using var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,

                ClientId = _identityServerSettings.Value.ClientName,
                ClientSecret = _identityServerSettings.Value.ClientPassword,
                Scope = scope
            });


            if (tokenResponse.IsError)
            {
                _logger.LogError($"Unable to get token. Error is: {tokenResponse.Error}");
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }

            return tokenResponse;
        }
        

       public async Task<bool> IsAccessTokenValid(string accessToken)
        {
            var result = true;
            //try
            //{
            //    var handler = new JwtSecurityTokenHandler();
            //    handler.ValidateToken(accessToken);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            return await Task.FromResult(result);
        }
    }
}
