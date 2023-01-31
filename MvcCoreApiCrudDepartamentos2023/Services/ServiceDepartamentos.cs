using MvcCoreApiCrudDepartamentos2023.Models;
using System.Net.Http.Headers;

namespace MvcCoreApiCrudDepartamentos2023.Services
{
    public class ServiceDepartamentos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceDepartamentos(string url)
        {
            this.UrlApi= url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> GetDatosApi<T>(string request)
        {
            using (HttpClient client = new HttpClient()) 
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T datos = await response.Content.ReadAsAsync<T>();
                    return datos;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<List<Departamentos>> GetDepartamentosAsync()
        {
            string request = "/api/Departamentos";
            List<Departamentos> departamentos = await this.GetDatosApi<List<Departamentos>>(request);
            return departamentos;

        }
        public async Task<List<string>> GetLocalidadesAsync()
        {
            string request = "api/Departamentos/GetLocalidades";
            List<string> localidades = await this.GetDatosApi<List<string>>(request);
            return localidades;
        }
        public async Task <Departamentos> FindDepartamentoAsync(int id)
        {
            string request = "api/Departamentos/" + id;
            Departamentos departamento = await this.GetDatosApi<Departamentos>(request);
            return departamento;
        }
        public async Task<List<Departamentos>> GetDepartamentosLocAsyc(string localidad)
        {
            string request = "/api/Departamentos/GetDepartamentosLocalidad/" + localidad;
            List<Departamentos> departamentos = await this.GetDatosApi<List<Departamentos>>(request);
            return departamentos;
        }
    }
}
