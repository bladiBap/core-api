using System;
using System.Collections.Generic;
using System.Text;

namespace TestDevCore.Application.Services.ExchangeRates
{

    internal class ExchangeRateResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public ExchangeRateData? Data { get; set; }
    }

    internal class ExchangeRateData
    {
        [System.Text.Json.Serialization.JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("target")]
        public string Target { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("mid")]
        public decimal Mid { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;
    }
}
