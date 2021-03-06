﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilmWebAPI.Requests.Get
{
    class GetFilmVotesAvg : RequestBase<double>
    {
        private const int AVG_RATE_INDEX = 2;

        public GetFilmVotesAvg(ulong movieId)
            : base(Signature.Create("getFilmInfoFull", movieId), FilmWebHttpMethod.Get)
        {
        }

        public override async Task<double> Parse(HttpResponseMessage responseMessage)
        {
            var jsonBody = await base.GetJsonBody(responseMessage);
            var json = JsonConvert.DeserializeObject<JArray>(Regex.Replace(jsonBody, "t(s?):(\\d+)$", string.Empty));

            var parsed = double.TryParse(json[AVG_RATE_INDEX].ToString(), out var avgRate);
            return parsed ? avgRate : default;
        }
    }
}
