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
        Dictionary<string, Pais> dictionary = new Dictionary<string, Pais>();
        Dictionary<NumCalcomania, bool> dictionary2 = new Dictionary<NumCalcomania, bool>();

        // GET: Pais
        public ActionResult Index()
        {
            Session["Diccionario1"] = Session["Diccionario1"] ?? dictionary;
            Session["Diccionario2"] = Session["Diccionario2"] ?? dictionary2; 
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
        public ActionResult LectorPaises()
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
                            var lista = JsonConvert.DeserializeObject<List<Dictionary<string, Pais>>>(info);
                            for (int i = 0; i < lista.Count; i++)
                            {
                                dictionary.Add(lista.ElementAt(i).ElementAt(0).Key, lista.ElementAt(i).ElementAt(0).Value);                               
                            }
                            Session["Diccionario1"] = dictionary; 
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
                            var lista = JsonConvert.DeserializeObject<List<Dictionary<string,bool>>>(info);                        

                            for (int i = 0; i < lista.ElementAt(0).Count(); i++)
                            {
                                var datos = lista.ElementAt(0).ElementAt(i).Key.Split('_');
                                NumCalcomania calcomania = new NumCalcomania
                                {
                                    Pais = datos[0],
                                    Num = Convert.ToInt32(datos[1])
                                };
                                dictionary2.Add(calcomania, lista.ElementAt(0).ElementAt(i).Value); 
                            }
                            Session["Diccionario2"] = dictionary2; 
                            return View("Dictionary1Success");
                        }
                    }
                }
            }
            return View();
        }

        private void ActualizarInformacion(NumCalcomania calcomania, bool flag)
        {
           
                bool contains = dictionary.ContainsKey(calcomania.Pais);
                
                if (contains)
                {
                    Pais PaisEditar = dictionary[calcomania.Pais];
                    if (flag)
                    {          
                        //si no esta en coleccionadas, agrega la calcomania actual
                        if(!PaisEditar.coleccionadas.Contains(calcomania.Num))
                        {
                            PaisEditar.coleccionadas.Add(calcomania.Num); 
                        }
                        //si ya esta en coleccionadas la agrega a las calcomanias disp. para cambios
                        else if(!PaisEditar.cambios.Contains(calcomania.Num))
                        {
                            PaisEditar.cambios.Add(calcomania.Num);
                        }

                    }
                    else
                    {      
                        //si no lo contiene en faltantes lo agrega.
                        if(!PaisEditar.faltantes.Contains(calcomania.Num))
                        {
                            PaisEditar.faltantes.Add(calcomania.Num);
                        }

                        //si esta en coleccionadas lo agrega en faltantes (segun el if previamente establecido)
                        if (PaisEditar.coleccionadas.Contains(calcomania.Num))
                        {
                            PaisEditar.coleccionadas.Remove(calcomania.Num);
                        }
                    }
                    dictionary[calcomania.Pais] = PaisEditar;
                }
            Session["Diccionario1"] = dictionary; 
        }

        public ActionResult Actualizar()
        {
            dictionary = (Dictionary<string, Pais>) Session["Diccionario1"];
            dictionary2 =(Dictionary<NumCalcomania, bool>) Session["Diccionario2"]; 
            for (int i = 0; i < dictionary2.Count; i++)
            {
                ActualizarInformacion(dictionary2.Keys.ElementAt(i), dictionary2.Values.ElementAt(i));
            }
            return View(); 
        }

        public ActionResult TablaDatos()
        {
            dictionary = (Dictionary<string, Pais>)Session["Diccionario1"];
            return View(dictionary);
        }
    }
}
