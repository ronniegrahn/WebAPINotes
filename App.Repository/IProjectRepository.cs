using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Repository
{
    public interface IProjectRepository
    {
        Task<int> CreateAsync(Project project);
        Task DeleteAsync(int id);
        Task<IEnumerable<Project>> GetAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Note>> GetProjectNotesAsync(int projectId);
        Task UpdateAsync(Project project);
    }
}