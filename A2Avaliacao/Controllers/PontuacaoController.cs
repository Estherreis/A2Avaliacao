using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2Avaliacao.Data;
using A2Avaliacao.Entities;
using A2Avaliacao.Models.Pontuacao;
using Azure.Core;
using A2Avaliacao.Models.Lembrete;

namespace A2Avaliacao.Controllers
{
    /// <summary>
    /// Controller de Pontuação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PontuacaoController : ControllerBase
    {
        private readonly A2AvaliacaoContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public PontuacaoController(A2AvaliacaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as pontuações cadastradas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontuacaoResponse>>> GetPontuacao()
        {
            return await _context.Pontuacao.Include(x => x.Tarefa)
                .Select(x => new PontuacaoResponse
                {
                    Id = x.Id,
                    Pontos = x.Pontos,
                    Descricao = x.Descricao,
                    Tarefa = new
                    {
                        Id = x.Tarefa.Id,
                        Nome = x.Tarefa.Nome,
                        Concluida = x.Tarefa.Concluida,
                    }
                })
                .ToListAsync();
        }

        /// <summary>
        /// Obtém pontuação pelo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PontuacaoResponse>> GetPontuacao(int id)
        {
            var pontuacao = await _context.Pontuacao.Where(x => x.Id == id)
                .Select(x => new PontuacaoResponse
                {
                    Id = x.Id,
                    Pontos = x.Pontos,
                    Descricao = x.Descricao,
                    Tarefa = new
                    {
                        Id = x.Tarefa!.Id,
                        Nome = x.Tarefa.Nome,
                        Concluida = x.Tarefa.Concluida,
                    }
                })
                .FirstOrDefaultAsync();

            if (pontuacao == null)
            {
                return NotFound();
            }

            return pontuacao;
        }

        /// <summary>
        /// Altera pontuação
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPontuacao(int id, PontuacaoRequest request)
        {
            if (!PontuacaoExists(id))
            {
                return NotFound();
            }

            Pontuacao pontuacao = await _context.Pontuacao.FindAsync(id);
            pontuacao.Descricao = request.Descricao;
            pontuacao.Pontos = request.Pontos;
            pontuacao.Tarefa = await _context.Tarefa.FindAsync(request.TarefaId);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cadastra pontuação
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Pontuacao>> PostPontuacao(PontuacaoRequest request)
        {
            Pontuacao pontuacao = new Pontuacao();
            pontuacao.Descricao = request.Descricao;
            pontuacao.Pontos = request.Pontos;
            pontuacao.Tarefa = await _context.Tarefa.FindAsync(request.TarefaId);

            _context.Pontuacao.Add(pontuacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPontuacao", new { id = pontuacao.Id }, pontuacao);
        }

        /// <summary>
        /// Deleta pontuação
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePontuacao(int id)
        {
            var pontuacao = await _context.Pontuacao.FindAsync(id);
            if (pontuacao == null)
            {
                return NotFound();
            }

            _context.Pontuacao.Remove(pontuacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PontuacaoExists(int id)
        {
            return _context.Pontuacao.Any(e => e.Id == id);
        }
    }
}
