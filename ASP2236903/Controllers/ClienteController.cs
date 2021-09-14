﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;
using Rotativa;

namespace ASP2236903.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
                return View(db.cliente.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.cliente.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findCliente = db.cliente.Find(id);
                return View(findCliente);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findCliente = db.cliente.Find(id);
                    db.cliente.Remove(findCliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    cliente findCliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findCliente);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente editCliente)
        {
            try
            {

                using (var db = new inventario2021Entities())
                {
                    cliente cliente = db.cliente.Find(editCliente.id);

                    cliente.nombre = editCliente.nombre;
                    cliente.documento = editCliente.documento;
                    cliente.email = editCliente.email;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        public ActionResult Reporte()
        {
            try
            {
                var db = new inventario2021Entities();
                var query = from tabCliente in db.cliente
                            join tabCompra in db.compra on tabCliente.id equals tabCompra.id_cliente
                            select new Reporte
                            {
                                nombreCliente = tabCliente.nombre,
                                DocumentoCliente = tabCliente.documento,
                                EmailCliente = tabCliente.email,
                                fechaCompra = tabCompra.fecha,
                                totalCompra = tabCompra.total,
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        public ActionResult PdfReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "reporte.pdf" };
        }
    }
}