using EquipeFacil.Data;
using EquipeFacil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EquipeFacil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboradorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/colaboradores/test-connection
        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Tenta consultar o banco de dados
                var canConnect = await _context.Database.CanConnectAsync();
                if (canConnect)
                {
                    return Ok("Conexão com o banco de dados bem-sucedida.");
                }
                else
                {
                    return StatusCode(500, "Não foi possível conectar ao banco de dados.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar conectar ao banco de dados: {ex.Message}");
            }
        }
        // GET: api/colaboradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradores()
        {
            return await _context.Colaboradores.ToListAsync();
        }

        // GET: api/colaboradores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }

        // POST: api/colaboradores
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
            if (!colaborador.MaiorDeIdade())
            {
                return BadRequest("O colaborador deve ser maior de 18 anos.");
            }

            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColaborador", new { id = colaborador.Id }, colaborador);
        }

        // PUT: api/colaboradores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.Id)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/colaboradores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}