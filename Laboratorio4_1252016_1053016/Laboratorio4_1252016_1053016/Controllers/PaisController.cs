using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using Laboratorio4_1252016_1053016.Models;


namespace Laboratorio4_1252016_1053016.Controllers
{
    public class PaisController : Controller
    {
        Dictionary<string, país> dictionary = new Dictionary<string, país>();
        Dictionary<país, NumCalcomania> dictionary2 = new Dictionary<país, NumCalcomania>();

        // GET: Pais
        public ActionResult Index()
        {
            return View();
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pais/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pais/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Lector()
        {
            return View("CargaArchivos"); 
        }

        private bool IsValidContentType(HttpPostedFileBase contentType)
        {
            return contentType.FileName.EndsWith(".json");
        }

        public ActionResult DictionarySuccess()
        {
            return View("CargaArchivoEstado"); 
        }

        public ActionResult InsertarDatos(HttpPostedFileBase File)
        {
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("Index");
            }
            else
            {
                if (!IsValidContentType(File))
                {
                    ViewBag.Error = "Solo archivos Json son válidos para la entrada";
                    return View("Index");
                }

                if (File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/JsonFiles/" + fileName));
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    File.SaveAs(path);
                    using (StreamReader reader = new StreamReader(path))
                    {
                        if (reader != null)
                        {                           
                            string info = reader.ReadToEnd();
                            var lista = JsonConvert.DeserializeObject<List<Dictionary<string, país>>>(info);
                            for (int i = 0; i < lista.Count; i++)
                            {
                                dictionary.Add(lista.ElementAt(i).ElementAt(0).Key, lista.ElementAt(i).ElementAt(0).Value); 
                            } 
                           
                            return View("Dictionary1Success");
                        }
                    }
                }
            }
            return View(); 
        }

        public ActionResult InsertarDatosEstado(HttpPostedFileBase File)
        {
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("CargaArchivoEstado");
            }
            else
            {
                if (!IsValidContentType(File))
                {
                    ViewBag.Error = "Solo archivos Json son válidos para la entrada";
                    return View("CargaArchivoEstado");
                }

                if (File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/JsonFiles/" + fileName));
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    File.SaveAs(path);
                    using (StreamReader reader = new StreamReader(path))
                    {
                        if (reader != null)
                        {
                            string info = reader.ReadToEnd();
                            var lista = JsonConvert.DeserializeObject<List<Dictionary<país, NumCalcomania>>>(info);
                            for (int i = 0; i < lista.Count; i++)
                            {
                               // dictionary.Add(lista.ElementAt(i).ElementAt(0).Key, lista.ElementAt(i).ElementAt(0).Value);
                            }

                            return View("Dictionary1Success");
                        }
                    }
                }
            }
            return View();
        }
    }
}
