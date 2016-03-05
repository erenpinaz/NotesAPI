using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using NotesAPI.Models;
using NotesAPI.Repositories;

namespace NotesAPI.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        [FromServices]
        public INoteRepository NoteRepository { get; set; }

        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return NoteRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetById(Guid id)
        {
            var item = NoteRepository.GetNoteById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Note item)
        {
            if (item == null)
            {
                return HttpBadRequest();
            }

            NoteRepository.Add(item);
            return CreatedAtRoute("GetNote", new { controller = "Notes", id = item.Id.ToString() }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody]Note item)
        {
            if (item == null || item.Id != id)
            {
                return HttpBadRequest();
            }

            var note = NoteRepository.GetNoteById(id);
            if (note == null)
            {
                return HttpNotFound();
            }

            NoteRepository.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            NoteRepository.Remove(id);
        }
    }
}
