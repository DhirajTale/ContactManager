using ContactMangerEvolent.Models;
using ContactMangerEvolent.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ContactMangerEvolent.Controllers
{
    public class HomeController : Controller
    {
        private List<ContactModel> contacts = new List<ContactModel>();
        // GET: Home
        public ActionResult Index()
        {
            return View(contacts);
        }

        public async Task <ActionResult> GetContacts()
        {
            try
            {
                ContactsRepository ContactsRepo = new ContactsRepository();
                ModelState.Clear();

                return View(await ContactsRepo.GetContacts());
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task <ActionResult> AddContact(ContactModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactsRepository contactRepo = new ContactsRepository();

                    if (await contactRepo.Add(contact))
                    {
                        ViewBag.Message = "Contact details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task <ActionResult> EditContactDetails(int id)
        {
            ContactsRepository ContactsRepo = new ContactsRepository();
            List<ContactModel> contacts = await ContactsRepo.GetContacts();
            return View(contacts.Find(Ct => Ct.id == id));
        }

        [HttpPost]
        public async Task <ActionResult> EditContactDetails(ContactModel contact)
        {
            ContactsRepository ContactsRepo = new ContactsRepository();
            try
            {
                if (await ContactsRepo.Update(contact))
                {
                    ViewBag.Message = "Contact details Updated successfully";
                }
                return RedirectToAction("GetContacts");
            }
            catch
            {
                return View("Error");
            }

        }

        public async Task <ActionResult> DeleteContact(int id)
        {
            try
            {
                ContactsRepository ContactsRepo = new ContactsRepository();
                if (await ContactsRepo.Delete(id))
                {
                    ViewBag.AlertMsg = "Contact details deleted successfully";

                }
                return RedirectToAction("GetContacts");
            }
            catch
            {
                return View("Error");
            }
        }
     
    }
}
