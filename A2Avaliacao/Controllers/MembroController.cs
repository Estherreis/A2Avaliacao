using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2Avaliacao.Data;
using A2Avaliacao.Entities;
using A2Avaliacao.Models.Membro;
using A2Avaliacao.Models.Lembrete;

namespace A2Avaliacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembroController : ControllerBase
    {
        private readonly A2AvaliacaoContext _context;

        public MembroController(A2AvaliacaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os membros
        /// </summary>
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembroResponse>>> GetMembro()
        {
            return await _context.Membro.Include(x => x.Lembretes).Select(x => new MembroResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email,
                Lembrete = x.Lembretes!.Select(l => new
                {
                    IdLembrete = l.Id,
                    DataHora = l.DataHora,
                    Mensagem = l.Mensagem
                }).ToList()
            }).ToListAsync();
        }

        /// <summary>
        /// Obtém membro pelo id
        /// </summary>
        /// 
        [HttpGet("{id}")]
        public async Task<ActionResult<MembroResponse>> GetMembro(int id)
        {
            var membro = await _context.Membro
                .Where(x => x.Id == id)
                .Select(x => new MembroResponse
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                    Lembrete = x.Lembretes!.Select(l => new
                    {
                        IdLembrete = l.Id,
                        DataHora = l.DataHora,
                        Mensagem = l.Mensagem
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (membro == null)
            {
                return NotFound();
            }

            return membro;
        }

        /// <summary>
        /// Altera membro
        /// </summary>
        /// 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembro(int id, MembroRequest request)
        {
            if (!MembroExists(id))
            {
                return NotFound();
            }

            Membro membro = await _context.Membro.FindAsync(id);
            membro.Nome = request.Nome;
            request.Email = request.Email;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cadastra membro
        /// </summary>
        /// 
        [HttpPost]
        public async Task<ActionResult<Membro>> PostMembro(MembroRequest request)
        {
            Membro membro = new Membro();
            membro.Email = request.Email;
            membro.Nome = request.Nome;

            _context.Membro.Add(membro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMembro", new { id = membro.Id }, membro);
        }

        /// <summary>
        /// Deleta membro
        /// </summary>
        /// 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembro(int id)
        {
            var membro = await _context.Membro.FindAsync(id);
            if (membro == null)
            {
                return NotFound();
            }

            _context.Membro.Remove(membro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MembroExists(int id)
        {
            return _context.Membro.Any(e => e.Id == id);
        }
    }
}
