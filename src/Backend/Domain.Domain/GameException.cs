using System;

namespace FFCG.RSPLS.DeathMatch.Domain
{
    public class GameException : Exception
    {
        public GameException(string message)
            : base(message)
        {
        }
    }
}