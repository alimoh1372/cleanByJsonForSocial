using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebUI.Services;


/// <summary>
/// Implementation
/// </summary>
public class AuthHelper : IAuthHelper
{
    private readonly Jwt _jwtSetting;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthHelper(IOptions<Jwt> jwtSetting, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtSetting = jwtSetting.Value;
    }

    /// <summary>
    /// Create a token from model you give
    /// </summary>
    /// <param name="authViewModel">a model to make claims</param>
    /// <returns></returns>
    public Task<string> CreateToken(AuthViewModel authViewModel)
    {
        var tokenDescriptor = SecurityTokenDescriptorBuilder(authViewModel,_jwtSetting);
        JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

        //Seciroty token 
        SecurityToken? securityToken = securityTokenHandler.CreateToken(tokenDescriptor);
        string token = securityTokenHandler.WriteToken(securityToken);
        return Task.FromResult(token);
    }

    private SecurityTokenDescriptor SecurityTokenDescriptorBuilder(AuthViewModel authViewModel,Jwt jwtSetting)
    {
        //Make encrypt key template
        var eckeyTemplate = Encoding.UTF8.GetBytes(jwtSetting.EncryptKey);
        //when we choose a 256 bit encrypt alghorihthm such as SecurityAlgorithms.Aes256KW,SecurityAlgorithms.Aes256CbcHmacSha512
        //so we must change the byte array to the 256/8 byte arraye
        byte[] EcryptKey = new byte[256 / 8];

        //copy the key to the current byte format

        Array.Copy(eckeyTemplate,EcryptKey,256/8);
        SymmetricSecurityKey encryptSymmetrciSecurityKey = new SymmetricSecurityKey(EcryptKey);
        EncryptingCredentials encryptKey = new EncryptingCredentials(encryptSymmetrciSecurityKey,
            SecurityAlgorithms.Aes256KW
            , SecurityAlgorithms.Aes256CbcHmacSha512);


        //Making the signature key ready

        var signatureEncodingKey = Encoding.UTF8.GetBytes(jwtSetting.SigningKey);

        var signatureSymmetricSecurityKey = new SymmetricSecurityKey(signatureEncodingKey);

        SigningCredentials signatureSigningKey =
            new SigningCredentials(signatureSymmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);


        //building the token descriptor:that specify the properties of a token that will make in future
        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
        {
            EncryptingCredentials = encryptKey,
            Expires = DateTime.Now.AddMinutes(jwtSetting.ExpireTimeInMinute),
            Issuer = jwtSetting.Issuer,
            IssuedAt = DateTime.Now.AddSeconds(-10),
            NotBefore = DateTime.Now.AddSeconds(-11),
            TokenType = "JWT",
            SigningCredentials = signatureSigningKey,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("UserId", authViewModel.Id.ToString()),
                new(ClaimTypes.Name, authViewModel.Username),
                new(ClaimTypes.Email, authViewModel.Username)
            })
        };
        return securityTokenDescriptor;

    }
    public async Task<AuthViewModel> GetUserInfo()
    {
        if (await IsAuthenticated())
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == "UserId")?.Value;
            string userName = _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;
            var authViewModel = new AuthViewModel(Convert.ToInt32(userId), userName);
            return authViewModel;
        }

        return null!;
    }

    public async Task<bool> IsAuthenticated()
    {
        if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated != null)
            return await Task.FromResult(_httpContextAccessor.HttpContext.User.Identity
                .IsAuthenticated);
        return false;
    }
}