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

        public IActionResult CreateDepartamento()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartamento(Departamentos departamento)
        {
            await this.service.InsertarDepartamentoAsync(departamento.IdDepartamento, departamento.Nombre, departamento.Localidad);
            return RedirectToAction("Departamentos");
        }

        public async Task<IActionResult> UpdateDepartamento(int id)
        {
            Departamentos departamento = await this.service.FindDepartamentoAsync(id);
            return View(departamento);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDepartamento(Departamentos departamento)
        {
            await this.service.UpdateDepartamentoAsync(departamento.IdDepartamento, departamento.Nombre, departamento.Localidad);
            return RedirectToAction("Departamentos");
        }
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            await this.service.DeleteDepartamentoAsync(id);
            return RedirectToAction("Departamentos");
        }



    }
}
