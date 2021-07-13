using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IWebApiExecuter webApiExecutor;

        public NoteRepository(IWebApiExecuter webApiExecutor)
        {
            this.webApiExecutor = webApiExecutor;
        }

        public async Task<IEnumerable<Note>> GetAsync(string filter = null)
        {
            string uri = "api/notes?api-version=2.0";
            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleordescription={filter.Trim()}";

            return await webApiExecutor.InvokeGet<IEnumerable<Note>>(uri);
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await webApiExecutor.InvokeGet<Note>($"api/notes/{id}?api-version=2.0");
        }

        public async Task<int> CreateAsync(Note note)
        {
            note = await webApiExecutor.InvokePost("api/notes?api-version=2.0", note);
            return note.NoteId.Value;
        }

        public async Task UpdateAsync(Note note)
        {
            await webApiExecutor.InvokePut($"api/notes/{note.NoteId}?api-version=2.0", note);
        }

        public async Task DeleteAsync(int id)
        {
            await webApiExecutor.InvokeDelete($"api/notes/{id}?api-version=2.0");
        }
    }
}
