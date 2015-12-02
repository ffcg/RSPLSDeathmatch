using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Tracing;
using FFCG.RSPLS.DeathMatch.Domain;
using FFCG.RSPLSDeathMatch.Server.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;

namespace FFCG.RSPLSDeathMatch.Server.Controllers
{
    [MobileAppController]
    public class GameController : ApiController
    {
        private static readonly IList<FFCG.RSPLS.DeathMatch.Domain.Game> Games;

        static GameController()
        {
            Games = new List<Game>
            {
                Game.StartGame("Apocoalypse!"),
                Game.StartGame("Ein, Zwei, Combat!"),
                Game.StartGame("War")
            };
        }

        // GET api/games
        [Route("api/games")]
        public JsonResult<FFCG.RSPLS.DeathMatch.ApiModel.Game[]> GetAll()
        {
            MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            ITraceWriter traceWriter = this.Configuration.Services.GetTraceWriter();

            var games = Games.Select(game => game.AsApiModel()).ToArray();

            traceWriter.Info($"GET Games {games.Length}");
            return Json<FFCG.RSPLS.DeathMatch.ApiModel.Game[]>(games);
        }

        // PUT api/games
        [HttpPut]
        [Route("api/games")]
        public IHttpActionResult Put([FromBody]FFCG.RSPLS.DeathMatch.ApiModel.NewGame game)
        {
            MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            ITraceWriter traceWriter = this.Configuration.Services.GetTraceWriter();

            var newGame = Game.StartGame(game.Name);
            Games.Add(newGame);

            traceWriter.Info($"PUT Games {game.Name}");

            return CreatedAtRoute("games", new { Name = game.Name }, newGame.AsApiModel());
        }


        [Route("api/games/{id}")]
        public JsonResult<FFCG.RSPLS.DeathMatch.ApiModel.Game> Get([FromUri]string id)
        {
            MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            ITraceWriter traceWriter = this.Configuration.Services.GetTraceWriter();

            var activeGame = Games
                .FirstOrDefault(game => game.Name.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                .AsApiModel();

            traceWriter.Info($"GET Games/{id} {activeGame}");
            return Json(activeGame);
        }

    }
}