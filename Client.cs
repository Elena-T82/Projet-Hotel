using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hôtel_Obegua
{
    public class Client
    {

        private Client unClient;
        private int IdClient;
        private string NomClient;
        private string PrenomClient;
        private DateTime DateNaissanceClient;
        private string Pays;
        private string Ville;
        private string Mail;
        private string Tel;
        private List<Client> ListeClientDataGrid = new List<Client>();

        private AccessBDD Data = new AccessBDD("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");
        private static MySqlConnection connect = new MySqlConnection("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");

        
        //Liste des Gets
        public int GetIdClient() { return IdClient; }
        public string GetNomClient() { return NomClient; }
        public string GetPrenomClient() { return PrenomClient; }
        public DateTime GetDateNaissanceClient() { return DateNaissanceClient; }
        public string GetPays() { return Pays; }
        public string GetVille() { return Ville; }
        public string GetMail() { return Mail; }
        public string GetTel() { return Tel; }
        public List<Client> GetListeClientDataGrid() { return ListeClientDataGrid; }


        //Liste des Sets
        public void SetNomClient(string nomClient) { NomClient = nomClient; }
        public void SetPrenomClient(string prenomClient) { PrenomClient = prenomClient; }
        public void SetDateNaissance(DateTime ddn) { DateNaissanceClient = ddn; }
        public void SetPays(string pays) { Pays = pays; }
        public void SetVille(string ville) { Ville = ville; }
        public void SetMail(string mail) { Mail = mail; }
        public void SetTel(string tel) { Tel = tel; }


        public Client(int IdClient, string NomClient, string PrenomClient, DateTime DateNaissanceClient, string Pays, string Ville, string Mail, string Tel)
        {
            this.IdClient = IdClient;
            this.NomClient = NomClient;
            this.PrenomClient = PrenomClient;
            this.DateNaissanceClient = DateNaissanceClient;
            this.Pays = Pays;
            this.Ville = Ville;
            this.Mail = Mail;
            this.Tel = Tel;            

        }

        public Client()
        {

        }


        //Liste des requêtes en rapport avec le client

        public void AjouterClient()
        {    
            Data.requete("INSERT INTO client(NomClient, PrenomClient, DateNaissanceClient, PaysClient, VilleClient, MailClient, TelClient) VALUES('" + NomClient + "', '" + PrenomClient + "', '"+ DateNaissanceClient.ToString("yyyy-MM-dd") + "', '"+ Pays + "', '" + Ville + "', '" + Mail + "', '" + Tel + "' );");
        }

        public void ModifierClient(int idClient)
        {
            Data.requete("UPDATE client SET NomClient = '" + NomClient + "', PrenomClient = '" + PrenomClient + "', DateNaissanceClient = '" + DateNaissanceClient.ToString("yyyy-MM-dd") + "', PaysClient = '" + Pays + "', VilleClient = '" + Ville + "', MailClient = '" + Mail + "', TelClient = '" + Tel + "' WHERE IDClient = '" + idClient + "'");
        }

        public void SupprimerClient(int idClient)
        {
            Data.requete("DELETE FROM Client WHERE IDClient = '" + idClient + "'");
        }
  

        public List<Client> ListeClientData()
        {
            connect.Open();

            GetListeClientDataGrid().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT * FROM Client ORDER BY NomClient", connect);

            MySqlDataReader lectureRequete = requete.ExecuteReader();

            while (lectureRequete.Read())
            {
                int IdClient = Convert.ToInt32(lectureRequete["IdClient"]);
                string NomClient = lectureRequete["NomClient"].ToString();
                string PrenomClient = lectureRequete["PrenomClient"].ToString();
                DateTime DateNaissance = Convert.ToDateTime(lectureRequete["DateNaissanceClient"]);
                string PaysClient = lectureRequete["PaysClient"].ToString();
                string VilleClient = lectureRequete["VilleClient"].ToString();
                string MailClient = lectureRequete["MailClient"].ToString();
                string TelClient = Convert.ToString(lectureRequete["TelClient"]);

                unClient = new Client(IdClient, NomClient, PrenomClient, DateNaissance, PaysClient, VilleClient, MailClient, TelClient);

                GetListeClientDataGrid().Add(unClient);
            }

            connect.Close();

            return GetListeClientDataGrid();
        }
    }
}