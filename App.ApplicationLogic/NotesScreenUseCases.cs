using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class NotesScreenUseCases : INotesScreenUseCases
    {
        private readonly IProjectRepository projectRepository;
        private readonly INoteRepository noteRepository;

        public NotesScreenUseCases(IProjectRepository projectRepository, INoteRepository noteRepository)
        {
            this.projectRepository = projectRepository;
            this.noteRepository = noteRepository;
        }

        public async Task<IEnumerable<Note>> ViewNotes(int projectId)
        {
            return await projectRepository.GetProjectNotesAsync(projectId);
        }

        public async Task<IEnumerable<Note>> SearchNotes(string filter)
        {
            if(int.TryParse(filter, out int noteId))
            {
                var note = await noteRepository.GetByIdAsync(noteId);
                var notes = new List<Note>();
                notes.Add(note);
                return notes;
            }

            return await noteRepository.GetAsync(filter);
        }

        public async Task<IEnumerable<Note>> ViewOwnersNotes(int projectId, string ownerName)
        {
            return await projectRepository.GetProjectNotesAsync(projectId, ownerName);
        }
    }
}
