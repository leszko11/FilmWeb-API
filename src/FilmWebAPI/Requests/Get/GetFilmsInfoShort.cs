﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FilmWebAPI.Requests.Get
{
    public class GetFilmsInfoShort : RequestBase<dynamic>
    {
        public GetFilmsInfoShort(long movieId) : base(Signature.Create("getFilmsInfoShort", movieId), FilmWebHttpMethod.Get)
        {
        }

        public override async Task<dynamic> Parse(HttpResponseMessage responseMessage)
        {
            throw new NotImplementedException();
        }
    }
}