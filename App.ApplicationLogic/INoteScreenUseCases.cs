using Core.Models;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface INoteScreenUseCases
    {
        Task<int> AddNote(Note note);
        Task DeleteNote(int noteId);
    }
}