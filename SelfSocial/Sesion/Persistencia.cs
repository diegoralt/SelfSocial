using System;
using SelfSocial.Services;

namespace SelfSocial.Sesion
{
    public class Persistencia
    {
        public static Usuarios persisten;
        public Persistencia(Usuarios usuario)
        {
            persisten = usuario;
        }
    }
}
