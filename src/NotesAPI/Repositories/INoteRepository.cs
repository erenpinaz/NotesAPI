using System;
using System.Collections.Generic;
using NotesAPI.Models;

namespace NotesAPI.Repositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAll();
        Note GetNoteById(Guid id);
        void Add(Note item);
        void Update(Note item);
        Note Remove(Guid id);
    }
}
