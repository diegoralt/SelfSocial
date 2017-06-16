using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SelfSocial.Services
{
    public class Usuarios
    {
        private string _id;
		private String _name;
		private String _email;
		private String _password;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return _id; }
			set
			{
				_id = value;
			}
		}

		[JsonProperty(PropertyName = "Name")]
		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
			}
		}

		[JsonProperty(PropertyName = "Email")]
		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
			}
		}

		[JsonProperty(PropertyName = "Password")]
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
			}
		}

		[Version]
		public string Version { get; set; }
    }
}
