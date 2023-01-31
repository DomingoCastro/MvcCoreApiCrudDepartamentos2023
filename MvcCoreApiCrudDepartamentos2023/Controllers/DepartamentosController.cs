using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudDepartamentos2023.Models;
using MvcCoreApiCrudDepartamentos2023.Services;

namespace MvcCoreApiCrudDepartamentos2023.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceDepartamentos service;
        public DepartamentosController(ServiceDepartamentos service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Departamentos()
        {
            List<Departamentos> departamentos = await this.service.GetDepartamentosAsync();
            List<string> localidades = await this.service.GetLocalidadesAsync();
            ViewData["LOCALIDADES"] = localidades;
            return View(departamentos);
        }
        [HttpPost]
        public async Task <IActionResult> Departamentos(string localidad)
        {
            List<Departamentos> departamentos = await this.service.GetDepartamentosLocAsyc(localidad); 
            List<string> localidades = await this.service.GetLocalidadesAsync();
            ViewData["LOCALIDADES"] = localidades;
            return View(departamentos);
        }
        public async Task <IActionResult> Details(int id)
        {
            Departamentos dept = await this.service.FindDepartamentoAsync(id);
            return View(dept);
        }
    }
}
