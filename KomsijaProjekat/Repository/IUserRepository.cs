using System.Collections.Generic;
using KomsijaProjekat.Models;

namespace KomsijaProjekat.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);

        User GetByEmail(string email);

        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
