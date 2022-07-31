using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace OddityX.Helpers.HistorySpaceX;

public class HistoryActions
{
    private const string HistoryUrl = "https://api.spacexdata.com/v4/history";

    private readonly WebClient _client;

    [Obsolete("Obsolete")]
    public HistoryActions()
    {
        _client = new WebClient();
    }

    public HistoryModel GetOne(string historyId)
    {
        string json = _client.DownloadString(HistoryUrl + $"/{historyId}");

        var history = JsonConvert.DeserializeObject<HistoryModel>(json);

        return history;
    }

    public List<HistoryModel> GetAll()
    {
        string json = _client.DownloadString(HistoryUrl);

        var history = JsonConvert.DeserializeObject<List<HistoryModel>>(HistoryUrl);

        return history;
    }

    ~HistoryActions()
    {
        _client.Dispose();
    }
}