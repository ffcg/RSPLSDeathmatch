using FFCG.RSPLS.DeathMatch.Domain;

namespace FFCG.RSPLSDeathMatch.Server.Models
{
    public static class ApiModelExtensions
    {
        public static FFCG.RSPLS.DeathMatch.ApiModel.Game AsApiModel(this Game game)
        {
            return new FFCG.RSPLS.DeathMatch.ApiModel.Game()
            {
                Name = game.Name,
                Round = game.GameStatus()[0]?.RoundNumber ?? 0,
                Players = game.Players.Count,
            };
        }
    }
}