using Enquiry.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Enquiry.Web
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
    public class DBInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _appContext;
        public DBInitializer(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task SeedAsync()
        {
            await _appContext.Database.MigrateAsync().ConfigureAwait(false);
            if (await _appContext.Employees.AnyAsync())
            {
                foreach (var emp in _appContext.Employees.ToList())
                {
                    if (string.IsNullOrEmpty(emp.Password))
                    {
                        emp.Password = emp.ContactNo;
                        _appContext.Update(emp);
                        await _appContext.SaveChangesAsync().ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
