using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        
        public IActionResult listaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }        

        public IActionResult editarUsuario(int id) {
            return View(new UsuarioService().Listar(id));    
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario usuarioEdicao) {
            UsuarioService us = new UsuarioService();
            us.editarUsuario(usuarioEdicao);
            return RedirectToAction("listaDeUsuarios","Usuarios");
        }

        public IActionResult registrarUsuarios() {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();            
        }

        [HttpPost]
        public IActionResult registrarUsuarios(Usuario novoUser)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            novoUser.Senha = Criptografia.TextoCriptografado(novoUser.Senha);

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);

            return RedirectToAction("cadastroRealizado");
        }        

        public IActionResult excluirUsuario(int id) {
            return View(new UsuarioService().Listar(id));    
        }

        [HttpPost]
        public IActionResult excluirUsuario(string decisao,int id)
        {
            if(decisao=="EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão do usuario "+ new UsuarioService().Listar(id).Nome + " realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios",new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão cancelada";
                return View("ListaDeUsuarios",new UsuarioService().Listar());
            }
        }

        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }

        public IActionResult needAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult sair(){
           HttpContext.Session.Clear();
           return RedirectToAction("Index","Home"); 
        }

    }
}