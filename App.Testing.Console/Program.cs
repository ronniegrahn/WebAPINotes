using App.Repository;
using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:44377", httpClient);

Console.WriteLine("---------------------------");
Console.WriteLine("Reading projects...");
await GetProjects();

Console.WriteLine("---------------------------");
Console.WriteLine("Reading project notes...");
await GetProjectNotes(1);

Console.WriteLine("---------------------------");
Console.WriteLine("Creating new project...");
var pId = await CreateProject();
await GetProjects();

Console.WriteLine("---------------------------");
Console.WriteLine("Update project...");
var project = await GetProject(pId);
await UpdateProject(project);

await GetProjects();

Console.WriteLine("---------------------------");
Console.WriteLine("Delete a project...");
await DeleteProject(pId);

await GetProjects();

async Task GetProjects()
{
    ProjectRepository repository = new(apiExecuter);

    var projects = await repository.GetAsync();

    foreach (var project in projects)
    {
        Console.WriteLine($"Project: {project.Name}");
    }
}

async Task<Project> GetProject(int id)
{
    ProjectRepository repository = new(apiExecuter);
    return await repository.GetByIdAsync(id);
}

async Task GetProjectNotes(int id)
{
    var project = await GetProject(id);
    Console.WriteLine($"Project: {project.Name}");

    ProjectRepository repository = new(apiExecuter);
    var notes = await repository.GetProjectNotesAsync(id);
    foreach (var note in notes)
    {
        Console.WriteLine($"    Note: {note.Title}");
    }
}

async Task<int> CreateProject()
{
    var project = new Project { Name = "This is another project" };
    ProjectRepository repository = new(apiExecuter);
    return await repository.CreateAsync(project);
}

async Task UpdateProject(Project project)
{
    ProjectRepository repository = new(apiExecuter);
    project.Name += " updated!";
    await repository.UpdateAsync(project);
}

async Task DeleteProject(int id)
{
    ProjectRepository repository = new(apiExecuter);
    await repository.DeleteAsync(id);
}
