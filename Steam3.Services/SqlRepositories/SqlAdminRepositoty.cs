using Steam3.Models;
using Steam3.Services.Interfaces;

namespace Steam3.Services.SqlRepositories
{
    public class SqlAdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public SqlAdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public Admin GetAdmin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;
            return _context.Admins.Find(login);
        }
    }
}
