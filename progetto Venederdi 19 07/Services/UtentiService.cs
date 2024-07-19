using Microsoft.Data.SqlClient;
using progetto_Venederdi_19_07.ViewModels;

namespace progetto_Venederdi_19_07.Services
{
    public class UtentiService
    {

        private readonly IConfiguration _configuration;

        public UtentiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<UtentiViewModel> NuovoUtente()
        {
            List<UtentiViewModel> utenti = new List<UtentiViewModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Anagrafica", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UtentiViewModel utente = new UtentiViewModel
                            {
                                IdAnagrafica = reader.GetInt32(0),
                                Cognome = reader.GetString(1),
                                Nome = reader.GetString(2),
                                Cod_Fisc = reader.GetString(3),
                                Indirizzo = reader.GetString(4),
                                Città = reader.GetString(5),
                                Cap = reader.GetString(6)
                            };

                            utenti.Add(utente);
                        }
                    }
                }
            }

            return utenti;
        }


        public void NuovoUtente(UtentiViewModel utente)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Anagrafica (Cognome, Nome, Cod_Fisc, Indirizzo, Città, Cap) VALUES (@Cognome, @Nome, @Cod_Fisc, @Indirizzo, @Città, @Cap)", connection))
                {
                    command.Parameters.AddWithValue("@Cognome", utente.Cognome);
                    command.Parameters.AddWithValue("@Nome", utente.Nome);
                    command.Parameters.AddWithValue("@Cod_Fisc", utente.Cod_Fisc);
                    command.Parameters.AddWithValue("@Indirizzo", utente.Indirizzo);
                    command.Parameters.AddWithValue("@Città", utente.Città);
                    command.Parameters.AddWithValue("@Cap", utente.Cap);

                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
