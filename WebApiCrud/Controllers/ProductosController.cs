using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCrud.Data.Interfaces;
using WebApiCrud.Dtos;
using WebApiCrud.Models;

namespace WebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly IApiRepository _repo;
        private readonly IMapper _mapper;

        public ProductosController(IApiRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listaProductos = await _repo.GetProductosAsync();
            var listaProductoDto = _mapper.Map<IEnumerable<ProductoGetListDTO>>(listaProductos);
            return Ok(listaProductoDto);
        }

        //[AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var productoId = await _repo.GetProductoByIdAsync(id);

            if (productoId == null)
            {
                return NotFound("Producto no encontrado");
            }

            //var productoDTO = new ProductoGetListIdDTO();
            //productoDTO.Id = productoId.Id;
            //productoDTO.Nombre = productoId.Nombre;
            //productoDTO.Descripcion = productoId.Descripcion;
            var productoDTO = _mapper.Map<ProductoGetListDTO>(productoId);
            return Ok(productoDTO);
        }

        //para evitar ambiguedad en el método Get genero la ruta nombre/nombre(parametro)
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> Get(string nombre)
        {
            var ProductoNombre = await _repo.GetUProductoByNombreAsync(nombre);
            if (ProductoNombre == null)
            {
                return NotFound("Producto no encontrado");
            }
            return Ok(ProductoNombre);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(ProductoCreateDTO productoDTO)
        {
            //var productoToCreate = new Producto();
            //productoToCreate.Nombre = productoDTO.Nombre;
            //productoToCreate.Descripcion = productoDTO.Descripcion;
            //productoToCreate.Precio = productoDTO.Precio;

            //_mapper.Map<Producto>(productoDTO); en <Producto> se me mapearan los datos que pase como paremetro en productoDTO
            var productoToCreate = _mapper.Map<Producto>(productoDTO);

            _repo.Add(productoToCreate);
            if (await _repo.SaveAll())
            {
                return Ok(productoToCreate);
            }
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductoUpdateDTO productoDTO)
        {
            if (id != productoDTO.Id)
            {
                return BadRequest("El Id no coincide");
            }
            var productoUpdate = await _repo.GetProductoByIdAsync(productoDTO.Id);
            if (id != productoDTO.Id)
            {
                return BadRequest();
            }
            if (productoUpdate == null)
            {
                return BadRequest("No se ha encontrado el producto");
            }

            //productoUpdate.Descripcion = productoDTO.Descripcion;
            //productoUpdate.Precio = productoDTO.Precio;

            //Acá es diferente porque actualizo los datos que obtengo en productoDto (origen) a productoUpdate (destino)
            _mapper.Map(productoDTO, productoUpdate);

            //Si no se puede grabar
            if (!await _repo.SaveAll())
            {
                return BadRequest();

            }
            else return Ok(productoUpdate); ;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ProductoId = await _repo.GetProductoByIdAsync(id);
            if (ProductoId == null)
            {
                return NotFound("Producto no encontrado");
            }
            _repo.Delete(ProductoId);
            if (!await _repo.SaveAll())
            {
                return BadRequest("No se pudo eliminar el producto");

            }
            else return Ok("Producto eliminado");
        }
    }
}
