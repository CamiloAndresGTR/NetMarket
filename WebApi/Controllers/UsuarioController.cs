using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApi.Errors;

namespace WebApi.Controllers
{
   
    public class UsuarioController : BaseApiController
    {
        /// <summary>
        /// Instanciar los objetos userManager basado en Usuario y SignInManager
        /// </summary>
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;


        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario =  await _userManager.FindByEmailAsync(loginDto.Email);

            if (usuario == null)
            { 
                return Unauthorized(new CodeErrorResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password,false);
            if (!result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }
            return new UsuarioDto() 
            { 
             Email = usuario.Email,
             UserName = usuario.UserName,
             Token = "Este es el token de usuario",
             Nombre = usuario.Nombre,
             Apellido = usuario.Apellido
            };
        }

        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Resgistro(RegistrarDto registrarDto)
        {
            var usuario = new Usuario()
            {
                Nombre = registrarDto.Nombre,
                Apellido = registrarDto.Apellido,
                UserName = registrarDto.UserName,
                Email = registrarDto.Email
            };
            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);
            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
            }
            return new UsuarioDto()
            { 
                Nombre = usuario.Nombre,
                Apellido= usuario.Apellido,
                Email = usuario.Email,
                Token = "Un token de usuario",
                UserName = usuario.UserName
            };
            
        
        }


    }
}
