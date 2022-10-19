using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreAPI.DbContext
{
    public interface IGenericDbContext<T> where T : Microsoft.EntityFrameworkCore.DbContext, IDisposable
    {
        DatabaseFacade Database { get; }
        DbSet<T> Repository<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
        Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);
        T GetContext();
    }
}