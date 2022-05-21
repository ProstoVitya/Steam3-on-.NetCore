using Steam3.Models;

namespace Steam3.Services.Interfaces
{
    public interface IAdminRepository
    {
        Admin GetAdmin(string login);
    }
}
