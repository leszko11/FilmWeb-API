﻿using System;
using System.Linq;
using FilmWebAPI.Helpers;

namespace FilmWebAPI
{
    /// <summary>
    /// Klasa do podpisywania zapytań API
    /// </summary>
    public sealed class Signature
    {
        private readonly string _method;
        private string _signature;

        private Signature(string method)
        {
            _method = method ?? throw new ArgumentNullException(nameof(method), "Nazwa metody nie może być pusta!");
        }

        public string GetMethod()
        {
            return _method;
        }

        public string GetSignature()
        {
            if (_signature == null)
                return _signature = HashHelpers.MakeSignature(_method);

            return _signature;
        }

        public static Signature Create(string method, params object[] strings)
        {
            return strings != null && strings.Any() ? new Signature(HashHelpers.ToCSV(method, strings)) : new Signature(method);
        }

    }
}