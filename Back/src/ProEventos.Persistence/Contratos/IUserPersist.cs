using ProEventos.Domain;
using ProEventos.Domain.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUsersAsync ();
        Task<User> GetUserByIdAsync (int id);
        Task<User> GetUserByUsernameAsync (string username);
    }
}

