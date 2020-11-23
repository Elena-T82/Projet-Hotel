using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hôtel_Obegua
{
    class TypeChambre
    {
        private TypeChambre typeChambre;
        private int IdTypeChambre;
        private string NomTypeChambre;
        private List<TypeChambre> ListeTypeChambre = new List<TypeChambre>();

        private AccessBDD Data = new AccessBDD("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");
        private static MySqlConnection connect = new MySqlConnection("Server = localhost; Database = hotel; Uid = Elena; Pwd =password82");

        public int GetIdTypeChambre() { return IdTypeChambre; }
        public string GetNomTypeChambre() { return NomTypeChambre; }
        public List<TypeChambre> GetListeTypeChambre() { return ListeTypeChambre; }


        public TypeChambre(int IdTypeChambre, string NomTypeChambre)
        {
            this.IdTypeChambre = IdTypeChambre;
            this.NomTypeChambre = NomTypeChambre;
        }

        public TypeChambre()
        {

        }


        public List<TypeChambre> ListeDeroulanteTypeChambre()
        {
            connect.Open();

            GetListeTypeChambre().Clear();

            MySqlCommand requete = new MySqlCommand("SELECT * FROM TypeChambre", connect);
            MySqlDataReader LireRequete = requete.ExecuteReader();

            while(LireRequete.Read())
            {
                int idTypeChambre = Convert.ToInt32(LireRequete["IdTypeChambre"]);
                string nomTypeChambre = Convert.ToString(LireRequete["NomTypeChambre"]);

                typeChambre = new TypeChambre(idTypeChambre, nomTypeChambre);

                GetListeTypeChambre().Add(typeChambre);
            }

            connect.Close();
            return GetListeTypeChambre();

        }
    }
}
