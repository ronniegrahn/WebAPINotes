using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface INotesScreenUseCases
    {
        Task<IEnumerable<Note>> SearchNotes(string filter);
        Task UpdateNote(Note note);
        Task<Note> ViewNoteById(int noteId);
        Task<IEnumerable<Note>> ViewNotes(int projectId);
        Task<IEnumerable<Note>> ViewOwnersNotes(int projectId, string ownerName);
    }
}