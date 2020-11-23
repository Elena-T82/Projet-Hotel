using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hôtel_Obegua
{
    class Chambre
    {
        private Chambre chambre;
        private int NumeroChambre;
        private bool Occuper;
        private int PrixParNuit;
        private string NomTypeChambre;
        private string LibelleVueChambre;

        private List<Chambre> ListeTouteLesChambreDataGrid = new List<Chambre>();
        private List<Chambre> ListeChambreDisponible = new List<Chambre>();
        private List<Chambre> ListeCompleteChambreDisponible = new List<Chambre>();
        private List<Chambre> ListeCompleteToutesLesChambres = new List<Chambre>();

        private Dictionary<string, int> NombreChambreLibre = new Dictionary<string, int>();

        private AccessBDD Data = new AccessBDD("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");
        private static MySqlConnection connect = new MySqlConnection("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");


        //Toutes les chambres
        public Chambre(int NumeroChambre, bool Occuper, int PrixParNuit, string NomTypeChambre, string LibelleVueChambre)
        {
            this.NumeroChambre = NumeroChambre;
            this.Occuper = Occuper;
            this.PrixParNuit = PrixParNuit;
            this.NomTypeChambre = NomTypeChambre;
            this.LibelleVueChambre = LibelleVueChambre;
        }

        public Chambre() { }


        //Liste Get
        public int GetNumeroChambre() { return NumeroChambre; }
        public bool GetOccupper() { return Occuper; }
        public int GetPrixParNuit() { return PrixParNuit; }
        public string GetNomTypeChambre() { return NomTypeChambre; }
        public string GetLibelleVueChambre() { return LibelleVueChambre; }
        public List<Chambre> GetListeToutesLesChambres() { return ListeTouteLesChambreDataGrid; }
        public List<Chambre> GetListeChambreDisponible() { return ListeChambreDisponible; }
        public List<Chambre> GetListeCompleteChambreDisponible() { return ListeCompleteChambreDisponible; }
        public List<Chambre> GetListeCompleteToutesLesChambres() { return ListeCompleteToutesLesChambres; }
        public Dictionary<string, int> GetNombreChambreLibre() { return NombreChambreLibre; }

        //Liste Set
        public void SetNumeroChambre(int numeroChambre) { NumeroChambre = numeroChambre; }
        public void SetOccuper(bool occuper) { Occuper = occuper; }
        public void SetPrixParNuit(int prix) { PrixParNuit = prix; }
        public void SetNomTypeChambre(string nomTypeChambre) { NomTypeChambre = nomTypeChambre; }
        public void SetLibelleVueChambre(string libelleVueChambre) { LibelleVueChambre = libelleVueChambre; }


        //Liste des requêtes en rapport avec Chambre

        public List<Chambre> CreationListeChambreDisponible(int idTypeChambre)
        {
            connect.Open();

            GetListeChambreDisponible().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT NumeroChambre, Occuper, PrixParNuit, NomTypeChambre, NomVueChambre FROM Chambre " +
                "INNER JOIN typechambre ON (chambre.IdTypeChambre = typechambre.IdTypeChambre) " +
                "INNER JOIN vuechambre ON (chambre.IdVueChambre = vuechambre.IdVueChambre) WHERE Occuper = 0 AND chambre.IdTypeChambre =" + idTypeChambre, connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while(LireRequete.Read())
            {
                int numeroChambre = Convert.ToInt32(LireRequete["NumeroChambre"]);
                bool occuper = Convert.ToBoolean(LireRequete["Occuper"]);
                int prixParNuit = Convert.ToInt32(LireRequete["PrixParNuit"]);
                string nomTypeChambre = Convert.ToString(LireRequete["NomTypeChambre"]);
                string nomVueChambre = Convert.ToString(LireRequete["NomVueChambre"]);

                chambre = new Chambre(numeroChambre, occuper, prixParNuit, nomTypeChambre, nomVueChambre);

                GetListeChambreDisponible().Add(chambre);
            }

            connect.Close();
            return GetListeChambreDisponible();
        }


        public List<Chambre> CreationListeCompleteChambreDisponible()
        {
            connect.Open();

            GetListeCompleteChambreDisponible().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT NumeroChambre, Occuper, PrixParNuit, NomTypeChambre, NomVueChambre FROM Chambre " +
                "INNER JOIN typechambre ON (chambre.IdTypeChambre = typechambre.IdTypeChambre) " +
                "INNER JOIN vuechambre ON (chambre.IdVueChambre = vuechambre.IdVueChambre) WHERE Occuper = 0 ORDER BY chambre.IdTypeChambre, chambre.NumeroChambre", connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while (LireRequete.Read())
            {
                int numeroChambre = Convert.ToInt32(LireRequete["NumeroChambre"]);
                bool occuper = Convert.ToBoolean(LireRequete["Occuper"]);
                int prixParNuit = Convert.ToInt32(LireRequete["PrixParNuit"]);
                string nomTypeChambre = Convert.ToString(LireRequete["NomTypeChambre"]);
                string nomVueChambre = Convert.ToString(LireRequete["NomVueChambre"]);

                chambre = new Chambre(numeroChambre, occuper, prixParNuit, nomTypeChambre, nomVueChambre);

                GetListeCompleteChambreDisponible().Add(chambre);
            }

            connect.Close();
            return GetListeCompleteChambreDisponible();
        }


        public List<Chambre> CreationListeCompleteToutesLesChambres()
        {
            connect.Open();

            GetListeCompleteToutesLesChambres().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT NumeroChambre, Occuper, PrixParNuit, NomTypeChambre, NomVueChambre FROM Chambre " +
                "INNER JOIN typechambre ON (chambre.IdTypeChambre = typechambre.IdTypeChambre) " +
                "INNER JOIN vuechambre ON (chambre.IdVueChambre = vuechambre.IdVueChambre) ORDER BY chambre.IdTypeChambre, chambre.NumeroChambre, chambre.Occuper", connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while (LireRequete.Read())
            {
                int numeroChambre = Convert.ToInt32(LireRequete["NumeroChambre"]);
                bool occuper = Convert.ToBoolean(LireRequete["Occuper"]);
                int prixParNuit = Convert.ToInt32(LireRequete["PrixParNuit"]);
                string nomTypeChambre = Convert.ToString(LireRequete["NomTypeChambre"]);
                string nomVueChambre = Convert.ToString(LireRequete["NomVueChambre"]);

                chambre = new Chambre(numeroChambre, occuper, prixParNuit, nomTypeChambre, nomVueChambre);

                GetListeCompleteToutesLesChambres().Add(chambre);
            }

            connect.Close();
            return GetListeCompleteToutesLesChambres();
        }

        public Dictionary<string, int> NombreChambreDispo()
        {
            connect.Open();

            GetNombreChambreLibre().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT Count(*) AS NbChambre, NomTypeChambre FROM chambre INNER JOIN typechambre ON (chambre.IdTypeChambre = typechambre.IdTypeChambre) WHERE Occuper = 0 GROUP BY chambre.IdTypeChambre", connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while (LireRequete.Read())
            {
                string nomTypeChambre = Convert.ToString(LireRequete["NomTypeChambre"]);
                int chambreLibre = Convert.ToInt32(LireRequete["NbChambre"]);

                GetNombreChambreLibre().Add(nomTypeChambre, chambreLibre);
            }

            connect.Close();

            return GetNombreChambreLibre();
        }
    }
}
