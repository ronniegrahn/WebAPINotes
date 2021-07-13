using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface INotesScreenUseCases
    {
        Task<IEnumerable<Note>> SearchNotes(string filter);
        Task<IEnumerable<Note>> ViewNotes(int projectId);
    }
}