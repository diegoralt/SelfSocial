
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
    [Activity(Label = "Registro")]
    public class RegisterActivity : Activity
    {
        EditText editNombre, editEmail, editPass, editPassCon;
        Button btn_aceptar, btn_cancelar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout_activity_registro);

            editNombre = FindViewById<EditText>(Resource.Id.edit_userRegister);
            editEmail = FindViewById<EditText>(Resource.Id.edit_emailRegistro);
            editPass = FindViewById<EditText>(Resource.Id.edit_passRegister);
            editPassCon = FindViewById<EditText>(Resource.Id.edit_passconRegister);
            btn_aceptar = FindViewById<Button>(Resource.Id.btn_confirmarRegister);
            btn_aceptar.Click += OnClickAceptar;

            btn_cancelar = FindViewById<Button>(Resource.Id.btn_cancelarRegister);
            btn_cancelar.Click += OnClickCancelar;
        }

        private async void OnClickAceptar(object sender, EventArgs e){
            try {
                ServiceHelper serviceHelper = new ServiceHelper();

                string name = editNombre.Text;
                string email = editEmail.Text;
                string pass = editPass.Text;
                string passCon = editPassCon.Text;

                if (pass.Equals(passCon)){
					List<Usuarios> items = await serviceHelper.BuscaRegistro(email);
					if (items.Count() == 0)
					{
                        await serviceHelper.InsertarEntidadAsync(name, email, pass);
                        Usuarios usuario = new Usuarios
                        {
                            Name = name,
                            Email = email,
                            Password = pass
                        };
                        Persistencia persistencia = new Persistencia(usuario);
                        Toast.MakeText(this, "¡Bienvenido!", ToastLength.Short).Show();
                        Intent intent = new Intent(this, typeof(MenuActivity));
                        StartActivity(intent);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "¡El usuario ya existe!", ToastLength.Short).Show();
                    }
                }
                else
                {
                    editPassCon.Text = "";
                    Toast.MakeText(this,"Las contraseña son diferentes",ToastLength.Short).Show();
                }

            }
            catch (Exception ex){
                Toast.MakeText(this, "Ocurrio un erro al consultar el servicio", ToastLength.Short).Show();
            }
        }

        private void OnClickCancelar(object sender, EventArgs e){
            Finish();
        }
    }
}
