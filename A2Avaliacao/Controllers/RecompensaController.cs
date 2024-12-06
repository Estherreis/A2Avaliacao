using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2Avaliacao.Data;
using A2Avaliacao.Entities;
using A2Avaliacao.Models.Recompensa;
using Azure.Core;

namespace A2Avaliacao.Controllers
{
    /// <summary>
    /// Controller de Recompensa
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecompensaController : ControllerBase
    {
        private readonly A2AvaliacaoContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public RecompensaController(A2AvaliacaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as recompensas cadastradas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recompensa>>> GetRecompensa()
        {
            return await _context.Recompensa.ToListAsync();
        }

        /// <summary>
        /// Obtém recompensa pelo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Recompensa>> GetRecompensa(int id)
        {
            var recompensa = await _context.Recompensa.FindAsync(id);

            if (recompensa == null)
            {
                return NotFound();
            }

            return recompensa;
        }

        /// <summary>
        /// Altera recompensa
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecompensa(int id, RecompensaRequest request)
        {
            if (!RecompensaExists(id))
            {
                return NotFound();
            }

            Recompensa recompensa = await _context.Recompensa.FindAsync(id);
            recompensa.CustoEmPontos = request.CustoEmPontos;
            recompensa.Descricao = request.Descricao;
            recompensa.Nome = request.Nome;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cadastra recompensa
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Recompensa>> PostRecompensa(RecompensaRequest request)
        {
            Recompensa recompensa = new Recompensa();
            recompensa.CustoEmPontos = request.CustoEmPontos;
            recompensa.Descricao = request.Descricao;
            recompensa.Nome = request.Nome;

            _context.Recompensa.Add(recompensa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecompensa", new { id = recompensa.Id }, recompensa);
        }

        /// <summary>
        /// Deleta recompensa
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecompensa(int id)
        {
            var recompensa = await _context.Recompensa.FindAsync(id);
            if (recompensa == null)
            {
                return NotFound();
            }

            _context.Recompensa.Remove(recompensa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecompensaExists(int id)
        {
            return _context.Recompensa.Any(e => e.Id == id);
        }
    }
}
