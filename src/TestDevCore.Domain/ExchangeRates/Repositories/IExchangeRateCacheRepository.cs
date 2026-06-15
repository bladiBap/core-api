using System;
using System.Collections.Generic;
using System.Text;
using TestDevCore.Domain.ExchangeRates.Entities;

namespace TestDevCore.Domain.ExchangeRates.Repositories
{
    public interface IExchangeRateCacheRepository
    {
        void Save(ExchangeRate exchangeRate);
        Task<ExchangeRate?> GetByPairAsync(string symbolBase, string symbolTarget);
    }
}
