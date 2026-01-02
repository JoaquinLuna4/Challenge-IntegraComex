using Microsoft.AspNetCore.Mvc;
using ChallengeIntegra.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace ChallengeIntegra.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteApiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IClienteService _clienteService;

        public ClienteApiController(IHttpClientFactory httpClientFactory, IClienteService clienteService)
        {
            _httpClientFactory = httpClientFactory;
            _clienteService = clienteService;
        }

        [AcceptVerbs("GET", "POST")] // Aceptar tanto GET como POST
        public async Task<IActionResult> VerificarCuitUnico(string cuit, int? id)
        {
            // Si el CUIT está vacío, la validación [Required] del modelo se encargará de eso.
            // Solo actua si hay un valor para verificar
            if (!string.IsNullOrWhiteSpace(cuit))
            {
                bool cuitExists = await _clienteService.CuitExists(cuit, id);
                if (cuitExists)
                {
                    // Si existe, devolvemos un JSON con el mensaje de error.
                    // El framework de validación lo interpretará como un error.
                    return new JsonResult($"El CUIT '{cuit}' ya está registrado.");
                }
            }

            // Si el CUIT no existe, devolvemos 'true', indicando que la validación es exitosa.
            return new JsonResult(true);
        }

        [HttpGet("GetNombrePorCuit")]
        public async Task<IActionResult> GetNombrePorCuit(string cuit)
        {
            if (string.IsNullOrWhiteSpace(cuit) || cuit.Length != 11)
            {
                return BadRequest(new { success = false, message = "El CUIT debe tener 11 dígitos." });
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var url = $"https://sistemaintegracomex.com.ar/Account/GetNombreByCuit?cuit={cuit}";
                
                var razonSocialString = await client.GetStringAsync(url);

                if (string.IsNullOrEmpty(razonSocialString))
                {
                    return NotFound(new { success = false, message = "El CUIT no fue encontrado en el servicio externo." });
                }
                
                return Ok(new { success = true, name = razonSocialString });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, new { success = false, message = $"Servicio externo no disponible o error de red: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error inesperado al obtener la razón social: {ex.Message}" });
            }
        }
    }
}
