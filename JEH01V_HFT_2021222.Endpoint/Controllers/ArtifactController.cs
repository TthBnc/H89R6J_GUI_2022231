using JEH01V_HFT_2021222.Endpoint.Services;
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
    public class ArtifactController : ControllerBase
    {
        IArtifactLogic aLogic;
        IHubContext<SignalRHub> hub;

        public ArtifactController(IArtifactLogic aLogic, IHubContext<SignalRHub> hub)
        {
            this.aLogic = aLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Artifact> ReadAll()
        {
            return aLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Artifact Read(int id)
        {
            return aLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Artifact value)
        {
            aLogic.Create(value);
            this.hub.Clients.All.SendAsync("ArtifactCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Artifact value)
        {
            aLogic.Update(value);
            this.hub.Clients.All.SendAsync("ArtifactUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var aToDelete = aLogic.Read(id);
            aLogic.Delete(id);
            this.hub.Clients.All.SendAsync("ArtifactDeleted", aToDelete);
        }

        [HttpGet("HighestCostArtifactUser")]
        public IEnumerable<string> HighestCostArtifactUser()
        {
            return aLogic.HighestCostArtifactUser();
        }

        [HttpGet("ArtifactAverageCostByCharacterName/{name}")]
        public double ArtifactAverageCostByCharacterName(string name)
        {
            return aLogic.ArtifactAverageCostByCharacterName(name);
        }

        [HttpGet("ArtifactStatistics")]
        public IEnumerable<KeyValuePair<double, int>> ArtifactStatistics()
        {
            return aLogic.ArtifactStatistics();
        }
    }
}
