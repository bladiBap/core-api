using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDevCore.Infrastructure.Persistence
{
    public interface IDatabase : IDisposable {
        void Migrate();
    }
}