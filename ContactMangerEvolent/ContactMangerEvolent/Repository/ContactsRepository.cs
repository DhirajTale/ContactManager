using ContactMangerEvolent.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
//using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json;

namespace ContactMangerEvolent.Repository
{
	public class ContactsRepository:IContactsRepository
	{
			private SqlConnection con;
		//To Handle connection related activities    
			private void connection()
			{
				string constr = ConfigurationManager.ConnectionStrings["dbConection"].ToString();
				con = new SqlConnection(constr);

			}
			//To Add contact details    
			public bool AddContact(ContactModel obj)
			{
				connection();
				SqlCommand com = new SqlCommand("AddNewContactDetails", con);
				com.CommandType = CommandType.StoredProcedure;
				com.Parameters.AddWithValue("@FName", obj.FirstName);
				com.Parameters.AddWithValue("@LName", obj.LastName);
				com.Parameters.AddWithValue("@Email", obj.Email);
				com.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);
				com.Parameters.AddWithValue("@Active", obj.Active);

				con.Open();
				int i = com.ExecuteNonQuery();
			
				con.Close();
				if (i >= 1)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			//To view Contact details with generic list     
			public List<ContactModel> GetAllContacts()
			{
				connection();
				List<ContactModel> ContactList = new List<ContactModel>();
				SqlCommand com = new SqlCommand("GetContactDetails", con);
				com.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter da = new SqlDataAdapter(com);
				DataTable dt = new DataTable();

				con.Open();
				da.Fill(dt);
				con.Close();
				//Bind Contact generic list using dataRow     
				foreach (DataRow dr in dt.Rows)
				{

				ContactList.Add(
						new ContactModel
						{
							id = Convert.ToInt32(dr["Id"]),
							FirstName = Convert.ToString(dr["FirstName"]),
							LastName = Convert.ToString(dr["LastName"]),
							Email = Convert.ToString(dr["Email"]),
							PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
							Active= Convert.ToBoolean(dr["Active"])
						}
						);
				}

			return ContactList;
			}
			//To Update Contact details    
			public bool UpdateContact(ContactModel obj)
			{

				connection();
				SqlCommand com = new SqlCommand("UpdateContactDetails", con);

				com.CommandType = CommandType.StoredProcedure;
				com.Parameters.AddWithValue("@Id", obj.id);
				com.Parameters.AddWithValue("@FName", obj.FirstName);
				com.Parameters.AddWithValue("@LName", obj.LastName);
				com.Parameters.AddWithValue("@Email", obj.Email);
				com.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);
				com.Parameters.AddWithValue("@Active", obj.Active);
				con.Open();
				int i = com.ExecuteNonQuery();
				con.Close();
				if (i >= 1)
				{

					return true;
				}
				else
				{
					return false;
				}
			}
			//To delete Contact details    
			public bool DeleteContact(int Id)
			{

				connection();
				SqlCommand com = new SqlCommand("DeleteContactById", con);

				com.CommandType = CommandType.StoredProcedure;
				com.Parameters.AddWithValue("@Id", Id);

				con.Open();
				int i = com.ExecuteNonQuery();
				con.Close();
				if (i >= 1)
				{
					return true;
				}
				else
				{

					return false;
				}
			}

		//New Code

		readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];

	
		public async Task<bool> Add(ContactModel contact)
        {
			bool response ;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(apiBaseAddress);
				HttpResponseMessage Res = await client.PostAsJsonAsync("AddContact", contact);
				var conactResponse = Res.Content.ReadAsStringAsync().Result;
				response = JsonConvert.DeserializeObject<bool>(conactResponse);
			}
			return response;
		}

        public async Task<bool> Update(ContactModel contact)
        {
			bool response;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(apiBaseAddress);
				HttpResponseMessage Res = await client.PostAsJsonAsync("UpdateContact", contact);
				var conactResponse = Res.Content.ReadAsStringAsync().Result;
				response = JsonConvert.DeserializeObject<bool>(conactResponse);
			}
			return response;
		}
		
		public async Task<bool> Delete(int id)
        {
			bool response;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(apiBaseAddress);
				HttpResponseMessage Res = await client.PostAsJsonAsync("Delete",id);
				var conactResponse = Res.Content.ReadAsStringAsync().Result;
				response = JsonConvert.DeserializeObject<bool>(conactResponse);
			}
			return response;
		}

        public Task<ContactModel> GetContact(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContactModel>> GetContacts()
        {
			List<ContactModel> contacts = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(apiBaseAddress);
				HttpResponseMessage Res = await client.GetAsync("GetContacts");
				var conactResponse = Res.Content.ReadAsStringAsync().Result;
				contacts = JsonConvert.DeserializeObject<List<ContactModel>>(conactResponse);
			}
			return contacts;
        }
    }
	
}