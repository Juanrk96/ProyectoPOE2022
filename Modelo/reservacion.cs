//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoPOE.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class reservacion
    {
        public int idReservacion { get; set; }
        public string codigo { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idVuelo { get; set; }
        public Nullable<int> cantReservada { get; set; }
    }
}