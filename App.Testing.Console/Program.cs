using MyApp.Repository;
using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:44377", httpClient);


await TestNotes();

async Task TestNotes()
{
    Console.WriteLine("---------------------------");
    Console.WriteLine("Reading notes...");
    await GetNotes();

    Console.WriteLine("---------------------------");
    Console.WriteLine("Reading note 1...");
    await GetNoteById(1);

    Console.WriteLine("---------------------------");
    Console.WriteLine("Create a note...");
    var tId = await CreateNote();
    await GetNotes();

    Console.WriteLine("---------------------------");
    Console.WriteLine("Updating note...");
    var note = await GetNoteById(tId);
    await UpdateNote(note);
    await GetNotes();

    Console.WriteLine("---------------------------");
    Console.WriteLine("Deleting a note...");
    await DeleteNote(tId);
    await GetNotes();

}

async Task TestProjects()
{
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
}

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

async Task GetNotes(string filter = null)
{
    NoteRepository noteRepository = new(apiExecuter);
    var notes = await noteRepository.GetAsync(filter);
    foreach (var note in notes)
    {
        Console.WriteLine($"Note: {note.Title}");
    }
}

async Task<Note> GetNoteById(int id)
{
    NoteRepository noteRepository = new(apiExecuter);
    var note = await noteRepository.GetByIdAsync(id);
    return note;
}

async Task<int> CreateNote()
{
    NoteRepository noteRepository = new(apiExecuter);
    return await noteRepository.CreateAsync(new Note { ProjectId = 2, Title = "This is a test note!", Description = "Testing testing." });
}

async Task UpdateNote(Note note)
{
    NoteRepository noteRepository = new NoteRepository(apiExecuter);
    note.Title += " Updated!";
    await noteRepository.UpdateAsync(note);
}

async Task DeleteNote(int id)
{
    NoteRepository noteRepository = new(apiExecuter);
    await noteRepository.DeleteAsync(id);
}