using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2Avaliacao.Data;
using A2Avaliacao.Entities;
using A2Avaliacao.Models.Lembrete;

namespace A2Avaliacao.Controllers
{
    /// <summary>
    /// Controller de Lembrete
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LembreteController : ControllerBase
    {
        private readonly A2AvaliacaoContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public LembreteController(A2AvaliacaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os lembretes cadastrados
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LembreteResponse>>> GetLembrete()
        {
            return await _context.Lembrete.Include(x => x.Membro).Select(x => new LembreteResponse
            {
                Id = x.Id,
                DataHora = x.DataHora,
                Mensagem = x.Mensagem,
                NomeMembro = x.Membro!.Nome,
                EmailMembro = x.Membro.Email
            }).ToListAsync();
        }

        /// <summary>
        /// Obtém lembrete pelo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LembreteResponse>> GetLembrete(int id)
        {
            var lembrete = await _context.Lembrete
                .Where(x => x.Id == id)
                .Select(x => new LembreteResponse
                {
                    Id = x.Id,
                    DataHora = x.DataHora,
                    Mensagem = x.Mensagem,
                    NomeMembro = x.Membro!.Nome,
                    EmailMembro = x.Membro.Email
                })
                .FirstOrDefaultAsync();

            if (lembrete == null)
            {
                return NotFound();
            }

            return lembrete;
        }

        /// <summary>
        /// Altera lembrete
        /// </summary>
        ///
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLembrete(int id, LembreteRequest request)
        {
            if (!LembreteExists(id))
            {
                return NotFound();
            }

            Lembrete lembrete = await _context.Lembrete.FindAsync(id);
            lembrete.Membro = await _context.Membro.FindAsync(request.MembroId);
            lembrete.DataHora = request.DataHora;
            lembrete.Mensagem = request.Mensagem;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cadastra lembrete
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Lembrete>> PostLembrete(LembreteRequest request)
        {
            Lembrete lembrete = new Lembrete();
            lembrete.Membro = await _context.Membro.FindAsync(request.MembroId);
            lembrete.DataHora = request.DataHora;
            lembrete.Mensagem = request.Mensagem;

            _context.Lembrete.Add(lembrete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLembrete", new { id = lembrete.Id }, lembrete);
        }

        /// <summary>
        /// Deleta lembrete
        /// </summary>
        /// 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLembrete(int id)
        {
            var lembrete = await _context.Lembrete.FindAsync(id);
            if (lembrete == null)
            {
                return NotFound();
            }

            _context.Lembrete.Remove(lembrete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LembreteExists(int id)
        {
            return _context.Lembrete.Any(e => e.Id == id);
        }
    }
}
