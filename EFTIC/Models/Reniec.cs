using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;


namespace EFTIC.Models
{
    public class Reniec
    {
        /*
         * ------------------------------------------------------------
         * CLASE: Reniec.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/04/2025 al 20/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Esta clase encapsula la lógica de consumo de la API REST pública de RENIEC 
         * mediante un cliente HTTP. Permite obtener datos de una persona natural 
         * registrada en RENIEC a partir de su número de DNI.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Consulta de datos personales (nombres y apellidos) mediante número de DNI.
         * - Uso del paquete `HttpClient` para realizar peticiones RESTful.
         * - Deserialización de la respuesta JSON usando `Newtonsoft.Json`.
         * 
         * MÉTODOS DESTACADOS:
         * - `ObtenerDatosPorDni(string dni)`: Devuelve una tupla `(Nombre, Apellido)` 
         *    con los datos asociados al DNI consultado.
         * 
         * NOTAS DE IMPLEMENTACIÓN:
         * - El token API está configurado en el constructor de la clase.
         * - La clase maneja errores HTTP y de deserialización con manejo de excepciones.
         * - Requiere tener instalado `Newtonsoft.Json` vía NuGet.
         * 
         * REFERENCIAS:
         * - Documentación oficial de la API: https://apis.net.pe/api-consulta-dni
         * - Newtonsoft.Json: https://www.newtonsoft.com/json
         * 
         * ------------------------------------------------------------
         */


        private readonly HttpClient _httpClient;

        public Reniec(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "apis-token-10179.RwYZkwvYtho5qwBmkRDNWbrjnIT9lf-I"); //Token

            _httpClient.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true
            };
        }

        public async Task<(string Nombre, string Apellido)> ObtenerDatosPorDni(string dni)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.apis.net.pe/v2/reniec/dni?numero={dni}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonResponse);

                    string nombre = data.nombres;
                    string apellido = $"{data.apellidoPaterno} {data.apellidoMaterno}";


                    return (nombre, apellido);
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error en la API: {response.StatusCode} - {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos del DNI: {ex.Message}");
                return (null, null);
            }
        }
    }
}