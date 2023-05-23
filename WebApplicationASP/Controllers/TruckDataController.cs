using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplicationASP.Models;

namespace WebApplicationASP.Controllers;

public class TruckDataController: Controller
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiUrl = "https://ptosm.pelindo.co.id/api/monitoring/TruckMonitoring/getListDTPub";

    public async Task<IActionResult> GetTruckData()
    {
        string offset = "0";
        string limit = "10";
        string cari = "";
        string token = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiI1Mjk0OSIsInVzZXJuYW1lIjoiMzkyMTExOTgyIiwiaGFrX2Frc2VzX2lkIjozMTQzLCJrZF9yZWdpb25hbCI6MTMzMCwia2RfY2FiYW5nIjo2MSwia2RfdGVybWluYWwiOjYsIm5tX3JlZ2lvbmFsIjoiUFQgUEVMSU5ETyBNVUxUSSBURVJNSU5BTCIsIm5tX2NhYmFuZyI6IlBUIFBFTElORE8gTVVMVEkgVEVSTUlOQUwiLCJubV90ZXJtaW5hbCI6IlRFUk1JTkFMIEpBTVJVRCIsInBlcnNvbl9hcmVhIjoiMTMzMCIsInBlcnNvbl9zdWJfYXJlYSI6IjA1MDIiLCJpYXQiOjE2ODQ4MTcyNDAsImV4cCI6MTY4NDkwMzY0MH0.6MJF2UpLWcBwZDac98ghvWAdOyVyrZU1LLzDKpPK6aDqajxliiXu-IeWQ6uzAsgGeCsVh_tCUA0mn7WO7pDYIA";

        string requestUrl = $"{apiUrl}?offset={offset}&limit={limit}&cari={cari}&token={token}";

        HttpResponseMessage httpResponseMessage = await client.GetAsync(requestUrl);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseData);
            var dataArray = jsonResponse["result"]["data"];
            var truckDataList = dataArray.ToObject<List<TruckData>>();
            return View("GetTruckData", truckDataList);
        }
        else
        {
            return StatusCode((int)httpResponseMessage.StatusCode);
        }
    }
}