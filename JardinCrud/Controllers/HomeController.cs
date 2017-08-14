using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JardinCrud.Models;
using Newtonsoft.Json;

namespace JardinCrud.Controllers
{
    public class HomeController : Controller
    {
        private senasoft2Entities db = new senasoft2Entities();

        public ActionResult Index()
        {

            return View();
        }

        public JsonResult ObtenerAcudientes()
        {
            List<AcudienteViewModel> acudientes = db.dacudiente.Select(x => new AcudienteViewModel()
            {
                ced = x.ced,
                celular = x.celular,
                correo = x.correo,
                dire = x.dire,
                nom_ape = x.nom_ape,
                tele = x.tele
            }).ToList();
            return Json(acudientes,JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult obtenerAcudientePorCedula(string cedula)
        {
            var modelo = db.dacudiente.Where(x => x.ced == cedula).SingleOrDefault();
            string valor = string.Empty;
             valor = JsonConvert.SerializeObject(modelo,Formatting.Indented,new JsonSerializerSettings{
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });
            return Json(valor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult guardarAcudiente(AcudienteViewModel acudiente)
        {
            var result = false;
          
            try
            {
                Console.WriteLine(123);
                if (acudiente.existe.Equals("existe") )
                {
               
                    dacudiente acud = db.dacudiente.Where(x => x.ced == acudiente.ced).SingleOrDefault();
                    acud.celular = acudiente.celular;
                    acud.correo = acudiente.correo;
                    acud.dire = acudiente.dire;
                    acud.nom_ape = acudiente.nom_ape;
                    acud.tele = acudiente.tele;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    dacudiente acud = new dacudiente();
                    acud.ced = acudiente.ced;
                    acud.celular = acudiente.celular;
                    acud.correo = acudiente.correo;
                    acud.dire = acudiente.dire;
                    acud.nom_ape = acudiente.nom_ape;
                    acud.tele = acudiente.tele;
                    db.dacudiente.Add(acud);
                    db.SaveChanges();
                    result = true;
                }

            }
            catch(Exception ex )
            {
                throw ex;

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult eliminarAcudiente(string cedula)
        {
            bool result = false;
            dacudiente acud = db.dacudiente.SingleOrDefault(x => x.ced == cedula);
            if (acud != null)
            {
                db.dacudiente.Remove(acud);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}