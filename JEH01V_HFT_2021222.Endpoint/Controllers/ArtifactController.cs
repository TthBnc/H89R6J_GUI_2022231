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

        public ArtifactController(IArtifactLogic aLogic)
        {
            this.aLogic = aLogic;
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
        }

        [HttpPut]
        public void Update([FromBody] Artifact value)
        {
            aLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            aLogic.Delete(id);
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
