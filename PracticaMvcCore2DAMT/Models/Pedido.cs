﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2DAMT.Models
{
    [Table("PEDIDOS")]
    public class Pedido
    {
        [Key]
        [Column("IDPEDIDO")]
        public int IdPedido { get; set; }
        [Column("IDFACTURA")]
        public int IdFactura { get; set; }
        [Column("FECHA")]
        public DateOnly Fecha { get; set; }
        [Column("IDLIBRO")]
        public int IdLibro { get; set; }
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("CANTIDAD")]
        public int Cantidad { get; set; }
    }
}
