using FilmWebAPI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace FilmWebAPI.Requests.Post
{
    public class Login : RequestBase<LoginResult>
    {
        public Login(string username, string password) : base(Signature.Create("login", username, password, "1"), FilmWebHttpMethod.Post)
        {
        }

        public override async Task<LoginResult> Parse(HttpResponseMessage responseMessage)
        {
            var apiResponse = await GetAsApiResponseAsync(responseMessage);
            if (!apiResponse.IsSucceed)
            {
                return new LoginResult(false, null);
            }

            var json = apiResponse.ToJson<dynamic>();

            var user = new User
            {
                Nick = json[0],
                Name = json[2],
                Id = json[3],
                Sex = json[4].ToString().Equals("M") ? Sex.Male : Sex.Female,
                Birthday = json[5],
            };

            return new LoginResult(true, user);
        }
    }
}