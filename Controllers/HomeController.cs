﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarea2Perros.Models.Entities;
using Tarea2Perros.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tarea2Perros.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index(string Id)
		{
			PerrosContext context = new();
			IndexRazasViewModel vm = new();
			var listaletras = context.Razas.OrderBy(x=> x.Nombre).Select(x=> x.Nombre[0]).ToList();
			vm.ListaLetrasAbecedario = listaletras.Distinct();
			if (Id == null)
			{
                var datos = context.Razas.OrderBy(x => x.Nombre).Select(x => new ListaPerrosModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
				});
				vm.ListaRazas = datos;
            }
			else
			{
				var datos = context.Razas.OrderBy(x => x.Nombre).Where(x => x.Nombre.StartsWith(Id)).Select(x => new ListaPerrosModel
				{
					Id = x.Id,
					Nombre = x.Nombre
				});
				vm.ListaRazas = datos;
			}
			return View(vm);
        }

		public IActionResult Raza(string Id)
		{
			Id = Id.Replace("-", " ");
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
			if (datos != null)
			{
                var random = new Random();
                var perrosrandom = context.Razas.Where(x => x.Nombre != Id).ToList().Select(x => new PerrosModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                }).OrderBy(x => random.Next()).Take(4).ToList();
                datos.ListaPerros = perrosrandom.ToList();
            }
			else
			{
				return RedirectToAction("Index");
			}
            return View(datos);
		}

		public IActionResult Pais()
		{
			PerrosContext context = new();
			var datos = context.Paises.OrderBy(x => x.Nombre).Select(x => new PaisViewModel
			{
				Pais = x.Nombre ?? "N/A",
				PerrosxPais = x.Razas.OrderBy(x => x.Nombre).Select(x => new PerroModel
				{
					Id = x.Id,
					Nombre= x.Nombre
				})
			});
			return View(datos);
		}
	}
}
