using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2Avaliacao.Data;
using A2Avaliacao.Entities;
using A2Avaliacao.Models.Reuniao;
using System.Collections.ObjectModel;

namespace A2Avaliacao.Controllers
{
    /// <summary>
    /// Controller de Reunião
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReuniaoController : ControllerBase
    {
        private readonly A2AvaliacaoContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ReuniaoController(A2AvaliacaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as reuniões cadastradas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReuniaoResponse>>> GetReuniao()
        {
            return await _context.Reuniao.Include(x => x.MembroReunioes)!.ThenInclude(x => x.Membro)
                .Select(x => new ReuniaoResponse
                {
                    Id = x.Id,
                    DataHora = x.DataHora,
                    Titulo = x.Titulo,
                    Notas = x.Notas,
                    Membros = x.MembroReunioes!.Select(x => new {
                        Id = x.Membro.Id,
                        Nome = x.Membro.Nome,
                    })
                })
                .ToListAsync();
        }

        /// <summary>
        /// Obtém reunião pelo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReuniaoResponse>> GetReuniao(int id)
        {
            var reuniao = await _context.Reuniao.Where(x => x.Id == id)
                .Select(x => new ReuniaoResponse
                {
                    Id = x.Id,
                    DataHora = x.DataHora,
                    Titulo = x.Titulo,
                    Notas = x.Notas,
                    Membros = x.MembroReunioes!.Select(x => new {
                        Id = x.Membro.Id,
                        Nome = x.Membro.Nome,
                    })
                })
                .FirstOrDefaultAsync();

            if (reuniao == null)
            {
                return NotFound();
            }

            return reuniao;
        }

        /// <summary>
        /// Altera reunião
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReuniao(int id, ReuniaoRequest request)
        {
            if (!ReuniaoExists(id))
            {
                return NotFound();
            }

            Reuniao reuniao = await _context.Reuniao.FindAsync(id);
            reuniao.Titulo = request.Titulo;
            reuniao.DataHora = request.DataHora;
            reuniao.Notas = request.Notas;
            _context.Reuniao.Add(reuniao);

            ICollection<MembroReuniao> membros = new List<MembroReuniao>();

            foreach (var membroId in request.MembrosId)
            {
                MembroReuniao membroReuniao = new MembroReuniao();
                membroReuniao.Membro = await _context.Membro.FindAsync(membroId);
                membroReuniao.Reuniao = reuniao;

                _context.MembroReuniao.Add(membroReuniao);
                membros.Add(membroReuniao);
            }

            reuniao.MembroReunioes = membros;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cadastra reunião
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Reuniao>> PostReuniao(ReuniaoRequest request)
        {
            Reuniao reuniao = new Reuniao();
            reuniao.Titulo = request.Titulo;
            reuniao.DataHora = request.DataHora;
            reuniao.Notas = request.Notas;
            _context.Reuniao.Add(reuniao);

            ICollection<MembroReuniao> membros = new List<MembroReuniao>();

            foreach (var membroId in request.MembrosId)
            {
                MembroReuniao membroReuniao = new MembroReuniao();
                membroReuniao.Membro = await _context.Membro.FindAsync(membroId);
                membroReuniao.Reuniao = reuniao;

                _context.MembroReuniao.Add(membroReuniao);
                membros.Add(membroReuniao);
            }

            reuniao.MembroReunioes = membros;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReuniao", new { id = reuniao.Id }, reuniao);
        }

        /// <summary>
        /// Deleta reunião
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReuniao(int id)
        {
            var reuniao = await _context.Reuniao.FindAsync(id);
            if (reuniao == null)
            {
                return NotFound();
            }

            _context.Reuniao.Remove(reuniao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReuniaoExists(int id)
        {
            return _context.Reuniao.Any(e => e.Id == id);
        }
    }
}
