
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SelfSocial.Services;
using SelfSocial.Sesion;

namespace SelfSocial
{
    [Activity(Label = "SelfSocial", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        Button btn_iniciar, btn_facebook;
        TextView txt_registro;
        EditText editName, editPass;
        Intent intent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout_activity_login);

            txt_registro = FindViewById<TextView>(Resource.Id.text_registerLogin);
            txt_registro.Click += (sender, e) => {
                intent = new Intent(this, typeof(RegisterActivity));
                StartActivity(intent);
            };

            editName = FindViewById<EditText>(Resource.Id.edit_userLogin);
            editPass = FindViewById<EditText>(Resource.Id.edit_passLogin);

            btn_iniciar = FindViewById<Button>(Resource.Id.btn_iniciarLogin);
            btn_iniciar.Click += OnClickIniciar;

            btn_facebook = FindViewById<Button>(Resource.Id.btn_facebook);
            btn_facebook.Click += OnClickFacebook;

            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

		private async void OnClickIniciar(object sender, EventArgs e)
		{
            try {
				ServiceHelper serviceHelper = new ServiceHelper();

				string name = editName.Text;
				string pass = editPass.Text;

				if ((name.Length < 1) || (pass.Length < 1))
				{
					Toast.MakeText(this, "Verifica tus datos por favor", ToastLength.Short).Show();
				}
				else
				{
					List<Usuarios> items = await serviceHelper.BuscaUsuario(name, pass);
					if (items.Count() == 0)
					{
						Persistencia persistencia = new Persistencia(items[0]);
						Toast.MakeText(this, "¡Bienvenido!", ToastLength.Short).Show();
						intent = new Intent(this, typeof(MenuActivity));
						StartActivity(intent);
					}
					else
					{
						Toast.MakeText(this, "El usuario o la contraseña son incorrectos", ToastLength.Short).Show();
						editPass.Text = "";
					}
				}
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, "Ocurrio un erro al consultar el servicio", ToastLength.Short).Show();
			}
		}

        private async void OnClickFacebook(object sender, EventArgs e){
            try{
				ServiceHelper serviceHelper = new ServiceHelper();
				if (await serviceHelper.Authenticate())
				{
					Toast.MakeText(this, "¡Bienvenido!", ToastLength.Short).Show();
					intent = new Intent(this, typeof(MenuActivity));
					StartActivity(intent);
				}
            }
			catch (Exception ex)
			{
				Toast.MakeText(this, "Fallo autenticación con Facebook", ToastLength.Short).Show();
			}
        }

		private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
			{
				Toast.MakeText(this, "Conectado a Internet", ToastLength.Long).Show();
			}
			else
			{
				Toast.MakeText(this, "No hay una conexión disponible", ToastLength.Long).Show();
			}
		}
    }
}
