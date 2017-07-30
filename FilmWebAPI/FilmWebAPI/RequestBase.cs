﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FilmWebAPI.Helpers;

namespace FilmWebAPI
{
    public abstract class RequestBase<T> : IHttpExecutable
    {
        private readonly Signature _signature;
        private readonly FilmWebHttpMethod _filmWebHttpMethod;

        protected RequestBase(Signature signature, FilmWebHttpMethod filmWebHttpMethod)
        {
            _signature = signature;
            _filmWebHttpMethod = filmWebHttpMethod;
        }

        protected RequestBase()
        {
            
        }
        public enum FilmWebHttpMethod
        {
            Get,
            Post,
        }

        public virtual HttpRequestMessage GetRequestMessage()
        {
            var query = new Dictionary<string, string>
            {
                {"methods", _signature.GetMethod()},
                {"appId", "android"},
                {"version", "2.8"},
                {"signature", _signature.GetSignature()},
            };

            switch (_filmWebHttpMethod)
            {
                case FilmWebHttpMethod.Get:
                    return new HttpRequestMessage(HttpMethod.Get, QueryHelpers.CreateQuery(FilmWeb.API_URL, query));
                case FilmWebHttpMethod.Post:
                    return new HttpRequestMessage(HttpMethod.Post, FilmWeb.API_URL) {Content = new FormUrlEncodedContent(query)};
                default:
                    throw new FilmWebException("Ta metoda nie jest obsługiwana!", FilmWebExceptionType.HttpMethodNotSupported);
            }
        }

        public abstract Task<T> Parse(HttpResponseMessage responseMessage);
    }

}