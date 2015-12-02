using System.Collections.Generic;
using System.Linq;

namespace FFCG.RSPLS.DeathMatch.Domain
{
    internal class Round
    {
        private readonly IList<Move> _moves;

        public RoundOutcome[] Outcome { get; private set; }

        public int Number { get; private set; }
        public Round(Game game, int number)
        {
            Game = game;            
            _moves = new List<Move>();
            Status = RoundStatus.Started;
            Number = number;
        }

        public RoundStatus Status { get; private set; }

        public void MakeMove(Player player, MoveType moveType)
        {
            if (Status != RoundStatus.Started) return;

            var existingMove = _moves.FirstOrDefault(move => move.ByPlayer == player);
            if (existingMove != null)
            {
                _moves.Remove(existingMove);
            }

            _moves.Add(new Move(player, moveType));
        }

        public void Abandon(Player player)
        {
            if( Status != RoundStatus.Started) return;

            var existingMove = _moves.FirstOrDefault(move => move.ByPlayer == player);
            if (existingMove != null)
            {
                _moves.Remove(existingMove);
            }
        }

        public void Finish()
        {
            Game.Log?.WriteLine($"Finished round {Number}");
            Status = RoundStatus.Finished;

            var outcome = Game.Players.ToDictionary(player => player, player => new RoundOutcome(player));

            var moves = _moves.ToArray();

            for (var i = 0; i < moves.Length - 1; i++)
            {
                var moveA = moves[i];
                for (var j = i + 1; j < moves.Length; j++)
                {
                    var moveB = moves[j];

                    var winningMove = Game.Rules.GetWinningRule(moveA.Type, moveB.Type);

                    if (winningMove != null)
                    {
                        if (winningMove.Move1 == moveA.Type)
                        {
                            Game.Log?.WriteLine($"Player {moveA.ByPlayer.Name}'s {moveA.Type} {winningMove.Outcome} {moveB.ByPlayer.Name}'s {moveB.Type}");
                            outcome[moveA.ByPlayer].Frags++;
                            outcome[moveB.ByPlayer].Deaths++;
                        }
                    }
                    else
                    {
                        winningMove = Game.Rules.GetWinningRule(moveB.Type, moveA.Type);
                        if (winningMove != null)
                        {
                            Game.Log?.WriteLine($"Player {moveB.ByPlayer.Name}'s {moveB.Type} {winningMove.Outcome} {moveA.ByPlayer.Name}'s {moveA.Type}");
                            outcome[moveB.ByPlayer].Frags++;
                            outcome[moveA.ByPlayer].Deaths++;
                        }
                    }
                }
                
            }
            Outcome = outcome.Select(kv => kv.Value).ToArray();
        }

        public Game Game { get; private set; }
    }

    public class RoundOutcome
    {
        public RoundOutcome(Player player)
        {
            Player = player;
            Frags = 0;
            Deaths = 0;
        }

        public Player Player { get; set; }
        public int Frags { get; set; }
        public int Deaths { get; set; }
    }

    public enum RoundStatus
    {
        Started = 0,
        Finished = 1,
    }    
}