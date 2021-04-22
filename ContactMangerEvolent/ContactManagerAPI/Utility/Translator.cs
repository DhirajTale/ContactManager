using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerAPI.Model;
using System.Data.SqlClient;

namespace ContactManagerAPI.Utility
{
    public static class Translator
    {
        public static ContactModel TranslateAsContact(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new ContactModel();
            if (reader.IsColumnExists("Id"))
                item.id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("FirstName"))
                item.FirstName = SqlHelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("LastName"))
                item.LastName = SqlHelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("Email"))
                item.Email = SqlHelper.GetNullableString(reader, "Email");

            if (reader.IsColumnExists("PhoneNumber"))
                item.PhoneNumber = SqlHelper.GetNullableString(reader, "PhoneNumber");

            if (reader.IsColumnExists("Active"))
                item.Active = SqlHelper.GetBoolean(reader, "    Active");

            return item;
        }

        public static List<ContactModel> TranslateAsContactList(this SqlDataReader reader)
        {
            var list = new List<ContactModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsContact(reader, true));
            }
            return list;
        }
    }
}
