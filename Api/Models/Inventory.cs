using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Inventory
    {
        public int inventoryId { get; set; }

        [Display(Name = "Código de Barras")]
        public int? barCode { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string name { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int amount { get; set; }

        [Display(Name = "Valor Unitário"), Required(ErrorMessage = "Campo obrigatório")]
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal unitValue { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade")]

        public DateTime? expirationDate { get; set; }

        [Display(Name = "Data de Registro")]
        public DateTime registerDate { get; set; }
    }
}
