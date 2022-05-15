using Steam3.Models;

namespace Steam3.Services
{
    public interface IAdminRepository
    {
        Admin GetAdmin(string login);
    }
}
