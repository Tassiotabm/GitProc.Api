using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public interface IUserService
    {
        Task<Usuario> Login(string password, string login);
        Task<Usuario> CreateUser(Usuario user);
        Task RemoveUser(Guid usuarioId);
        Task EditUser(string password, string login, Guid usuarioId);
    }
}