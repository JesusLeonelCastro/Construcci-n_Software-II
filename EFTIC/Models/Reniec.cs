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