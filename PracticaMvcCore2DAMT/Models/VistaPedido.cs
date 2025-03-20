using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2DAMT.Models
{
    [Table("VISTAPEDIDOS")]
    public class VistaPedido
    {
        [Key]
        [Column("IDVISTAPEDIDOS")]
        public int IdVistaPedidos { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("PORTADA")]
        public string Portada { get; set; }
    }
}
