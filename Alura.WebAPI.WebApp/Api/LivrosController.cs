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

        [HttpPost]
        public IActionResult Incluir(LivroUpload model)
        {
            if(ModelState.IsValid)
            {
                var livro = model.ToLivro();
                _repository.Incluir(livro);
                var uri = Url.Action("Recuperar", new { id = livro.Id });
                return Created(uri, livro); // 201
            } else
            {
                return Json(model.ToLivro());
                return BadRequest();
            }
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

        [HttpPost]
        public IActionResult Alterar(LivroUpload model)
        {
            if(ModelState.IsValid)
            {
                var livro = model.ToLivro();
                if (model.Capa == null)
                {
                    livro.ImagemCapa = _repository.All
                        .Where(l => l.Id == livro.Id)
                        .Select(l => l.ImagemCapa)
                        .FirstOrDefault();
                }
                _repository.Alterar(livro);
                return Ok(); // 200
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Remover(int id)
        {
            var model = _repository.Find(id);
            if(model == null)
            {
                return NotFound();
            } else
            {
                _repository.Excluir(model);
                return NoContent(); // 204
            }
        }
    }
}
