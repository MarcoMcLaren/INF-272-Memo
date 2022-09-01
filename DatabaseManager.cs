using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CT2SourceCode.Models;
using System.Data.SqlClient;

namespace CT2SourceCode.TODO
{
    public class DatabaseManager
    {
        public static string ConnectionString = "Data Source=MARCO_LAPTOP;Initial Catalog=CT2CountryStats;Integrated Security=True";
        //Q1
        public SqlConnection openDBConnection() {
            SqlConnection con = null;
            con = new SqlConnection(ConnectionString);
            //TODO: open connection and return it. 
            con.Open();
            return con;
        }


        //Q2
        public void updateCountryNames()
        {
            List<CountryModel> countries = getAllCountries();
            //TODO: Update the names of the countries
            try
            {
                SqlCommand myCommand = new SqlCommand("UPDATE Countries SET Country_Name = SUBSTRING(Country_Name,1,LEN(Country_Name)-1) " +
                    "WHERE Country_Name LIKE '%X%'", openDBConnection());

            }
            catch
            {
                
            }
            finally
            {
                openDBConnection().Close();
            }
        }

        //Q3
        public void deleteInvalidCountry()
        {
            //TODO: Delete references to Pretoria

            //Hint: its ok to harcode the pretoria Id
            try
            {
                SqlCommand myCommand = new SqlCommand("DELETE from YearlyData  where countryID = 13" + "DELETE from Countries where ID = 13", openDBConnection());

            }
            catch
            {

            }
            finally
            {
                openDBConnection().Close();
            }
        }




        //=========================ENDS==================================














































































































        //NO need to worry about all the methods that will follow:

        public List<CountryIndicatorViewModel> getAllCountryIndicators()
        {
            List<CountryIndicatorViewModel> data = new List<CountryIndicatorViewModel>();

            List<Tuple<int, int>> countryIdAndIndicatorIdPairs = getCountryIndicatorIdPair();


            foreach (Tuple<int, int> idPair in countryIdAndIndicatorIdPairs) {

                if (!data.Where(model => model.Country.ID == idPair.Item1).Any())
                { 
                    data.Add(new CountryIndicatorViewModel
                    {
                        Country = getCountryModelByID(idPair.Item1),
                        Indicators = getAllIndicatorModels(idPair.Item1, idPair.Item2)
                    });
                }

            }
            
            return data;
        }

        public List<Tuple<int, int>> getCountryIndicatorIdPair() {
            List<Tuple<int, int>> data = new List<Tuple<int, int>>();

            try
            {
                String q = "select countryID, indicatorID from YearlyData";
                using (SqlConnection con = openDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Add(
                                    new Tuple<int, int>(Convert.ToInt32(reader["countryID"]), Convert.ToInt32(reader["indicatorID"]))
                                    );
                            }
                        }
                    }
                }
            } catch (Exception e) { 
            
            }

            return data;
        }

        public List<CountryModel> getAllCountries()
        {
            List<CountryModel> countries = new List<CountryModel>();
            String q = "select * from Countries";

            using (SqlConnection con = openDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                countries.Add(
                                    new CountryModel
                                    {
                                        CountryName = Convert.ToString(reader["Country_Name"]),
                                        CountryCode = Convert.ToString(reader["Country_Code"]),
                                        ID = Convert.ToInt32(reader["ID"])
                                    }
                                );
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            return countries;
        }


        public CountryModel getCountryModelByID(int id) {
            CountryModel country = null;

            String q = "select * from Countries where id = " + id;
            using (SqlConnection con = openDBConnection()) {
                using (SqlCommand cmd = new SqlCommand(q, con)) {
                    try {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            country = new CountryModel
                            {
                                CountryName = Convert.ToString(reader["Country_Name"]),
                                CountryCode = Convert.ToString(reader["Country_Code"]),
                                ID = Convert.ToInt32(reader["ID"])
                            };
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return country;
        }

        public List<IndicatorModel> getAllIndicatorModels(int CountryId, int IndicatorId)
        {
            List<IndicatorModel> indicatorModels = new List<IndicatorModel>();

            String q = @"select YearlyData.countryId, YearlyData.indicatorID, YearlyData.Year, YearlyData.Pct, Indicators.SeriesName
                    from YearlyData
                    Inner Join Indicators ON YearlyData.indicatorID="+ IndicatorId + " and YearlyData.countryId="+ CountryId;
            using (SqlConnection con = openDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IndicatorModel indicator = new IndicatorModel
                                {
                                    ID = Convert.ToInt32(reader["indicatorID"]),
                                    SeriesName = Convert.ToString(reader["SeriesName"]),
                                    Year = Convert.ToInt32(reader["Year"]),
                                    Pct = Convert.ToDouble(reader["Pct"])
                                };

                                if (!indicatorModels.Where(indx => indx.ID == IndicatorId && indx.Year == indicator.Year).Any()) { //dirty hack. never do this
                                    indicatorModels.Add(indicator);
                                } 
                                
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

                return indicatorModels;
            }
        }
    }
}
