using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerAPI.Utility;
using ContactManagerAPI.Model;
using System.Data.SqlClient;

namespace ContactManagerAPI.Repository
{
    public class ContactDBClient
    {
        public List<Model.ContactModel> GetAllContacts(string connstring)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<ContactModel>>(connstring, "GetContactDetails", r => r.TranslateAsContactList());
        }

        public bool DeleteContact(string connString,int id)
        {
            SqlParameter[] param = { new SqlParameter("@Id", id) }; 
            return SqlHelper.ExecuteProcedureReturnString(connString, "DeleteContactById", param);
        }

        public bool AddContact(string connString, ContactModel model)
        {
            SqlParameter[] param = {
            new SqlParameter("@FName", model.FirstName),
            new SqlParameter("@LName", model.LastName),
            new SqlParameter("@Email", model.Email),
            new SqlParameter("@PhoneNumber", model.PhoneNumber),
            new SqlParameter("@Active", model.Active) };

            return SqlHelper.ExecuteProcedureReturnString(connString, "AddNewContactDetails", param);

        }

        public bool UpdateContact(string connString, ContactModel model)
        {
            SqlParameter[] param = {
            new SqlParameter("@Id", model.id),
            new SqlParameter("@FName", model.FirstName),
            new SqlParameter("@LName", model.LastName),
            new SqlParameter("@Email", model.Email),
            new SqlParameter("@PhoneNumber", model.PhoneNumber),
            new SqlParameter("@Active", model.Active) };

            return SqlHelper.ExecuteProcedureReturnString(connString, "UpdateContactDetails", param);

        }
    }
}
