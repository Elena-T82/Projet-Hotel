using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hôtel_Obegua
{
    public class Parking
    {
        private Parking parking;
        private int NumeroPlace;
        private bool Occuper;
        private int NombrePlace;
        private List<Parking> ListeNombrePlaceLibre = new List<Parking>();

        private AccessBDD Data = new AccessBDD("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");
        private static MySqlConnection connect = new MySqlConnection("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");

        //Liste get
        public int GetNumeroPlace() { return NumeroPlace; }
        public bool GetOccuper() { return Occuper; }
        public int GetNombrePlace() { return NombrePlace; }

        //Liste Set
        public void SetNumeroPlace(int numPlace) { NumeroPlace = numPlace; }
        public void SetOccuper(bool occup) { Occuper = occup; }
        public void SetNombrePlace(int nbPlace) { NombrePlace = nbPlace; }

        public Parking(int nbPlace)
        {
            NombrePlace = nbPlace;
        }


        public Parking(){ }

        public int CreationListePlaceLibre()
        {
            connect.Open();
            MySqlCommand requete = new MySqlCommand("SELECT Count(NumeroPlace) AS NumPlace FROM placeparking WHERE Occuper = 0", connect);

            MySqlDataReader LireRequete = requete.ExecuteReader();

            while (LireRequete.Read())
            {
                int NbPlace = Convert.ToInt32(LireRequete["NumPlace"]);

                NombrePlace = NbPlace;
            }

            connect.Close();
            
            return NombrePlace;
        }
    }
}
