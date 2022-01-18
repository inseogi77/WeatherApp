﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweater.com/ ";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITION_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = "u8PTaY74NvRVX1mEylWIHthVyODHtLca";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT,API_KEY,query);

            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string citykey)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            string url = BASE_URL + string.Format(citykey, CURRENT_CONDITION_ENDPOINT, citykey);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
