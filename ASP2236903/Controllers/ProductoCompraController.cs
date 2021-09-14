using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;

namespace ASP2236903.Controllers
{
    [Authorize]
    public class ProductoCompraController : Controller
    {
            // GET: Producto
            public ActionResult Index()
            {
                using (var db = new inventario2021Entities())
                {
                    return View(db.producto_compra.ToList());
                }
            }

            public static int IdCompra(int idCompra)
            {
                using (var db = new inventario2021Entities())
                {
                    return db.compra.Find(idCompra).id;
                }
            }

            public ActionResult ListarCompras()
            {
                using (var db = new inventario2021Entities())
                {
                    return PartialView(db.compra.ToList());
                }
            }

            public static string NombreProducto(int idProducto)
            {
                using (var db = new inventario2021Entities())
                {
                    return db.producto.Find(idProducto).nombre;
                }
            }

            public ActionResult ListarProductos()
            {
                using (var db = new inventario2021Entities())
                {
                    return PartialView(db.producto.ToList());
                }
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(producto_compra productoCompra)
            {
                if (!ModelState.IsValid)
                    return View();

                try
                {
                    using (var db = new inventario2021Entities())
                    {
                        db.producto_compra.Add(productoCompra);
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
                    return View(db.producto_compra.Find(id));
                }
            }

            public ActionResult Edit(int id)
            {
                using (var db = new inventario2021Entities())
                {
                    producto_compra productoCompraEdit = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(productoCompraEdit);
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(producto_compra productoCompraEdit)
            {
                try
                {
                    using (var db = new inventario2021Entities())
                    {
                        var oldProduct = db.producto_compra.Find(productoCompraEdit.id);
                        oldProduct.compra = productoCompraEdit.compra;
                        oldProduct.cantidad = productoCompraEdit.cantidad;
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

            public ActionResult Delete(int id)
            {
                try
                {
                    using (var db = new inventario2021Entities())
                    {
                        producto_compra productoCompraEdit = db.producto_compra.Find(id);
                        db.producto_compra.Remove(productoCompraEdit);
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
    }
}