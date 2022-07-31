using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OddityX.Helpers.HistorySpaceX;

public class HistoryActions
{
    private const string HistoryUrl = "https://api.spacexdata.com/v4/history";

    // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
    private readonly HttpClient _client;

    [Obsolete("Obsolete")]
    public HistoryActions()
    {
        _client = new HttpClient(); ;
    }

    public async Task<HistoryModel> GetOne(string historyId)
    {
        HttpResponseMessage response = await _client.GetAsync(HistoryUrl + $"/{historyId}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();

        var history = JsonConvert.DeserializeObject<HistoryModel>(json);

        return history;
    }

    public async Task<List<HistoryModel>> GetAll()
    {
        var response = await _client.GetAsync(HistoryUrl);
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();

        var histories = JsonConvert.DeserializeObject<List<HistoryModel>>(json);

        return histories;
    }

    ~HistoryActions()
    {
        _client.Dispose();
    }
}