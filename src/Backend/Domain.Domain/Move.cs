using System;

namespace FFCG.RSPLS.DeathMatch.Domain
{
    public class Move
    {
        public Move(Player player, MoveType type)
        {
            ByPlayer = player;
            Type = type;
            Performed = DateTime.Now;

        }
        public Player ByPlayer { get; private set; }
        public MoveType Type { get; private set; }
        public DateTime Performed { get; private set; }
    }
}