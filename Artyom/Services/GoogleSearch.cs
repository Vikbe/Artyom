using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Artyom;

namespace Disco.Services
{
    class GoogleSearch
    {
        private static string apiKey = Config.bot.GoogleApi;
        private static string cx = Config.bot.SearchId;

        public static CustomsearchService Service = new CustomsearchService(
            new BaseClientService.Initializer
            {
                ApiKey = apiKey
            });
        

        public static async Task<IList<Result>> Search(string query)
        {
            CseResource.ListRequest listRequest = Service.Cse.List(query);
            listRequest.Cx = cx; 
            
            Search search = await listRequest.ExecuteAsync();

            return search.Items; 
        } 

    

    }
}
