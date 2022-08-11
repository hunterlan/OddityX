#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oddity.Models.Crew;
using Oddity.Models.Launches;
using OddityX.Helpers;

namespace OddityX.ViewModels;

public class CrewInfoView
{
    private readonly CrewInfo _crew;

    public CrewInfoView(CrewInfo crew)
    {
        _crew = crew;
    }

    public string Name => _crew.Name;

    public string Agency => _crew.Agency;

    public string Status => _crew.Status.ToString();

    public string Wikipedia => _crew.Wikipedia;

    public string? Image => _crew.Image;

    public async Task<List<LaunchRecordInfo>> GetCrewLaunches()
    {
        var launches = await App.OddityCore.LaunchesEndpoint.GetAll().ExecuteAsync();
        var crewLaunchesList = launches.Where(launch => launch.CrewId.Any(ci => ci == _crew.Id)).ToList();
        var crewLaunchesRecord = Map(crewLaunchesList);

        return crewLaunchesRecord;
    }

    private List<LaunchRecordInfo> Map(List<LaunchInfo> launchInfo)
    {
        return launchInfo.Select(launch => new LaunchRecordInfo()
        {
            Name = launch.Name,
            Success = launch.Success,
            Upcoming = launch.Upcoming,
            FlightNumber = launch.FlightNumber,
            DateUtc = launch.DateUtc,
            DateLocal = launch.DateLocal
        }).ToList();
    }
}