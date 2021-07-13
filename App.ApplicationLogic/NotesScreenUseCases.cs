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

        public NotesScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Note>> ViewNotes(int projectId)
        {
            return await projectRepository.GetProjectNotesAsync(projectId);
        }
    }
}
