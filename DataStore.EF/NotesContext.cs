using Microsoft.EntityFrameworkCore;
using Core.Models;
using System;

namespace DataStore.EF
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Notes);

            modelBuilder.Entity<Project>().HasData(
                    new Project { ProjectId = 1, Name = "Project 1" },
                    new Project { ProjectId = 2, Name = "Project 2" }
                );

            modelBuilder.Entity<Note>().HasData(
                    new Note { NoteId = 1, Title = "Hello World!", ProjectId = 1 },
                    new Note { NoteId = 2, Title = "Second note.", ProjectId = 1 },
                    new Note { NoteId = 3, Title = "Second note.", ProjectId = 2 }
                );
        }
    }
}
