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

        public CharacterController(ICharacterLogic cLogic)
        {
            this.cLogic = cLogic;
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
        }

        [HttpPut]
        public void Update([FromBody] Character value)
        {
            cLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cLogic.Delete(id);
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
