using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hôtel_Obegua
{
    public partial class Accueil : Form
    {
        private Client client = new Client();
        private Reservation reservation = new Reservation();
        private Chambre chambre = new Chambre();
        private TypeChambre typeChambre = new TypeChambre();
        private Parking parking = new Parking();
        
        public int idDuClientSelection = 0;
        public int idTypeChambreSelection = 0;
        public int idChambreSelection = 0;
        public int idReservSelection = 0;

        public string Place;
        public string Statut;

        List<int> listeIDClient = new List<int>();
        List<int> ListeIdTypeChambreChoisi = new List<int>();
        List<int> ListeIdChambreChoisi = new List<int>();
        List<int> ListeIdReservationChoisi = new List<int>();

        public Accueil()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------Client-------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------

        private void Accueil_Load(object sender, EventArgs e)
        {
            ActualisationData();
        }
        private void ActualisationData()
        {
            //Actualiser données partie client
            ActualiserListeDataGridClient();
            ActualiserListeClient();
            EffacerTextBoxClient();

            idDuClientSelection = 0;

            //Actualiser données partie reserv
            ActualiserListeDataGridReserv();
            ActualiserListeTypeChambre();
            ActualiserListeChambreAjout();
            ActualiserListeChambreModif();
            ActualiserListeReservationModif();
            ActualiserListeReservationSuppr();
            EffacerTextBoxReservation();

            idTypeChambreSelection = 0;
            idChambreSelection = 0;
            idReservSelection = 0;

            //Actualiser données partie chambre
            EffacerTextBoxChambre();
            ActualiserNombreChambre();

        }

        private void ActualiserListeDataGridClient()
        {
            dataGridView1.Rows.Clear();

            foreach (Client clients in client.ListeClientData())
            {
                string NomClient = clients.GetNomClient();
                string PrenomClient = clients.GetPrenomClient();
                string DateNaissance = clients.GetDateNaissanceClient().ToString("dd/MM/yyyy");
                string PaysClient = clients.GetPays();
                string VilleClient = clients.GetVille();
                string Mail = clients.GetMail();
                string Tel = clients.GetTel();

                dataGridView1.Rows.Add(NomClient, PrenomClient, DateNaissance, PaysClient, VilleClient, Mail, Tel);
            }
        }

        private void ActualiserListeClient()
        {
            ListeClientModif.Items.Clear();
            ListeClientSuppr.Items.Clear();
            ListeClientReserv.Items.Clear();
            ListeClientReservModif.Items.Clear();
            ListeClientReservSuppr.Items.Clear();

            listeIDClient.Clear();

             foreach (Client client in client.ListeClientData())
             {
                 ListeClientModif.Items.Add(client.GetNomClient() + " " + client.GetPrenomClient());
                 ListeClientSuppr.Items.Add(client.GetNomClient() + " " + client.GetPrenomClient());
                 ListeClientReserv.Items.Add(client.GetNomClient() + " " + client.GetPrenomClient());
                 ListeClientReservModif.Items.Add(client.GetNomClient() + " " + client.GetPrenomClient());
                 ListeClientReservSuppr.Items.Add(client.GetNomClient() + " " + client.GetPrenomClient());

                 listeIDClient.Add(client.GetIdClient());
             }
        }


        private void EffacerTextBoxClient()
        {
            TextBoxNom.Clear();
            TextBoxPrenom.Clear();
            dateTimeDateNaissance.ResetText();
            TextBoxPays.Clear();
            TextBoxVille.Clear();
            TextBoxMail.Clear();
            TextBoxTel.Clear();
            TextBoxNouveauNom.Clear();
            TextBoxNouveauPrenom.Clear();
            dateTimeNouvelleDateNaissance.ResetText();
            TextBoxNouveauPays.Clear();
            TextBoxNouvelleVille.Clear();
            TextBoxNouveauMail.Clear();
            TextBoxNouveauTel.Clear();
            ListeClientModif.Text = "";
            ListeClientSuppr.Text = "";
        }

        //On récupère l'id du client séléctionné pour le modifier
        private void ListeClientModif_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex = ListeClientModif.SelectedIndex;
            idDuClientSelection = listeIDClient.ElementAt(SelectedIndex);
        }

        //On récupère l'id du client selectionné pour le supprimer
        private void ListeClientSuppr_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex = ListeClientSuppr.SelectedIndex;
            idDuClientSelection = listeIDClient.ElementAt(SelectedIndex);
        }

        private void BoutonAjouterClient_Click(object sender, EventArgs e)
        {
            if (TextBoxNom.Text != "" && TextBoxPrenom.Text != "" && dateTimeDateNaissance.Text != "" && TextBoxPays.Text != "" && TextBoxVille.Text != "" && TextBoxMail.Text != "" && TextBoxTel.Text != "")
            {
                int IdClient = 0;
                string NomClient = TextBoxNom.Text;
                string PrenomClient = TextBoxPrenom.Text;
                DateTime DateNaissance = Convert.ToDateTime(dateTimeDateNaissance.Text);
                string Pays = TextBoxPays.Text;
                string Ville = TextBoxVille.Text;
                string Mail = TextBoxMail.Text;
                string Tel = Convert.ToString(TextBoxTel.Text);

                client = new Client(IdClient, NomClient, PrenomClient, DateNaissance, Pays, Ville, Mail, Tel);
                client.AjouterClient();
                             
                MessageBox.Show("Le client a bien été ajouté.");

                ActualisationData();
            }
            else
            {
                MessageBox.Show("Vous devez remplir tous les champs pour ajouter un client !");
            }

        }

        private void BoutonModifierClient_Click(object sender, EventArgs e)
        {
            if (ListeClientModif.Text != "" && TextBoxNouveauNom.Text != "" && TextBoxNouveauPrenom.Text != "" && dateTimeNouvelleDateNaissance.Text != "" && TextBoxNouveauPays.Text != "" && TextBoxNouvelleVille.Text != "" && TextBoxNouveauMail.Text != "" && TextBoxNouveauTel.Text != "")
            {
                int IdClient = idDuClientSelection;
                string NomClient = TextBoxNouveauNom.Text;
                string PrenomClient = TextBoxNouveauPrenom.Text;
                DateTime DateNaissance = Convert.ToDateTime(dateTimeNouvelleDateNaissance.Text);
                string Pays = TextBoxNouveauPays.Text;
                string Ville = TextBoxNouvelleVille.Text;
                string Mail = TextBoxNouveauMail.Text;
                string Tel = Convert.ToString(TextBoxNouveauTel.Text);

                client = new Client(IdClient, NomClient, PrenomClient, DateNaissance, Pays, Ville, Mail, Tel);
                client.ModifierClient(IdClient);

                MessageBox.Show("Les modifications ont bien été effectué.");

                ActualisationData();
            }
            else {
                MessageBox.Show("Vous devez remplir tous les champs pour modifier un client !");
            }

        }

        private void BoutonSupprimerClient_Click(object sender, EventArgs e)
        {
            if (ListeClientSuppr.Text != "")
            {
                int IdClient = idDuClientSelection;

                client.SupprimerClient(IdClient);
                MessageBox.Show("Le client a bien été supprimé.");

                ActualisationData();
            }
            else {
                MessageBox.Show("Vous devez choisir un client à supprimer !");
            }

        }
















        //------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------Reservation--------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------
        private void ActualiserListeDataGridReserv()
        {
            dataGridView2.Rows.Clear();

           // Console.WriteLine(reservation.CreationListeReservation());
            foreach (Reservation reservation in reservation.CreationListeReservation())
            {
                string NomClient = reservation.GetNomClient();
                string PrenomClient = reservation.GetPrenomClient();
                string DateDebut = reservation.GetDateDebut().ToString("dd/MM/yyyy");
                string DateFin = reservation.GetDateFin().ToString("dd/MM/yyyy");
                int Prix = reservation.GetPrix();
                string TypeChambre = reservation.GetNomTypeChambre();
                int NumeroChambre = reservation.GetNumChambre();
                string VueChambre = reservation.GetVueChambre();
                int PlaceParking = reservation.GetPlaceParking();
                int NumeroPlaceParking = reservation.GetNumeroPlaceParking();

                if(PlaceParking == 1)
                {
                    Place = "Oui : " + NumeroPlaceParking;
                }
                else
                {
                    Place = "Non";
                }

                dataGridView2.Rows.Add(NomClient + " " + PrenomClient, DateDebut, DateFin, Prix + " €", TypeChambre, NumeroChambre + " / Vue : " + VueChambre, Place);
            }
        }

        private void ActualiserListeTypeChambre()
        {
            ListeTypeChambre.Items.Clear();
            ListeTypeChambreModif.Items.Clear();
            ListeIdTypeChambreChoisi.Clear();

            foreach (TypeChambre typeChambre in typeChambre.ListeDeroulanteTypeChambre())
            {
                ListeTypeChambre.Items.Add(typeChambre.GetNomTypeChambre());
                ListeTypeChambreModif.Items.Add(typeChambre.GetNomTypeChambre());

                ListeIdTypeChambreChoisi.Add(typeChambre.GetIdTypeChambre());
            }
        }

        private void ActualiserListeChambreAjout()
        {
            ListeNumeroChambre.Items.Clear();
            ListeIdChambreChoisi.Clear();

            foreach(Chambre chambre in chambre.CreationListeChambreDisponible(idTypeChambreSelection))
            {
                ListeNumeroChambre.Items.Add(chambre.GetNumeroChambre() + " / vue : " + chambre.GetLibelleVueChambre());

                ListeIdChambreChoisi.Add(chambre.GetNumeroChambre());
            }
        }

        private void ActualiserListeChambreModif()
        {
            ListeNumeroChambreModif.Items.Clear();
            ListeIdChambreChoisi.Clear();

            foreach (Chambre chambre in chambre.CreationListeChambreDisponible(idTypeChambreSelection))
            {
                ListeNumeroChambreModif.Items.Add(chambre.GetNumeroChambre() + " / vue : " + chambre.GetLibelleVueChambre());

                ListeIdChambreChoisi.Add(chambre.GetNumeroChambre());
            }
        }

        private void ActualiserListeReservationModif()
        {
            ListeReservClientModif.Items.Clear();
            ListeIdReservationChoisi.Clear();

            foreach(Reservation reserv in reservation.CreationListeReservationParClient(idDuClientSelection))
            {
                ListeReservClientModif.Items.Add(reserv.GetIdReservation());

                ListeIdReservationChoisi.Add(reserv.GetIdReservation());
            }
        }

        private void ActualiserListeReservationSuppr()
        {
            ListeReservClientSuppr.Items.Clear();
            ListeIdReservationChoisi.Clear();

            foreach (Reservation reserv in reservation.CreationListeReservationParClient(idDuClientSelection))
            {
                ListeReservClientSuppr.Items.Add(reserv.GetIdReservation());

                ListeIdReservationChoisi.Add(reserv.GetIdReservation());
            }
        }

        private void EffacerTextBoxReservation()
        {
            ListeClientReserv.Text = "";
            ListeTypeChambre.Text = "";
            ListeNumeroChambre.Text = "";
            dateTimeDebut.ResetText();
            dateTimeFin.ResetText();
            BoutonRadioParkingOui.Checked = false;
            BoutonRadioParkingNon.Checked = false;

            ListeClientReservModif.Text = "";
            ListeReservClientModif.Text = "";
            ListeTypeChambreModif.Text = "";
            ListeNumeroChambreModif.Text = "";
            dateTimeDebutModif.ResetText();
            dateTimeFinModif.ResetText();
            BoutonRadioParkingOuiModiff.Checked = false;
            BoutonRadioParkingNonModiff.Checked = false;

            ListeClientReservSuppr.Text = "";
            ListeReservClientSuppr.Text = "";

        }

        //On récupère l'id du client pour lui faire une réservation
        private void ListeClientReserv_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex = ListeClientReserv.SelectedIndex;
            idDuClientSelection = listeIDClient.ElementAt(SelectedIndex);
        }

        //on récupère l'id du typechambre choisit pour faire la reserv
        private void ListeTypeChambre_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex = ListeTypeChambre.SelectedIndex;
            idTypeChambreSelection = ListeIdTypeChambreChoisi.ElementAt(SelectedIndex);
            ActualiserListeChambreAjout();
        }

        //on récupère l'id du typechambre choisit pour modifier la reserv
        private void ListeTypeChambreModif_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex = ListeTypeChambreModif.SelectedIndex;
            idTypeChambreSelection = ListeIdTypeChambreChoisi.ElementAt(SelectedIndex);
            ActualiserListeChambreModif();
        }

        //On recupère l'id du numeroChambre choisit pour faire la reserv
        private void ListeNumeroChambre_SelectedIndexChanger(object sender, EventArgs e)
        {
            int SelectedIndex = ListeNumeroChambre.SelectedIndex;
            idChambreSelection = ListeIdChambreChoisi.ElementAt(SelectedIndex);
        }

        //On recupère l'id du numeroChambre choisit pour modifier la reserv
        private void ListeNumeroChambreModif_SelectedIndexChanger(object sender, EventArgs e)
        {
            int SelectedIndex = ListeNumeroChambreModif.SelectedIndex;
            idChambreSelection = ListeIdChambreChoisi.ElementAt(SelectedIndex);
        }

        //On recupère l'id du client choisi pour faire afficher ses reservations(modiff).
        private void ListeClientReservModif_SelectedIndexChanger(object sender, EventArgs e)
        {
            int SelectedIndex = ListeClientReservModif.SelectedIndex;
            idDuClientSelection = listeIDClient.ElementAt(SelectedIndex);
            ActualiserListeReservationModif(); //On affiche les reservs de l'id client selectionné
        }

        //On recupère l'id du client choisi pour faire afficher ses reservations(suppr).
        private void ListeClientReservSuppr_SelectedIndexChanger(object sender, EventArgs e)
        {
            int SelectedIndex = ListeClientReservSuppr.SelectedIndex;
            idDuClientSelection = listeIDClient.ElementAt(SelectedIndex);
            ActualiserListeReservationSuppr(); //On affiche les reservs de l'id client selectionné
        }

        //On récupère l'id de la reserv à modifier
        private void ListeReservClientModif_SelectedIndexChanger(object sender, EventArgs e)
        {
            int SelectedIndex = ListeReservClientModif.SelectedIndex;
            idReservSelection = ListeIdReservationChoisi.ElementAt(SelectedIndex);
        }

        //On récupère l'id de la reserv à supprimer
        private void ListeReservClientSuppr_SelectedIndexChanger(object sender, EventArgs e)
        {
            int SelectedIndex = ListeReservClientSuppr.SelectedIndex;
            idReservSelection = ListeIdReservationChoisi.ElementAt(SelectedIndex);
        }

        private void BoutonFaireReservation_Click(object sender, EventArgs e)
        {
            if(ListeClientReserv.Text !="" && ListeTypeChambre.Text != "" && ListeNumeroChambre.Text != "" && dateTimeDebut.Text != "" && dateTimeFin.Text != "" && (BoutonRadioParkingOui.Checked || BoutonRadioParkingNon.Checked))
            {
                int IdClient = idDuClientSelection;
                int IdTypeChambre = idTypeChambreSelection;
                int NumeroChambre = idChambreSelection;
                DateTime DateDebut = Convert.ToDateTime(dateTimeDebut.Text);
                DateTime DateFin = Convert.ToDateTime(dateTimeFin.Text);
                bool ParkingOui = BoutonRadioParkingOui.Checked;
                int Parking;
                int idReservation = 0;
                
                if(ParkingOui == true)
                {
                    if (parking.CreationListePlaceLibre() == 0)
                    {
                        Parking = 0;
                        MessageBox.Show("Il n'y a plus de place de parking.");
                    }
                    else
                    {
                        Parking = 1;
                    }
                }
                else
                {
                    Parking = 0;
                }

                int Prix = reservation.PrixDeLaNuit(NumeroChambre);

                reservation = new Reservation(idReservation, DateDebut, DateFin, Prix, IdClient, NumeroChambre, Parking);
                reservation.AjouterReservation();

                MessageBox.Show("La réservation a bien été effectuer.");
                ActualisationData();
                ListeNumeroChambre.Items.Clear();
            }
            else
            {
                MessageBox.Show("Vous devez remplir tous les champs pour faire une réservation.");
            }

        }

        private void BoutonModifierReservation_Click(object sender, EventArgs e)
        {
            if(ListeClientReservModif.Text != "" && ListeReservClientModif.Text !="" && ListeTypeChambreModif.Text != "" && ListeNumeroChambreModif.Text != "" && dateTimeDebutModif.Text != "" && dateTimeFinModif.Text !="" && (BoutonRadioParkingOuiModiff.Checked || BoutonRadioParkingNonModiff.Checked))
            {
                int IdClient = idDuClientSelection;
                int IdReservation = idReservSelection;
                int NumeroChambre = idChambreSelection;
                DateTime DateDebut = Convert.ToDateTime(dateTimeDebutModif.Text);
                DateTime DateFin = Convert.ToDateTime(dateTimeFinModif.Text);
                bool ParkingOui = BoutonRadioParkingOuiModiff.Checked;
                int Parking;

                int ParkingActuel = reservation.RetourneParkingActuel(IdReservation);

                if (ParkingOui == true)//S'il veut une place
                {
                    if(parking.CreationListePlaceLibre() == 0 && ParkingActuel == 0)//Et s'il n'en a pas déjà
                    {
                        Parking = 0;
                        MessageBox.Show("Il n'y a plus de place de parking.");
                    }
                    else //S'il en a déjà une
                    {
                        Parking = 1;
                    }
                }
                else
                {
                    Parking = 0;
                }

                int Prix = reservation.PrixDeLaNuit(NumeroChambre);

                reservation = new Reservation(IdReservation, DateDebut, DateFin, Prix, IdClient, NumeroChambre, Parking);
                reservation.ModifierReservation();

                MessageBox.Show("Votre modification a bien été prise en compte.");

                ActualisationData();
            }
            else
            {
                MessageBox.Show("Vous devez remplir tous les champs pour modifier une réservation.");
            }
        }

        private void BoutonSupprimerReservation_Click(object sender, EventArgs e)
        {
            if(ListeClientReservSuppr.Text != "" && ListeReservClientSuppr.Text != "")
            {
                int IdReservation = idReservSelection;

                reservation.SupprimerReservation(IdReservation);
                MessageBox.Show("La réservation a bien été supprimé.");

                ActualisationData();
            }
            else
            {
                MessageBox.Show("Vous devez remplir tous les champs pour supprimer une réservation.");
            }
        }














        //------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------Chambre--------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------

        public void EffacerTextBoxChambre()
        {
            NombreChambreSeul.Text = "0";
            NombreChambreDuo.Text = "0";
            NombreChambreFamiliale4.Text = "0";
            NombreChambreFamiliale6.Text = "0";
            NombreChambreColonie.Text = "0";

            BoutonRadioToutesLesChambres.Checked = false;
            BoutonRadioChambresLibres.Checked = false;

            dataGridView3.Rows.Clear();
        }

        public void ActualiserNombreChambre()
        {
            if(chambre.NombreChambreDispo().TryGetValue("Chambre seul", out int NbChambreSeul))
            {
                NombreChambreSeul.Text = NbChambreSeul.ToString();
            }
            if(chambre.NombreChambreDispo().TryGetValue("Chambre duo", out int NbChambreDuo))
            {
                NombreChambreDuo.Text = NbChambreDuo.ToString();
            }
            if(chambre.NombreChambreDispo().TryGetValue("Chambre familiale (4)", out int NbChambreFamiliale4))
            {
                NombreChambreFamiliale4.Text = NbChambreFamiliale4.ToString();
            }
            if(chambre.NombreChambreDispo().TryGetValue("Chambre familiale (6)", out int NbChambreFamiliale6))
            {
                NombreChambreFamiliale6.Text = NbChambreFamiliale6.ToString();
            }
            if (chambre.NombreChambreDispo().TryGetValue("Colonie (8)", out int NbColonie))
            {
                NombreChambreColonie.Text = NbColonie.ToString();
            }
        }


        private void ActualiserListeDataGridTouteLesChambres()
        {
            dataGridView3.Rows.Clear();

            foreach (Chambre chambre in chambre.CreationListeCompleteToutesLesChambres())
            {
                string NomTypeChambre = chambre.GetNomTypeChambre();
                int NumeroChambre = chambre.GetNumeroChambre();
                string NomVueChambre = chambre.GetLibelleVueChambre();
                int Prix = chambre.GetPrixParNuit();
                bool Occuper = chambre.GetOccupper();

                if(Occuper == true)
                {
                    Statut = "Oui";
                }
                else
                {
                    Statut = "Non";
                }

                dataGridView3.Rows.Add(NomTypeChambre, NumeroChambre, NomVueChambre, Prix + " €", Statut);
            }
        }

        private void ActualiserListeDataGridChambreLibre()
        {
            dataGridView3.Rows.Clear();
            
            foreach (Chambre chambre in chambre.CreationListeCompleteChambreDisponible())
            {
                string NomTypeChambre = chambre.GetNomTypeChambre();
                int NumeroChambre = chambre.GetNumeroChambre();
                string NomVueChambre = chambre.GetLibelleVueChambre();
                int Prix = chambre.GetPrixParNuit();

                string Statut = "Non"; //On sait que les chambres sont dispo
                
                dataGridView3.Rows.Add(NomTypeChambre, NumeroChambre, NomVueChambre, Prix + " €", Statut);
            }
        }

        private void BoutonRadioToutesLesChambres_Click(object sender, EventArgs e)
        {
            ActualiserListeDataGridTouteLesChambres();
        }

        private void BoutonRadioChambresLibres_Click(object sender, EventArgs e)
        {
            ActualiserListeDataGridChambreLibre();
        }
    }
}

