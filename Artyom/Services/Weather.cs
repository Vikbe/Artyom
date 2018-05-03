using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DarkSky.Models;
using DarkSky.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Artyom.Services
{
    class Weather
    {
        private static readonly string apiKey;
        private static DarkSkyService m_service;

        static Weather()
        {
            apiKey = Config.bot.DSkyApi;
            m_service = new DarkSkyService(apiKey);
        }



        public static async Task<DarkSkyResponse> StockholmForecast()
        {
          var forecast = await m_service.GetForecast(59.34, 18.048);
            return forecast;
        }

        public static async Task<DarkSkyResponse> KansasCityForeCast()
        {
            var forecast = await m_service.GetForecast(39.099, -94.57);
            return forecast;
        }
    }
}