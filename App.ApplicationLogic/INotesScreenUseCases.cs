using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface INotesScreenUseCases
    {
        Task<IEnumerable<Note>> ViewNotes(int projectId);
    }
}