using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarea2Perros.Models.Entities;
using Tarea2Perros.Models.ViewModels;

namespace Tarea2Perros.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			PerrosContext context = new();
			var datos = context.Razas.OrderBy(x=>x.Nombre).Select(x => new IndexRazasViewModel
			{
				Id = x.Id,
				Nombre = x.Nombre,
			});
			return View(datos);
		}

		public IActionResult Raza(string Id)
		{
			PerrosContext context = new();
			var datos = context.Razas.Include(x => x.IdPaisNavigation).Include(x => x.Estadisticasraza).Include(x => x.Caracteristicasfisicas).Where(x=>x.Nombre == Id).Select(x => new RazaViewModel
			{
				Id = x.Id,
				Nombre = x.Nombre,
				Descripcion = x.Descripcion,
				Pais = x.IdPaisNavigation.Nombre ?? "N/A",
				OtrosNombres= x.OtrosNombres,
				PesoMax = x.PesoMax,
				PesoMin = x.PesoMin,
				AlturaMax = x.AlturaMax,
				AlturaMin = x.AlturaMin,
				EsperanzaVida = x.EsperanzaVida,
				NivelEnergia = x.Estadisticasraza != null ? x.Estadisticasraza.NivelEnergia : 0,
				FacilidadEntrenamiento = x.Estadisticasraza != null ? x.Estadisticasraza.FacilidadEntrenamiento : 0,
				EjercicioObligatorio = x.Estadisticasraza != null ? x.Estadisticasraza.EjercicioObligatorio : 0,
				AmistadDesconocidos = x.Estadisticasraza != null ? x.Estadisticasraza.AmistadDesconocidos : 0,
				AmistadPerros = x.Estadisticasraza != null ? x.Estadisticasraza.AmistadPerros : 0,
				NecesidadCepillado = x.Estadisticasraza != null ? x.Estadisticasraza.NecesidadCepillado : 0,
				Patas = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Patas : "N/A",
				Cola = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Cola : "N/A",
				Hocico = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Hocico : "N/A",
				Pelo = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Pelo : "N/A",
				Color = x.Caracteristicasfisicas != null ? x.Caracteristicasfisicas.Color : "N/A"
			}).FirstOrDefault();
			return View(datos);
		}

		public IActionResult Pais()
		{
			return View();
		}
	}
}
