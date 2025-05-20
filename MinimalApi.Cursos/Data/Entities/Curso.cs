using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Cursos.Data.Entities
{
    public class Curso
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Technologies { get; set; }
        public double Price { get; set; }
        public string Instructor { get; set; }

        public string CreatedAt { get; set; } = DateTime.Now.ToString("D");
    }

    public class CursoDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} supera la longitud máxima")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} supera la longitud máxima")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} supera la longitud máxima")]
        public required string[] Technologies { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} supera la longitud máxima")]
        public required double Price { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} supera la longitud máxima")]
        public required string Instructor { get; set; }

    }
}
