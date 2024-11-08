using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP3.Domain.Entities
{
    [Table("tb_barco")]
    public class BarcoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty; 

        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        [MaxLength(50)]
        public string Modelo { get; set; } = string.Empty; 

        [Range(1900, 2050, ErrorMessage = "O Ano deve estar entre 1900 e 2050.")]
        public int Ano { get; set; }

        [Range(1.0, 500.0, ErrorMessage = "O Tamanho deve estar entre 1.0 e 500.0 metros.")]
        public double Tamanho { get; set; }
    }
}
