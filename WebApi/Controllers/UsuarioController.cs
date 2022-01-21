﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApi.Errors;
using WebApi.Extensions;

namespace WebApi.Controllers
{
   
    public class UsuarioController : BaseApiController
    {
        /// <summary>
        /// Instanciar los objetos userManager basado en Usuario, SignInManager y tokenService
        /// </summary>
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;


        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            
        }

        /// <summary>
        /// Método para el incio de sesión
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
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
             Token = _tokenService.CreateToken(usuario),
             Nombre = usuario.Nombre,
             Apellido = usuario.Apellido
            };
        }

        /// <summary>
        /// Método para el registro de un nuevo usuario
        /// </summary>
        /// <param name="registrarDto"></param>
        /// <returns></returns>
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
                Token = _tokenService.CreateToken(usuario),
                UserName = usuario.UserName
            };
            
        
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var usuario = await _userManager.FindByEmailAsync(email);

            var usuario = await _userManager.BuscarUsuarioAsync(HttpContext.User);

            return new UsuarioDto()
            {
                Nombre=usuario.Nombre,
                Apellido=usuario.Apellido,
                Email=usuario.Email,
                UserName = usuario.UserName,
                Token = _tokenService.CreateToken(usuario)
            };
        }


        /// <summary>
        /// Método para validar si un email ya está registrado en la base de datos
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("emailvalido")]
        public async Task<ActionResult<bool>> ValidarEmail([FromQuery]string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null) return false;
            return true;
        }

        [Authorize]
        [HttpGet("direccion")]
        public async Task<ActionResult<DireccionDto>> GetDireccion()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var usuario = await _userManager.FindByEmailAsync(email);
            var usuario = await _userManager.BuscarUsuarioConDireccionAsync(HttpContext.User);

            return _mapper.Map<Direccion, DireccionDto>(usuario.Direccion);
        }

        [Authorize]
        [HttpPut("direccion")]
        public async Task<ActionResult<DireccionDto>> PutDireccion(DireccionDto direccion)
        {
            var usuario = await _userManager.BuscarUsuarioConDireccionAsync(HttpContext.User);
            usuario.Direccion = _mapper.Map<DireccionDto, Direccion>(direccion);
            var resultado = await _userManager.UpdateAsync(usuario);
            if (resultado.Succeeded) return Ok(_mapper.Map<Direccion, DireccionDto>(usuario.Direccion));
            return BadRequest("No se pudo actualizar la dirección");
            

            


        }
    }
}