using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using WPFModernVerticalMenu.Model;

namespace WPFModernVerticalMenu.Services
{
    internal class EncadreurService
    {
        public List<Encadreur> servGetListEncadreur()
        {
            HttpClient client = new HttpClient();
            var services = new List<Encadreur>();
            var reponse = client.GetAsync("http://localhost/backend_Shared_memory/list.php").Result;
            if (reponse.IsSuccessStatusCode)
            {
                var responseData = reponse.Content.ReadAsStringAsync().Result;
                services = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Encadreur>>(responseData);

            }
            return services;
        }
        public bool AddEncadreur(Encadreur encadreur)
        {
            bool req = false;
            string IdE = encadreur.id > 0 ? encadreur.id.ToString() : "0";
            var values = new Dictionary<string, string>
            {
                { "id",IdE},
                { "nom",encadreur.nom},
                { "prenom",encadreur.prenom},
                { "specialite",encadreur.specialite},

            };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync("http://localhost/backend_Shared_memory/create.php", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return req;
        }

        public bool DeleteEncadreur(int id)
        {

            using (var client = new HttpClient())
            {
                var reponse = client.DeleteAsync($"http://localhost/backend_Shared_memory/delete.php/{id}").Result;
                if (reponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("reussit");
                    return true;
                }
                else
                {
                    MessageBox.Show("error");
                    return false;
                }
            }


        }
        public bool UpdateEncadreur(Encadreur encadreur)
        {
            bool req = false;
            string IdE = encadreur.id > 0 ? encadreur.id.ToString() : "0";
            var values = new Dictionary<string, string>
            {
                { "id",IdE},
                { "nom",encadreur.nom},
                { "prenom",encadreur.prenom},
                { "specialite",encadreur.specialite},

            };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PutAsync("http://localhost/backend_Shared_memory/update.php/{encadreur.id}", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return req;
        }
    }
}

