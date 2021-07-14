using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class NoteScreenUseCases : INoteScreenUseCases
    {
        private readonly INoteRepository noteRepository;

        public NoteScreenUseCases(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public async Task<int> AddNote(Note note)
        {
            return await this.noteRepository.CreateAsync(note);
        }

        public async Task DeleteNote(int noteId)
        {
            await this.noteRepository.DeleteAsync(noteId);
        }
    }
}
