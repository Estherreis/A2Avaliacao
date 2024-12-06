using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2Avaliacao.Data;
using A2Avaliacao.Entities;
using A2Avaliacao.Models.Tarefa;
using Azure.Core;

namespace A2Avaliacao.Controllers
{
    /// <summary>
    /// Controller de Tarefa
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly A2AvaliacaoContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public TarefaController(A2AvaliacaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as tarefas cadastradas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefa()
        {
            return await _context.Tarefa.ToListAsync();
        }

        /// <summary>
        /// Obtém tarefa pelo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        /// <summary>
        /// Altera tarefa
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, TarefaRequest request)
        {
            if (!TarefaExists(id))
            {
                return NotFound();
            }

            Tarefa tarefa = await _context.Tarefa.FindAsync(id);
            tarefa.Descricao = request.Descricao;
            tarefa.DataCriacao = request.DataCriacao;
            tarefa.DataConclusao = request.DataConclusao;
            tarefa.Concluida = request.Concluida;
            tarefa.Nome = request.Nome;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cadastra tarefa
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(TarefaRequest request)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Descricao = request.Descricao;
            tarefa.DataCriacao = request.DataCriacao;
            tarefa.DataConclusao = request.DataConclusao;
            tarefa.Concluida = request.Concluida;
            tarefa.Nome = request.Nome;

            _context.Tarefa.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarefa", new { id = tarefa.Id }, tarefa);
        }

        /// <summary>
        /// Deleta tarefa
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefa.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefa.Any(e => e.Id == id);
        }
    }
}
