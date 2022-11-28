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
    public class CharacterController : ControllerBase
    {
        ICharacterLogic cLogic;
        IHubContext<SignalRHub> hub;

        public CharacterController(ICharacterLogic cLogic, IHubContext<SignalRHub> hub)
        {
            this.cLogic = cLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Character> ReadAll()
        {
            return cLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Character Read(int id)
        {
            return cLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Character value)
        {
            cLogic.Create(value);
            this.hub.Clients.All.SendAsync("CharacterCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Character value)
        {
            cLogic.Update(value);
            this.hub.Clients.All.SendAsync("CharacterUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cToDelete = cLogic.Read(id);
            cLogic.Delete(id);
            this.hub.Clients.All.SendAsync("CharacterDeleted", cToDelete);
        }

        [HttpGet("WeaponsOfGivenElement/{element}")]
        public IEnumerable<Weapon> WeaponsOfGivenElement(string element)
        {
            var result = cLogic.WeaponsOfGivenElement(element);
            return result;
        }

        [HttpGet("ArtifactsOfGivenElement/{element}")]
        public IEnumerable<Artifact> ArtifactsOfGivenElement(string element)
        {
            return cLogic.ArtifactsOfGivenElement(element);
        }
    }
}
