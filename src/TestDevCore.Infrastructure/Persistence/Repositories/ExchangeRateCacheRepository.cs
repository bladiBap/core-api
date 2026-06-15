using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Domain.ExchangeRates.Entities;
using TestDevCore.Domain.ExchangeRates.Repositories;
using TestDevCore.Infrastructure.Persistence.DomainModel;

namespace TestDevCore.Infrastructure.Persistence.Repositories
{
    internal class ExchangeRateCacheRepository: IExchangeRateCacheRepository
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _expirationTime = TimeSpan.FromMinutes(1);

        public ExchangeRateCacheRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Save(ExchangeRate exchangeRate)
        {
            string key = GetCacheKey(exchangeRate.SymbolBase, exchangeRate.SymbolTarget);

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(_expirationTime);

            _cache.Set(key, exchangeRate, options);
        }

        public async Task<ExchangeRate?> GetByPairAsync(string symbolBase, string symbolTarget)
        {
            string key = GetCacheKey(symbolBase, symbolTarget);

            if (_cache.TryGetValue(key, out ExchangeRate? cachedRate))
            {
                return cachedRate;
            }

            return null;
        }
        private string GetCacheKey(string symbolBase, string symbolTarget)
        {
            return $"{symbolBase.ToUpper()}-{symbolTarget.ToUpper()}";
        }
    }
}
