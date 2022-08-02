#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using Oddity;
using Oddity.Models.Launches;
using Oddity.Models.Ships;

namespace OddityX.ViewModels;

public class ShipInfoView
{
    private readonly ShipInfo _ship;
    private readonly OddityCore _core;

    public ShipInfoView(ShipInfo currentShip)
    {
        _ship = currentShip;
        _core = new OddityCore();
    }

    public string ShipName => _ship.Name;

    public bool? IsShipActive => _ship.Active;

    public string? ShipModel => _ship.Model;

    public string? ShipType => _ship.Type;

    public string ShipStatus => _ship.Status;

    public Uri? ShipLink => _ship.Link is null ? null : new Uri(_ship.Link);

    public string? ShipImage => _ship.Image;

    public List<string> ShipRoles => _ship.Roles;

    public double? ShipLatitude => _ship.Latitude;

    public double? ShipLongitude => _ship.Longitude;

    public string? ShipLastUpdate => _ship.LastAisUpdate.ToString();

    public async Task<List<LaunchInfo>> GetShipLaunches()
    {
        var listLaunches = await _core.LaunchesEndpoint.GetAll().ExecuteAsync();
        return listLaunches.Where(l => _ship.LaunchesId.Any(lid => l.Id == lid)).ToList();
    }
}