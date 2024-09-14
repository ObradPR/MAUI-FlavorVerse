using FlavorVerse.BusinessLogic.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace FlavorVerse.Extensions
{
    public static partial class Extensions
    {
        public static UserDto GetUser(this ISecureStorage storage)
        {
            var token = Task.Run(async () => await storage.GetAsync("token")).Result;
            var userDisplayName = Task.Run(async () => await storage.GetAsync("userDisplayName")).Result;

            if (token == null)
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var id = tokenS.Claims.First(_ => _.Type == "nameid").Value;
            var exp = tokenS.Claims.First(_ => _.Type == "exp").Value;

            long expTimestamp = long.Parse(exp);

            DateTime d = UnixTimeStampToDateTime(expTimestamp);

            if (d < DateTime.Now)
            {
                SecureStorage.Default.Remove("token");
                return null;
            }

            return new UserDto { Id = Guid.Parse(id), Token = token, DisplayName = userDisplayName };
        }
    }
}