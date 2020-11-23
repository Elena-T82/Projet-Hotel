using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hôtel_Obegua
{
    class Reservation
    {
        private Reservation reservation;
        private int IdReservation;
        private DateTime DateDebut;
        private DateTime DateFin;
        private int Prix;
        private int IdClient;
        private string NomClient;
        private string PrenomClient;
        private string NomTypeChambre;
        private int NumChambre;
        private int NumeroPlaceParking;
        private int PlaceParking;
        private string VueChambre;

        private int ChambreAUpdate;
        private int PlaceParkingAUpdate;
        private int NumeroPlaceParkingAUpdate;
        private int ParkingActuel;
        private int ParkingActuelARetourner;

        private List<Reservation> ListeReservationParClient = new List<Reservation>();
        private List<Reservation> ListeReservationDataGrid = new List<Reservation>();
        private Parking NumPlace = new Parking();

        private AccessBDD Data = new AccessBDD("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");
        private static MySqlConnection connect = new MySqlConnection("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");

        //Liste Get
        public int GetIdReservation(){return IdReservation;}
        public DateTime GetDateDebut() { return DateDebut; }
        public DateTime GetDateFin() { return DateFin; }
        public int GetPrix() { return Prix; }
        public string GetNomClient() { return NomClient; }
        public string GetPrenomClient() { return PrenomClient; }
        public string GetNomTypeChambre() { return NomTypeChambre; }
        public int GetNumChambre() { return NumChambre; }
        public int GetNumeroPlaceParking() { return NumeroPlaceParking; }
        public int GetPlaceParking() { return PlaceParking; }
        public string GetVueChambre() { return VueChambre; }
        public int GetChambreAUpdate() { return ChambreAUpdate; }
        public int GetPlaceParkingAUpdate() { return PlaceParkingAUpdate; }
        public int GetNumeroPlaceParkingAUpdate() { return NumeroPlaceParkingAUpdate; }
        public List<Reservation> GetListeReservationParClient() { return ListeReservationParClient; }
        public List<Reservation> GetListeReservationDataGrid() { return ListeReservationDataGrid; }

        //Liste Set
        public void SetIdReservation(int idReservation){IdReservation = idReservation;}
        public void SetDateDebut(DateTime dateDebut) { DateDebut = dateDebut; }
        public void SetDateFin(DateTime dateFin) { DateFin = dateFin; }
        public void SetPrix(int prix) { Prix = prix; }
        public void SetNomClient(string nomClient) { NomClient = nomClient; }
        public void SetNomTypeReservation(string nomTypeChambre) { NomTypeChambre = nomTypeChambre; }
        public void SetIdClient(int idClient) { IdClient = idClient; }
        public void SetNumChambre(int numChambre) { NumChambre = numChambre; }
        public void SetNumeroPlaceParking(int numPlaceParking) { NumeroPlaceParking = numPlaceParking; }
        public void SetPlaceParking(int placeParking) { PlaceParking = placeParking; }


        public Reservation(int idReservation, DateTime dateDebut, DateTime dateFin, int prix, int idClient, int numeroChambre, int placeParking)
        {
            IdReservation = idReservation;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Prix = prix;
            IdClient = idClient;
            NumChambre = numeroChambre;
            PlaceParking = placeParking;
        }

        public Reservation(int idReservation, DateTime dateDebut, DateTime dateFin, int prix, int idClient, string nomClient, string prenomClient, string nomTypeChambre, int numeroChambre, int placeParking, int numeroPlaceParking, string vueChambre)
        {
            IdReservation = idReservation;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Prix = prix;
            IdClient = idClient;
            NomClient = nomClient;
            PrenomClient = prenomClient;
            NomTypeChambre = nomTypeChambre;
            NumChambre = numeroChambre;
            PlaceParking = placeParking;
            NumeroPlaceParking = numeroPlaceParking;
            VueChambre = vueChambre;
        }

        public Reservation() { }

        public void AjouterReservation()
        {
            connect.Open();
          
            if (PlaceParking == 1)
            {
                MySqlCommand requete = new MySqlCommand("SELECT MIN(NumeroPlace) AS num FROM placeparking WHERE Occuper= 0", connect);
                MySqlDataReader LireResultat = requete.ExecuteReader();

                while (LireResultat.Read())
                {
                    NumeroPlaceParking = Convert.ToInt32(LireResultat["num"]);
                }


                Data.requete("INSERT INTO reservation(DateDebut, DateFin, PrixParNuit, IdClient, NumeroChambre, PlaceParking, NumeroPlaceParking) " +
                    "VALUES('" + DateDebut.ToString("yyyy-MM-dd") + "', '" + DateFin.ToString("yyyy-MM-dd") + "', '" + Prix + "', '" + IdClient + "', '" + NumChambre + "', '" + PlaceParking + "', '" + NumeroPlaceParking + "' );");


                Data.requete("UPDATE placeparking SET Occuper = 1 WHERE NumeroPlace =" + NumeroPlaceParking);
            }
            else
            {
                Data.requete("INSERT INTO reservation(DateDebut, DateFin, PrixParNuit, IdClient, NumeroChambre, PlaceParking) " +
                   "VALUES('" + DateDebut.ToString("yyyy-MM-dd") + "', '" + DateFin.ToString("yyyy-MM-dd") + "', '" + Prix + "', '" + IdClient + "', '" + NumChambre + "', '" + PlaceParking + "');");

            }

            Data.requete("UPDATE chambre SET Occuper = 1 WHERE NumeroChambre =" + NumChambre);

            connect.Close();
        }

        public void ModifierReservation()
        {
            UpdateChambreEtParkingANull(IdReservation, PlaceParking);

            connect.Open();

            if (PlaceParking == 1 && ParkingActuel == 0) //S'il n'a pas de place et qu'il en veut une
            {
                MySqlCommand requete = new MySqlCommand("SELECT MIN(NumeroPlace) AS num FROM placeparking WHERE Occuper= 0", connect);
                MySqlDataReader LireResultat = requete.ExecuteReader();

                while (LireResultat.Read())
                {
                    NumeroPlaceParking = Convert.ToInt32(LireResultat["num"]);
                }

                Data.requete("UPDATE reservation SET DateDebut ='" + DateDebut.ToString("yyyy-MM-dd") + "', DateFin='" + DateFin.ToString("yyyy-MM-dd") + "'," +
                    "PrixParNuit='" + Prix + "', NumeroChambre ='" + NumChambre + "', PlaceParking='" + PlaceParking + "', NumeroPlaceParking='" + NumeroPlaceParking + "' WHERE IdReservation =" + IdReservation);

                Data.requete("UPDATE placeparking SET Occuper = 1 WHERE NumeroPlace =" + NumeroPlaceParking);
            }

            else if((PlaceParking == 1 && ParkingActuel == 1) || (PlaceParking == 0 && ParkingActuel == 0))//S'il veut une place alors qu'il en a déjà une OU
                                                                                                           //S'il en avait pas et il en veut toujours pas->donc on change rien
            {
                Data.requete("UPDATE reservation SET DateDebut ='" + DateDebut.ToString("yyyy-MM-dd") + "', DateFin='" + DateFin.ToString("yyyy-MM-dd") + "'," +
                   "PrixParNuit='" + Prix + "', NumeroChambre ='" + NumChambre + "' WHERE IdReservation=" + IdReservation);
            }

            else if(ParkingActuel == 1 && PlaceParking == 0) //S'il en avait une, mais qu'il en veut plus
            {
                Data.requete("UPDATE reservation SET DateDebut ='" + DateDebut.ToString("yyyy-MM-dd") + "', DateFin='" + DateFin.ToString("yyyy-MM-dd") + "'," +
                   "PrixParNuit='" + Prix + "', NumeroChambre ='" + NumChambre + "', PlaceParking='" + PlaceParking + "' WHERE IdReservation =" + IdReservation);

                Data.requete("UPDATE reservation SET NumeroPlaceParking = NULL WHERE IdReservation=" + IdReservation);

            }

            Data.requete("UPDATE chambre SET Occuper = 1 WHERE NumeroChambre =" + NumChambre);

            connect.Close();
        }

        public void SupprimerReservation(int idReservation)
        {
            UpdateChambreEtParkingANull(idReservation);
            Data.requete("DELETE FROM Reservation WHERE IdReservation = '" + idReservation + "'");     
        }

        public void UpdateChambreEtParkingANull(int idReservation, int placeParking=0)
        {
            connect.Open();

            //Pour update la chambre
            MySqlCommand requete = new MySqlCommand("SELECT NumeroChambre FROM reservation WHERE IdReservation=" + idReservation, connect);
            MySqlDataReader LireResultat = requete.ExecuteReader();

            while (LireResultat.Read())
            {
                ChambreAUpdate = Convert.ToInt32(LireResultat["NumeroChambre"]);
            }

            Data.requete("UPDATE chambre SET Occuper = 0 WHERE NumeroChambre=" + ChambreAUpdate);

            connect.Close();
            connect.Open();


            //Pour update le parking
            MySqlCommand requete2 = new MySqlCommand("SELECT PlaceParking FROM reservation WHERE IdReservation=" + idReservation, connect);
            MySqlDataReader LireResultat2 = requete2.ExecuteReader();

            while(LireResultat2.Read())
            {
                PlaceParkingAUpdate = Convert.ToInt32(LireResultat2["PlaceParking"]);                             
            }

            ParkingActuel= PlaceParkingAUpdate;

            connect.Close();
            connect.Open();

            if(PlaceParkingAUpdate == 1 && placeParking == 0)
            {
                MySqlCommand requete3 = new MySqlCommand("SELECT NumeroPlaceParking FROM Reservation WHERE IdReservation=" + idReservation, connect);
                MySqlDataReader LireResultat3 = requete3.ExecuteReader();

                while(LireResultat3.Read())
                {
                    NumeroPlaceParkingAUpdate = Convert.ToInt32(LireResultat3["NumeroPlaceParking"]);
                }

                Data.requete("UPDATE placeparking SET Occuper = 0 WHERE NumeroPlace =" + NumeroPlaceParkingAUpdate);
            }

            connect.Close();

        }

        public List<Reservation> CreationListeReservationParClient(int idClient) //pour les listbox formulaire
        {
            connect.Open();

            GetListeReservationParClient().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT * FROM Reservation WHERE IdClient =" + idClient, connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while (LireRequete.Read())
            {
                int idReservation = Convert.ToInt32(LireRequete["IdReservation"]);
                DateTime DateDebut = Convert.ToDateTime(LireRequete["DateDebut"]);
                DateTime DateFin = Convert.ToDateTime(LireRequete["DateFin"]);
                int PrixParNuit = Convert.ToInt32(LireRequete["PrixParNuit"]);
                int IdClient = Convert.ToInt32(LireRequete["IdClient"]);
                int NumeroChambre = Convert.ToInt32(LireRequete["NumeroChambre"]);
                int PlaceParking = Convert.ToInt32(LireRequete["PlaceParking"]);

                reservation = new Reservation(idReservation, DateDebut, DateFin, PrixParNuit, IdClient, NumeroChambre, PlaceParking);

                GetListeReservationParClient().Add(reservation);
            }

            connect.Close();

            return GetListeReservationParClient();
        }

        public List<Reservation> CreationListeReservation() //Pour datagrid reservation
        {
            connect.Open();

            GetListeReservationDataGrid().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT reservation.*, vuechambre.NomVueChambre, typeChambre.NomTypeChambre, Client.NomClient, Client.PrenomClient FROM Reservation " +
                "INNER JOIN Client ON(reservation.IdClient = Client.IdClient)" +
                "INNER JOIN Chambre on(reservation.NumeroChambre = chambre.NumeroChambre)" +
                "INNER JOIN TypeChambre ON(chambre.IdTypeChambre = typeChambre.IdTypeChambre)" +
                "INNER JOIN vuechambre ON(chambre.IdVueChambre = vuechambre.IdVueChambre)", connect);

            MySqlDataReader LireRequete = requete.ExecuteReader();

            while (LireRequete.Read())
            {
                int idReservation = Convert.ToInt32(LireRequete["IdReservation"]);
                DateTime DateDebut = Convert.ToDateTime(LireRequete["DateDebut"]);
                DateTime DateFin = Convert.ToDateTime(LireRequete["DateFin"]);
                int PrixParNuit = Convert.ToInt32(LireRequete["PrixParNuit"]);
                int IdClient = Convert.ToInt32(LireRequete["IdClient"]);
                string NomClient = Convert.ToString(LireRequete["NomClient"]);
                string PrenomClient = Convert.ToString(LireRequete["PrenomClient"]);
                string NomTypeChambre = Convert.ToString(LireRequete["NomTypeChambre"]);
                int NumeroChambre = Convert.ToInt32(LireRequete["NumeroChambre"]);
                int PlaceParking = Convert.ToInt32(LireRequete["PlaceParking"]);

                int NumeroPlaceParking = 0;

                if (PlaceParking == 1)
                {
                    NumeroPlaceParking = Convert.ToInt32(LireRequete["NumeroPlaceParking"]);
                }
                string VueChambre = Convert.ToString(LireRequete["NomVueChambre"]);

                reservation = new Reservation(idReservation, DateDebut, DateFin, PrixParNuit, IdClient, NomClient, PrenomClient, NomTypeChambre, NumeroChambre, PlaceParking, NumeroPlaceParking, VueChambre);

                GetListeReservationDataGrid().Add(reservation);
            }

            connect.Close();

            return GetListeReservationDataGrid();
        }

        public int RetourneParkingActuel(int idReservation)
        {
            connect.Open();

            MySqlCommand requete = new MySqlCommand("SELECT PlaceParking FROM reservation WHERE IdReservation=" + idReservation, connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while(LireRequete.Read())
            {
                ParkingActuelARetourner = Convert.ToInt32(LireRequete["PlaceParking"]);
            }

            connect.Close();

            return ParkingActuelARetourner;
        }

        public int PrixDeLaNuit(int numeroChambre)
        {
            connect.Open();

            MySqlCommand requete = new MySqlCommand("SELECT PrixParNuit FROM chambre WHERE NumeroCHambre=" + numeroChambre, connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while (LireRequete.Read())
            {
                Prix = Convert.ToInt32(LireRequete["PrixParNuit"]);
            }

            connect.Close();

            return GetPrix();
        }

    }
}
