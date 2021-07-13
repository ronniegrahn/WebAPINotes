using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface IProjectScreenUseCases
    {
        Task<IEnumerable<Project>> ViewProjectsAsync();
    }
}