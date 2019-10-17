using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace getcity.Controllers
{
    public class CityController : ApiController
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["CityString"].ToString();
            con = new SqlConnection(constring);
        }


        public IHttpActionResult GetCountry()
        {
            connection();
            List<Country> countrylist = new List<Country>();

            SqlCommand cmd = new SqlCommand("select * from country", con);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                countrylist.Add(
                    new Country
                    {
                        CountryID = Convert.ToInt32(dr["CountryID"]),
                        CountryName = Convert.ToString(dr["CountryName"])

                    });
            }
            return Ok(countrylist);
        }

        public IHttpActionResult GetCity(int countryID)
        {
            connection();
            List<City> citylist = new List<City>();

            SqlCommand cmd = new SqlCommand("select * from city where CountryID=@countryID", con);
            cmd.Parameters.AddWithValue("@countryID", countryID);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                citylist.Add(
                    new City
                    {
                        CityID = Convert.ToInt32(dr["CityID"]),
                        CountryID = Convert.ToInt32(dr["CountryID"]),
                        CityName = Convert.ToString(dr["CityName"])

                    });
            }
            return Ok(citylist);
        }


        public IHttpActionResult GetTown(int cityID)
        {
            connection();
            List<Town> townlist = new List<Town>();

            SqlCommand cmd = new SqlCommand("select * from town where CityID=@cityID", con);
            cmd.Parameters.AddWithValue("@cityID", cityID);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                townlist.Add(
                    new Town
                    {
                        TownID = Convert.ToInt32(dr["TownID"]),
                        CityID = Convert.ToInt32(dr["CityID"]),
                        TownName = Convert.ToString(dr["TownName"])

                    });
            }
            return Ok(townlist);
        }

        public IHttpActionResult GetDistrict(int townID)
        {
            connection();
            List<District> list = new List<District>();

            SqlCommand cmd = new SqlCommand("select * from district where TownID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", townID);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(
                    new District
                    {
                        DistrictID = Convert.ToInt32(dr["DistrictID"]),
                        TownID = Convert.ToInt32(dr["TownID"]),
                        DistrictName = Convert.ToString(dr["DistrictName"])

                    });
            }
            return Ok(list);
        }

        public IHttpActionResult GetNeighbourhood(int districtID)
        {
            connection();
            List<Neighbourhood> list = new List<Neighbourhood>();

            SqlCommand cmd = new SqlCommand("select * from neighborhood where DistrictID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", districtID);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(
                    new Neighbourhood
                    {
                        NeighbourhoodID = Convert.ToInt32(dr["NeighborhoodID"]),
                        DistrictID = Convert.ToInt32(dr["DistrictID"]),
                        NeighbourhoodName = Convert.ToString(dr["NeighborhoodName"])

                    });
            }
            return Ok(list);
        }

    }

    internal class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

    }

    internal class City
    {
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public string CityName { get; set; }

    }

    internal class Town
    {
        public int TownID { get; set; }
        public int CityID { get; set; }
        public string TownName { get; set; }

    }

    internal class District
    {
        public int DistrictID { get; set; }
        public int TownID { get; set; }
        public string DistrictName { get; set; }

    }

    internal class Neighbourhood
    {
        public int NeighbourhoodID { get; set; }
        public int DistrictID { get; set; }
        public string NeighbourhoodName { get; set; }

    }
}
