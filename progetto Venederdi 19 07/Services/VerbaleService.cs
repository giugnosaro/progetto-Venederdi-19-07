using Microsoft.Data.SqlClient;
using progetto_Venederdi_19_07.ViewModels;
using System.Text.RegularExpressions;

namespace progetto_Venederdi_19_07.Services
{
    public class VerbaleService
    {
        private readonly IConfiguration _configuration;

        public VerbaleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<VerbaleViewModel> GetVerbali()
        {
            List<VerbaleViewModel> ListaVerbali = new List<VerbaleViewModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();
                    const string SELECT_ALL_CMD = "SELECT v.IdVerbale,a.IdAnagrafica,a.Nome,a.Cognome,v.IdViolazione,t.Descrizione,v.DataViolazione,v.IndirizzoViolazione,v.Nominativo_Agente,v.DataTrascrizioneVerbale,v.Importo,v.DecurtamentoPunti FROM VERBALE AS V INNER JOIN ANAGRAFICA AS A ON A.IdAnagrafica = V.IdAnagrafica INNER JOIN TIPO_VIOLAZIONE AS T ON T.IdViolazione = V.IdViolazione";
                    using (SqlCommand cmd = new SqlCommand(SELECT_ALL_CMD, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VerbaleViewModel verbale = new VerbaleViewModel
                                {
                                    IdVerbale = reader.GetInt32(0),
                                    IdUtente = reader.GetInt32(1),
                                    Nome = reader.GetString(2),
                                    Cognome = reader.GetString(3),
                                    IdViolazione = reader.GetInt32(4),
                                    Descrizione = reader.GetString(5),
                                    DataViolazione = reader.GetDateTime(6),
                                    IndirizzoViolazione = reader.GetString(7),
                                    Nominativo_Agente = reader.GetString(8),
                                    DataTrascrizioneVerbale = reader.GetDateTime(9),
                                    Importo = reader.GetDecimal(10),
                                    DecurtamentoPunti = reader.GetInt32(11)
                                };
                                ListaVerbali.Add(verbale);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero dei verbali", ex);
            }
            return ListaVerbali;
        }


        public void NuovoVerbale(VerbaleViewModel verb)

        {

            try

            {

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

                {

                    conn.Open();

                    const string INSERT_CMD = "INSERT INTO VERBALE (IdAnagrafica,IdViolazione,DataViolazione,IndirizzoViolazione,Nominativo_Agente,DataTrascrizioneVerbale,Importo,DecurtamentoPunti) VALUES (@IdAnagrafica,@IdViolazione,@DataViolazione,@IndirizzoViolazione,@Nominativo_Agente,@DataTrascrizioneVerbale,@Importo,@DecurtamentoPunti)";

                    using (SqlCommand cmd = new SqlCommand(INSERT_CMD, conn))

                    {

                        cmd.Parameters.AddWithValue("@IdAnagrafica", verb.IdUtente);

                        cmd.Parameters.AddWithValue("@IdViolazione", verb.IdViolazione);

                        cmd.Parameters.AddWithValue("@DataViolazione", verb.DataViolazione);

                        cmd.Parameters.AddWithValue("@IndirizzoViolazione", verb.IndirizzoViolazione);

                        cmd.Parameters.AddWithValue("@Nominativo_Agente", verb.Nominativo_Agente);

                        cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verb.DataTrascrizioneVerbale);

                        cmd.Parameters.AddWithValue("@Importo", verb.Importo);

                        cmd.Parameters.AddWithValue("@DecurtamentoPunti", verb.DecurtamentoPunti);

                        cmd.ExecuteNonQuery();

                    }

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Errore nell'inserimento del verbale", ex);

            }

        }

        public List<Violazione> TutteLeViolazioni()

        {

            List<Violazione> ListaViolazioni = new List<Violazione>();

            try

            {

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

                {

                    conn.Open();

                    const string SELECT_ALL_CMD = "SELECT * FROM TIPO_VIOLAZIONE";

                    using (SqlCommand cmd = new SqlCommand(SELECT_ALL_CMD, conn))

                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {

                            while (reader.Read())

                            {

                                Violazione violazione = new Violazione

                                {

                                    IdViolazione = reader.GetInt32(0),

                                    Descrizione = reader.GetString(1),

                                };

                                ListaViolazioni.Add(violazione);

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Errore nel recupero delle violazioni", ex);

            }

            return ListaViolazioni;


        }

        public List<VerbaleViewModel> TotVerbTrasgressore()
        {

            List<VerbaleViewModel> ListaUtenti = new List<VerbaleViewModel>();

            try

            {

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

                {
                    conn.Open();

                    const string SELECT_ALL_CMD = "SELECT a.Nome,a.Cognome,COUNT(v.IdVerbale) as TotaleVerbali FROM VERBALE AS V INNER JOIN ANAGRAFICA AS A ON A.IdAnagrafica = V.IdAnagrafica INNER JOIN TIPO_VIOLAZIONE AS T ON T.IdViolazione = V.IdViolazione GROUP BY a.Nome, a.Cognome";

                    using (SqlCommand cmd = new SqlCommand(SELECT_ALL_CMD, conn))

                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {

                            while (reader.Read())

                            {

                                VerbaleViewModel utente = new VerbaleViewModel

                                {

                                    Nome = reader.GetString(0),

                                    Cognome = reader.GetString(1),

                                    TotVerbali = reader.GetInt32(2),

                                };

                                ListaUtenti.Add(utente);

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Errore nel recupero dei verbali", ex);

            }

            return ListaUtenti;

        }

        public List<VerbaleViewModel> TotPuntiDecurtati()
        {


            List<VerbaleViewModel> ListaUtenti = new List<VerbaleViewModel>();

            try

            {

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

                {

                    conn.Open();

                    const string SELECT_ALL_CMD = "SELECT a.Nome,a.Cognome,SUM(v.DecurtamentoPunti) as TotalePunti FROM VERBALE AS V INNER JOIN ANAGRAFICA AS A ON A.IdAnagrafica = V.IdAnagrafica INNER JOIN TIPO_VIOLAZIONE AS T ON T.IdViolazione = V.IdViolazione GROUP BY a.Nome, a.Cognome";

                    using (SqlCommand cmd = new SqlCommand(SELECT_ALL_CMD, conn))

                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {

                            while (reader.Read())

                            {

                                VerbaleViewModel utente = new VerbaleViewModel

                                {

                                    Nome = reader.GetString(0),

                                    Cognome = reader.GetString(1),

                                    DecurtamentoPunti = reader.GetInt32(2),

                                };

                                ListaUtenti.Add(utente);

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Errore nel recupero dei verbali", ex);

            }

            return ListaUtenti;


        }


        public List<VerbaleViewModel> VisualizzazionePiuDiDieciPunti()
        {

            List<VerbaleViewModel> ListaUtenti = new List<VerbaleViewModel>();

            try

            {

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

                {

                    conn.Open();

                    const string SELECT_ALL_CMD = "SELECT a.Nome , a.Cognome, v.DataViolazione, v.DecurtamentoPunti , v.Importo FROM ANAGRAFICA as a Inner Join VERBALE as v on a.IdAnagrafica = v.IdAnagrafica where DecurtamentoPunti >= 10 ";

                    using (SqlCommand cmd = new SqlCommand(SELECT_ALL_CMD, conn))

                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {

                            while (reader.Read())

                            {

                                VerbaleViewModel utente = new VerbaleViewModel

                                {

                                    Nome = reader.GetString(0),
                                    Cognome = reader.GetString(1),
                                    DataViolazione = reader.GetDateTime(2),
                                    DecurtamentoPunti = reader.GetInt32(3),
                                    Importo = reader.GetDecimal(4),

                                };

                                ListaUtenti.Add(utente);

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Errore nel recupero dei verbali", ex);

            }

            return ListaUtenti;


        }

          public List<VerbaleViewModel> VisualizzazionePiuDiQuattroCentoEuro()
        {

            List<VerbaleViewModel> ListaUtenti = new List<VerbaleViewModel>();

            try

            {

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

                {

                    conn.Open();

                    const string SELECT_ALL_CMD = "SELECT a.Nome , a.Cognome, v.DataViolazione, v.DecurtamentoPunti , v.Importo , t.Descrizione FROM ANAGRAFICA as a Inner Join VERBALE as v on a.IdAnagrafica = v.IdAnagrafica Inner Join TIPO_VIOLAZIONE as t on v.IdViolazione = t.IdViolazione where Importo >= 400 ";

                    using (SqlCommand cmd = new SqlCommand(SELECT_ALL_CMD, conn))

                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {

                            while (reader.Read())

                            {

                                VerbaleViewModel utente = new VerbaleViewModel

                                {

                                    Nome = reader.GetString(0),
                                    Cognome = reader.GetString(1),
                                    DataViolazione = reader.GetDateTime(2),
                                    DecurtamentoPunti = reader.GetInt32(3),
                                    Importo = reader.GetDecimal(4),
                                    Descrizione = reader.GetString(5),

                                };

                                ListaUtenti.Add(utente);

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Errore nel recupero dei verbali", ex);

            }

            return ListaUtenti;

    }
    }
    }
    


