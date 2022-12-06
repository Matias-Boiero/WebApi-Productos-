using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCrud.Data.Interfaces;
using WebApiCrud.Dtos;
using WebApiCrud.Models;
using WebApiCrud.Services.Interfaces;

namespace WebApiCrud.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IApiRepository _apiRepository;


        public AuthController(IAuthRepository repo, ITokenService tokenService, IMapper mapper, IApiRepository apiRepository)
        {
            _repo = repo;
            _tokenService = tokenService;
            _mapper = mapper;
            _apiRepository = apiRepository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegisterDTO usuarioDTO)
        {
            //normalizo el email 
            usuarioDTO.Email = usuarioDTO.Email.ToLower();
            //Si existe el usuario envio un BadRecquest porque el usuario ya existe y no se puede volver a registrarse
            if (await _repo.ExisteUsuario(usuarioDTO.Email))
            {
                return BadRequest("El usuario ya se encuentra registrado");
            }
            //Si el usuario no existe lo registro
            //mapeo al usuarioDTO como Usuario
            var usuarioNuevo = _mapper.Map<Usuario>(usuarioDTO);
            //Registro al usuario nuevo
            var usuarioCreado = await _repo.Registrar(usuarioNuevo, usuarioDTO.Contraseña);
            //mapeo al usuario creado en <UsuarioListDTO> para que lo muestre a quien consume el api con datos especificos
            var usuarioCreadoDTO = _mapper.Map<UsuarioListDTO>(usuarioCreado);
            return Ok(usuarioCreadoDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var usuarioFromRepo = await _repo.Login(usuarioLoginDTO.Email, usuarioLoginDTO.Contraseña);
            if (usuarioFromRepo == null)
                return Unauthorized();
            var usuario = _mapper.Map<UsuarioListDTO>(usuarioFromRepo);
            var token = _tokenService.CreateToken(usuarioFromRepo);
            return Ok(new
            {
                token = token,
                usuario = usuario,
            });
        }

        [HttpGet("listUsers")]
        public async Task<IActionResult> Get()
        {
            var listaUsuarios = await _apiRepository.GetUsuariosAsync();
            var listaUsuariosDTO = _mapper.Map<IEnumerable<UsuarioListDTO>>(listaUsuarios);
            return Ok(listaUsuariosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var usuarioId = await _apiRepository.GetUsuarioByIdAsync(id);

            if (usuarioId == null)
            {
                return NotFound("Usuario no encontrado");
            }


            var usuarioDTO = _mapper.Map<UsuarioListDTO>(usuarioId);
            return Ok(usuarioDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioUpdateDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id)
            {
                return BadRequest("El Id no coincide");
            }
            var usuarioUpdate = await _apiRepository.GetUsuarioByIdAsync(usuarioDTO.Id);
            if (id != usuarioDTO.Id)
            {
                return BadRequest();
            }
            if (usuarioUpdate == null)
            {
                return BadRequest("No se ha encontrado el usuario");
            }


            //Acá es diferente porque actualizo los datos que obtengo en usuarioDto (origen) a usuarioUpdate (destino)
            _mapper.Map(usuarioDTO, usuarioUpdate);

            //Si no se puede grabar
            if (!await _apiRepository.SaveAll())
            {
                return BadRequest();

            }
            else return Ok(usuarioUpdate); ;
        }

    }
}
