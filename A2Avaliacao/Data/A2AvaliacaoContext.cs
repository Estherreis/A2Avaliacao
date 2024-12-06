using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using A2Avaliacao.Entities;

namespace A2Avaliacao.Data
{
    public class A2AvaliacaoContext : DbContext
    {
        public A2AvaliacaoContext (DbContextOptions<A2AvaliacaoContext> options)
            : base(options)
        {
        }

        public DbSet<A2Avaliacao.Entities.Lembrete> Lembrete { get; set; } = default!;
        public DbSet<A2Avaliacao.Entities.Membro> Membro { get; set; } = default!;
        public DbSet<A2Avaliacao.Entities.Pontuacao> Pontuacao { get; set; } = default!;
        public DbSet<A2Avaliacao.Entities.Recompensa> Recompensa { get; set; } = default!;
        public DbSet<A2Avaliacao.Entities.Reuniao> Reuniao { get; set; } = default!;
        public DbSet<A2Avaliacao.Entities.Tarefa> Tarefa { get; set; } = default!;
        public DbSet<A2Avaliacao.Entities.MembroReuniao> MembroReuniao { get; set; } = default!;
    }
}
