using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace SelfSocial.Services
{
    public class ServiceHelper
    {
        MobileServiceClient clienteServicio = new MobileServiceClient(@"http://selfsocial.azurewebsites.net");

        private IMobileServiceTable<Usuarios> _UsuarioItem;

        private MobileServiceUser user;

        public async Task<List<Usuarios>> BuscaRegistro(string Email)
		{
			_UsuarioItem = clienteServicio.GetTable<Usuarios>();
			List<Usuarios> items = await _UsuarioItem.Where(
				usuarioItem => usuarioItem.Email == Email).ToListAsync();
            return items;
		}

        public async Task<List<Usuarios>> BuscaUsuario(string Name, string Pass)
		{
			_UsuarioItem = clienteServicio.GetTable<Usuarios>();
			List<Usuarios> items = await _UsuarioItem.Where(
                usuarioItem => usuarioItem.Name == Name && usuarioItem.Password == Pass).ToListAsync();
			return items; 
		}

        public async Task InsertarEntidadAsync(string name, string email, string password)
        {
            _UsuarioItem = clienteServicio.GetTable<Usuarios>();
            await _UsuarioItem.InsertAsync(new Usuarios
            {
                Name = name,
                Email = email,
                Password = password
            });
        }

        public async Task<bool> Authenticate()
		{
            var token = new JObject();
            token.Add("access_token", "access_token_value");
			var success = false;
			try
			{
				// Sign in with Facebook login using a server-managed flow.
				user = await clienteServicio.LoginAsync(MobileServiceAuthenticationProvider.Facebook, token);

				success = true;
			}
			catch (Exception ex)
			{
                success = false;
			}
			return success;
		}
    }
}
