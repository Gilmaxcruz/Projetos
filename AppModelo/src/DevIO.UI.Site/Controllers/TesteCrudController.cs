using DevIO.UI.Site.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.UI.Site.Data;

namespace DevIO.UI.Site.Controllers
{
    public class TesteCrudController : Controller
    {
        private readonly MeuDbContext _contexto;
        public TesteCrudController(MeuDbContext contexto)
        {
            _contexto = contexto;
        }


        // GET: TesteCrudController
        public ActionResult Index()
        {
            var aluno = new Aluno
            {
                nome = "Gilmax",
                DataNascimento = DateTime.Now,
                email = "gilmaxsoaresdacruz@gmail.com"
            };
            _contexto.Alunos.Add(aluno);
            _contexto.SaveChanges();

            var aluno2 = _contexto.Alunos.Find(aluno.Id);

            var aluno3= _contexto.Alunos.FirstOrDefault(x =>x.email == "gilmaxsoaresdacruz@gmail.com");
            var aluno4 = _contexto.Alunos.Where(x => x.nome == "Gilmax");

            aluno.nome = "Larissa";
            _contexto.Alunos.Update(aluno);
            _contexto.SaveChanges();

            _contexto.Alunos.Remove(aluno);
            _contexto.SaveChanges();

            return View();
        }

        // GET: TesteCrudController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TesteCrudController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TesteCrudController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TesteCrudController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TesteCrudController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TesteCrudController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TesteCrudController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
