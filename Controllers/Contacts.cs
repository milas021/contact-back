using Contacts.Context;
using Contacts.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Contacts : ControllerBase
    {
        private readonly AppDbContext _context;
        public Contacts(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Add(Model.Contact contact)
        {
            contact.Id = Guid.NewGuid();
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok();


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Contacts.ToListAsync();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _context.Contacts.SingleOrDefaultAsync(x => x.Id == id);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var contact = await _context.Contacts.SingleOrDefaultAsync(x=>x.Id == id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(EditContactCommand command)
        {
            var contact = await _context.Contacts.SingleOrDefaultAsync(x => x.Id == command.Id);
            if (contact == null)
                return BadRequest();

            contact.Name = command.Name;
            contact.Family = command.Family;
            contact.Phone = command.Phone;
            contact.Email = command.Email;

            await _context.SaveChangesAsync();

            return Ok();
        }




    }

    public class EditContactCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
