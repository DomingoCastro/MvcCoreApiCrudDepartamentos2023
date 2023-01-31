using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreApiCrudDepartamentos2023.Models
{
    public class Departamentos
    {
      public int IdDepartamento { get; set; }
      public string Nombre { get; set; }
      public string Localidad { get; set; }
    }
}
