﻿using JEH01V_HFT_2021222.Endpoint.Services;
using JEH01V_HFT_2021222.Logic.Interfaces;
using JEH01V_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace JEH01V_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        IWeaponLogic wLogic;
        IHubContext<SignalRHub> hub;

        public WeaponController(IWeaponLogic wLogic, IHubContext<SignalRHub> hub)
        {
            this.wLogic = wLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Weapon> ReadAll()
        {
            return wLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Weapon Read(int id)
        {
            return wLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Weapon value)
        {
            wLogic.Create(value);
            this.hub.Clients.All.SendAsync("WeaponCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Weapon value)
        {
            wLogic.Update(value);
            this.hub.Clients.All.SendAsync("WeaponUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var wToDelete = wLogic.Read(id);
            wLogic.Delete(id);
            this.hub.Clients.All.SendAsync("WeaponDeleted", wToDelete);
        }

        [HttpGet("HighestDMGWeaponUser")]
        public IEnumerable<string> HighestDMGWeaponUser()
        {
            return wLogic.HighestDMGWeaponUser();
        }

        [HttpGet("WeaponAverageDMGByCharacterName/{name}")]
        public double WeaponAverageDMGByCharacterName(string name)
        {
            return wLogic.WeaponAverageDMGByCharacterName(name);
        }

        [HttpGet("WeaponStatistics")]
        public IEnumerable<KeyValuePair<double, int>> WeaponStatistics()
        {
            return wLogic.WeaponStatistics();
        }
    }
}
