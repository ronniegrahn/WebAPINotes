using App.Repository;
using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ApplicationLogic
{
    public class ProjectScreenUseCases
    {
        private readonly IProjectRepository projectRepository;

        public ProjectScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public async Task<IEnumerable<Project>> ViewProjectsAsync()
        {
            return await projectRepository.GetAsync();
        }
    }
}
