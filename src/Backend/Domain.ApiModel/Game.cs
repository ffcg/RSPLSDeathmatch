namespace FFCG.RSPLS.DeathMatch.ApiModel
{
    public class Game
    {
        public string Name { get; set; }
        public int Round { get; set; }

        public int Players { get; set; }
        public RoundOutcome Outcome { get; set; }
    }

    public class NewGame
    {
        public string Name { get; set; }

    }
}