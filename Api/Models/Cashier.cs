using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Cashier
    {
        public int cashierId { get; set; }
        public int? barCode { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Column(TypeName = "int(4)")]
        public int amount { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal unitValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime saleDate { get; set; }

    }
}
