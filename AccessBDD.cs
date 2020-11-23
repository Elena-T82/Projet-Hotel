using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hôtel_Obegua
{
    public class AccessBDD
    {
        MySqlConnection Connect;
        MySqlCommand Commande;
        public AccessBDD(string connectionString)
        {
            Connect = new MySqlConnection(connectionString);
            Commande = new MySqlCommand("", Connect);
        }

        public void requete(string query)
        {
            //Changer la requête, elle devient celle qui est passée en paramètre
            Commande.CommandText = query;

            Connect.Open();
            Commande.ExecuteNonQuery();
            Connect.Close();
        }
    }
}
