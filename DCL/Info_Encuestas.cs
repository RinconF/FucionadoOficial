using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL
{
    public class Info_Encuestas
    {
        public int IdEncuesta { get; set; }
        public string NombreEncuesta { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }  // Cambiado a DateTime?

        public Info_Encuestas() { }

        public Info_Encuestas(string nombreEncuesta, string descripcion)
        {
            NombreEncuesta = nombreEncuesta;
            Descripcion = descripcion;
        }

        public Info_Encuestas(IDataRecord obj)
        {
            IdEncuesta = Convert.ToInt32(obj["IdEncuesta"]);
            NombreEncuesta = Convert.ToString(obj["NombreEncuesta"]);
            Descripcion = Convert.ToString(obj["Descripcion"]);
            FechaCreacion = obj["FechaCreacion"] as DateTime?;
        }

        public Info_Encuestas(DataRow obj)
        {
            IdEncuesta = Convert.ToInt32(obj["IdEncuesta"]);
            NombreEncuesta = Convert.ToString(obj["NombreEncuesta"]);
            Descripcion = Convert.ToString(obj["Descripcion"]);
            FechaCreacion = obj["FechaCreacion"] as DateTime?;
        }
    }
}

