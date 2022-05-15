using Steam3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam3.Services
{
    public class MockAdminRepository : IAdminRepository
    {
        private List<Admin> _adminsList;

        public MockAdminRepository()
        {
            _adminsList = new List<Admin>()
            {
                new Admin{ Login ="prostovitya", Password="01234", Name = "prostovitya"},
                new Admin{ Login ="maxonproger", Password="567890", Name = "maxon"},
            };
        }

        public Admin GetAdmin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;
            return _adminsList.Find(a => a.Login.Equals(login));
        }
    }
}
