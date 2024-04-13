using System.Reflection.Metadata.Ecma335;

namespace Contacts.Model
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
