using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oddity;
using Oddity.Models.Capsules;
using Oddity.Models.Crew;
using Oddity.Models.Launches;
using Oddity.Models.Rockets;

namespace OddityX.ViewModels;

public class CapsuleInfoView
{
    private readonly CapsuleInfo _capsule;

    public CapsuleInfoView(CapsuleInfo capsule)
    {
        _capsule = capsule;
    }

    public string Serial => _capsule.Serial;

    public string Status => _capsule.Status.ToString();

    public uint? ReuseCount => _capsule.ReuseCount;

    public uint? CountWaterLanding => _capsule.WaterLandings;

    public uint? CountLandLanding => _capsule.LandLandings;

    public string LastUpdate => _capsule.LastUpdate;

    private List<string> LaunchesId => _capsule.LaunchesId;

    public async Task<int> GetCountLaunches()
    {
        var launches = await App.OddityCore.LaunchesEndpoint.GetAll().ExecuteAsync();
        return launches.FindAll(l => l.CapsulesId.Contains(_capsule.Id)).Count;
    }

    public async Task<List<LaunchInfo>> GetCapsuleLaunches()
    {
        var launches = await App.OddityCore.LaunchesEndpoint.GetAll().ExecuteAsync();
        List<LaunchInfo> capsuleLaunches = new();
        LaunchesId.ForEach(launchId => capsuleLaunches.AddRange(launches.Where(l => l.Id == launchId)));

        if (!capsuleLaunches.Any())
        {
            var newInfo = new LaunchInfo() { Name = "Doesn't have launches" };
            capsuleLaunches.Add(newInfo);
        }

        return capsuleLaunches;
    }

    public async Task<List<CrewInfo>> GetCapsuleCrew()
    {
        var launches = await App.OddityCore.LaunchesEndpoint.GetAll().ExecuteAsync();
        var crews = await App.OddityCore.CrewEndpoint.GetAll().ExecuteAsync();

        List<LaunchInfo> capsuleLaunches = new();
        List<CrewInfo> capsuleCrew = new();
        LaunchesId.ForEach(launchId => capsuleLaunches.AddRange(launches.Where(l => l.Id == launchId)));

        if (!capsuleLaunches.Any())
        {
            capsuleCrew.Add(new CrewInfo() { Name = "Haven't had a crew yet" });
        }
        else
        {
            foreach (var crewId in capsuleLaunches.SelectMany(launch => launch.CrewId))
            {
                capsuleCrew.AddRange(crews.Where(crew => crew.Id == crewId));
            }

            if (!capsuleCrew.Any())
            {
                capsuleCrew.Add(new CrewInfo() { Name = "Haven't had a crew yet" });
            }
        }

        return capsuleCrew;
    }

    public async Task<List<RocketInfo>> GetCapsuleRockets()
    {
        var launches = await App.OddityCore.LaunchesEndpoint.GetAll().ExecuteAsync();
        var rockets = await App.OddityCore.RocketsEndpoint.GetAll().ExecuteAsync();

        List<LaunchInfo> capsuleLaunches = new();
        List<RocketInfo> capsuleRockets = new();
        LaunchesId.ForEach(launchId => capsuleLaunches.AddRange(launches.Where(l => l.Id == launchId)));

        if (!capsuleLaunches.Any())
        {
            capsuleRockets.Add(new RocketInfo() { Name = "Haven't had rockets yet" });
        }
        else
        {
            foreach (var launch in capsuleLaunches)
            {
                capsuleRockets.AddRange(rockets.FindAll(rocket => rocket.Id == launch.RocketId));
            }

            if (!capsuleRockets.Any())
            {
                capsuleRockets.Add(new RocketInfo() { Name = "Haven't had rockets yet" });
            }
        }

        return capsuleRockets;
    }
}