using Library.Repositories.Database.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Web;

namespace Library.Services.Handlers;

  public class DatabaseTokenAuthOptions : AuthenticationSchemeOptions
  {
      public string TokenHeaderName { get; set; } = AuthentificationKeys.UserAuthTokenKey;
  }

  public class DatabaseTokenAuthHandler : AuthenticationHandler<DatabaseTokenAuthOptions>
  {
      private readonly IAuthorizationTokenRepository _authTokenRepository;

      public DatabaseTokenAuthHandler(
          IAuthorizationTokenRepository authTokenRepository,
          IOptionsMonitor<DatabaseTokenAuthOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          Microsoft.AspNetCore.Authentication.ISystemClock clock)
      : base(options, logger, encoder, clock)
      {
          _authTokenRepository = authTokenRepository;
      }

      protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
      {
          if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
          {
              return await Task.FromResult(AuthenticateResult.Fail($"Missing Header For Token: {Options.TokenHeaderName}\nUri: {Request.Path}"));
          }

          var token = Request.Headers[Options.TokenHeaderName];
          token = HttpUtility.UrlDecode(token);
          var tokenModel = await _authTokenRepository.GetByTokenAsync(token);

          if (tokenModel == null)
          {
              return await Task.FromResult(AuthenticateResult.Fail($"Your token invalid: {Options.TokenHeaderName}\nUri: {Request.Path}"));
          }
          return await Authenticate(tokenModel);
      }

      protected async Task<AuthenticateResult> Authenticate(AuthorizationToken? tokenModel)
      {
          var isStaticToken = tokenModel.IsStatic;
          if (!isStaticToken && tokenModel.Expiration < DateTime.UtcNow)
          {
              _authTokenRepository.Delete(tokenModel);
              return await Task.FromResult(AuthenticateResult.Fail($"Your token expired and was deleted: {Options.TokenHeaderName}\nUri: {Request.Path}"));
          }

          var userRoleId = tokenModel.User.RoleId;

          var claims = new List<Claim>()
          {
              new Claim(ClaimTypes.Sid, tokenModel.Id.ToString()),
              new Claim(ClaimTypes.UserData, tokenModel.UserId.ToString()),
              new Claim(ClaimTypes.Expiration, tokenModel.Expiration.ToString()),
              new Claim(ClaimTypes.Hash, tokenModel.Token),
              new Claim(ClaimTypes.Role, userRoleId.ToString())
          }; 

          var id = new ClaimsIdentity(claims, Scheme.Name);
          var principal = new ClaimsPrincipal(id);
          var ticket = new AuthenticationTicket(principal, Scheme.Name);
          return await Task.FromResult(AuthenticateResult.Success(ticket));
      }
  }

  public class CookieAuthHandler : DatabaseTokenAuthHandler
  {
      readonly IAuthorizationTokenRepository _authTokenRepository;

      public CookieAuthHandler(
          IAuthorizationTokenRepository authTokenRepository,
          IOptionsMonitor<DatabaseTokenAuthOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          Microsoft.AspNetCore.Authentication.ISystemClock clock) : base(authTokenRepository, options, logger, encoder, clock)
      {
          _authTokenRepository = authTokenRepository;
      }

      protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
      {
          var token = Request.Cookies["accessToken"];
          token = HttpUtility.UrlDecode(token);
          var tokenModel = await _authTokenRepository.GetByTokenAsync(token);
          if (tokenModel == null)
              return await base.HandleAuthenticateAsync();

          return await Authenticate(tokenModel);
      }
  }