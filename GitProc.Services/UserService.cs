using GitProc.Data;
using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Usuario> CreateUser(Usuario user)
        {
            Usuario existUser = await _uow.Usuario.SingleOrDefault(x => x.Login == user.Login);
            if (existUser != null)
            {
                return null;
            }

            user.LastLogin = DateTime.Now;
            user.UsuarioId = new Guid();

            await _uow.Usuario.Add(user);
            _uow.Complete();
            return user;
        }

        public async Task RemoveUser(Guid usuarioId)
        {
            Usuario usuario = await _uow.Usuario.SingleOrDefault(x => x.UsuarioId == usuarioId);
            _uow.Usuario.Remove(usuario);
            _uow.Complete();
        }

        public async Task EditUser(string password, string login, Guid usuarioId)
        {
            Usuario usuarioToBeEdited = await _uow.Usuario.SingleOrDefault(x => x.UsuarioId == usuarioId);
            _uow.Usuario.Attach(usuarioToBeEdited);
            usuarioToBeEdited.Login = login;
            usuarioToBeEdited.Password = password;
            _uow.Complete();
        }

        public async Task<Usuario> Login (string password, string login)
        {
            Usuario user = await _uow.Usuario.SingleOrDefault(x => x.Login == login && x.Password == password);
            return user;
        }
    }
}
