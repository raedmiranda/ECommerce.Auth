using ECommerce.Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace ECommerce.Auth.Controllers
{
    public class LoginController : Controller
    {
        const string LoginName = "_User";
        private readonly IConfiguration _IConfiguration;

        public LoginController(IConfiguration configuration)
        {
            _IConfiguration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UsuarioModel());
        }


        [HttpPost]
        public IActionResult Index(UsuarioModel model)
        {
            string cn = _IConfiguration["ConnectionStrings:Development"];
            if (!string.IsNullOrEmpty(cn))
                return RedirectToAction("Home", "Error");

            using (SqlConnection connection = new SqlConnection(cn))
            {
                if (string.IsNullOrWhiteSpace(model.NombreUsuario) || string.IsNullOrWhiteSpace(model.Clave))
                {
                    ModelState.AddModelError("", "El nombre de usuario o clave no deben estar vacíos.");
                }
                else
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DevolverUsuario", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@usuario", model.NombreUsuario);
                    command.Parameters.AddWithValue("@contrasena", model.Clave);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        HttpContext.Session.SetString(LoginName, model.NombreUsuario);
                        return RedirectToAction("Home", "Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Datos ingresados no son válidos.");
                    }
                }
            }

            return View(model);
            //return RedirectToAction(nameof(Index));
        }
    }
}
