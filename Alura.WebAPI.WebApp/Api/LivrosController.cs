using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Api
{
    public class LivrosController : Controller
    {
        private readonly IRepository<Livro> _repository;

        public LivrosController(IRepository<Livro> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Recuperar(int id)
        {
            var model = _repository.Find(id); 

            if(model == null)
            {
                return NotFound();
            } else
            {
                return Json(model.ToModel());
            }
        }
    }
}
