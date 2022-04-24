﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using NashGramAPI.Model;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace NashGramAPI.API;
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    #region Property  
    readonly IUserService _userService;
    #endregion

    #region Constructor  
    public BasicAuthenticationHandler(IUserService userService,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
        _userService = userService;
    }
    #endregion

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string username = null;
        string password = null;
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
            username = credentials.FirstOrDefault();
            password = credentials.LastOrDefault();

            if (!_userService.ValidateCredentials(username, password))
                throw new ArgumentException("Invalid credentials");
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
        }

        var claims = new[] {
                new Claim(ClaimTypes.Name, _userService.GetIdAccount(username, password).ToString())
            };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

}