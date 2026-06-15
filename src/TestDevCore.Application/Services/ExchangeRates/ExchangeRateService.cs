using System.Net.Http.Json;
using TestDevCore.Domain.ExchangeRates.Entities;
using TestDevCore.Domain.ExchangeRates.Repositories;

namespace TestDevCore.Application.Services.ExchangeRates
{
    public class ExchangeRateService
    {
        private readonly IExchangeRateCacheRepository _cacheRepository;
        private readonly IExchangeRateRepository _dbRepository;
        private readonly HttpClient _httpClient;

        public ExchangeRateService(
            IExchangeRateCacheRepository cacheRepository,
            IExchangeRateRepository dbRepository,
            HttpClient httpClient)
        {
            _cacheRepository = cacheRepository;
            _dbRepository = dbRepository;
            _httpClient = httpClient;
        }

        public async Task<ExchangeRate?> GetByPairAsync(string symbolBase, string symbolTarget, CancellationToken ct = default)
        {
            var cachedRate = await _cacheRepository.GetByPairAsync(symbolBase, symbolTarget);
            if (cachedRate != null)
            {
                return cachedRate;
            }

            string url = $"https://hexarate.paikama.co/api/rates/{symbolBase}/{symbolTarget}/latest";

            ExchangeRateResponse? apiResponse;
            try
            {
                apiResponse = await _httpClient.GetFromJsonAsync<ExchangeRateResponse>(url, ct);
            }
            catch (Exception ex)
            {
                apiResponse = null;
            }

            var existingRate = await _dbRepository.GetByPairAsync(symbolBase, symbolTarget, ct);

            if (apiResponse == null || apiResponse.Data == null) 
            {
                return existingRate;
            }

            if (existingRate != null)
            {
                existingRate.ChangeRate(apiResponse.Data.Mid);

                _dbRepository.Save(existingRate);
                _cacheRepository.Save(existingRate);

                return existingRate;
            } else
            {
                var newRate = ExchangeRate.Create
                (
                    symbolBase.ToUpper(),
                    symbolTarget.ToUpper(),
                    apiResponse.Data.Mid
                );

                await _dbRepository.AddAsync(newRate, ct);
                _cacheRepository.Save(newRate);

                return newRate;
            }
        }

    }
}
