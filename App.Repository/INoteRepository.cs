using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Repository
{
    public interface INoteRepository
    {
        Task<int> CreateAsync(Note note);
        Task DeleteAsync(int id);
        Task<IEnumerable<Note>> GetAsync(string filter = null);
        Task<Note> GetByIdAsync(int id);
        Task UpdateAsync(Note note);
    }
}