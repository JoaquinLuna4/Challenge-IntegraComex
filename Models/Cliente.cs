using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc; // Necesario para el atributo [Remote]

namespace ChallengeIntegra.Models
{
    public class Cliente
    {
        // Le damos la propiedad de llave primaria y autoincrementable.
        // También es el campo que se usará para la validación remota en el modo de edición.
        [Key]
        public int Id { get; set; }

        // Asignamos las data anotations a las propiedades y algunas validaciones. 
        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El CUIT debe tener 11 caracteres.")]


        // --- VALIDACIÓN REMOTA ---
        // Llama a la acción 'VerificarCuitUnico' en 'ClienteApiController'.
        // Le pasa el valor de este campo (CUIT) y el valor del campo 'Id' (AdditionalFields).
        [Remote(action: "VerificarCuitUnico", controller: "ClienteApi", AdditionalFields = nameof(Id))]
        public string CUIT { get; set; }

        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El teléfono solo puede contener números.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
        public string Direccion { get; set; }

        public bool Activo { get; set; }
    }
}
