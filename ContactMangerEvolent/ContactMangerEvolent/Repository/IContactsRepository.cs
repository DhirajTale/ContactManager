using ContactMangerEvolent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMangerEvolent.Repository
{
    interface IContactsRepository
    {
        Task<bool> Add(ContactModel contact);
        Task<bool> Update(ContactModel employee);
        Task<bool> Delete(int id);
        Task<ContactModel> GetContact(string id);
        Task<List<ContactModel>> GetContacts();
    }
}
