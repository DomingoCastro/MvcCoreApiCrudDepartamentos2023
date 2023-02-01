using MvcCoreApiCrudDepartamentos2023.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        //LOS METODOS DE ACCION NO SUELEN TENER UN METODO GENERICO 
        // YA QUE CADA UNO RECIBE UN VALOR DISTINTO
        public async Task DeleteDepartamentoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //COMO NO VAMOS A RECIBIR NADA SIMPLEMENTE SE REALIZA LA ACCION
                await client.DeleteAsync(request);
            }
        }

        public async Task InsertarDepartamentoAsync(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos/";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //TENEMOS QUE ENVIAR UN OBJETO DEPARTAMENTO
                //POR LO QUE CREAMOS UNA CLASE DEL MODEL DEPARTAMENTO
                //CON LOS DATOS QUE NOS DAN
                Departamentos departamento = new Departamentos();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                //CONVERTIMOS EL OBJETO DEPARTAMENTO EN UN JSON
                string departamentojson = JsonConvert.SerializeObject(departamento);
                //PARA ENVIAR EL OBJETO JSON EN EL BODY SE REALIZA MENDIANTE
                //UNA CLASE LLAMADA STRINGCONTENT DONDE DEBEMOS INDICAR EL TIPO
                //DE CONTENIDO QUE ESTAMOS ENVIANDO (JSON)
                StringContent content = new StringContent(departamentojson, Encoding.UTF8, "application/json");
                ///REALIZAMOS LA LLAMADA AL SERVICIO ENVIANDO EL OBJETO CONTENT
                await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                Departamentos departamento = new Departamentos();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad= localidad;
                string json = JsonConvert.SerializeObject(departamento);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
    
    }
}
