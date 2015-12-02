using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FFCG.RSPLS.DeathMatch.Domain
{
    public interface ILog
    {
        void Write(string text, LogCategory category = LogCategory.Info);
        void WriteLine(string text, LogCategory category = LogCategory.Info);
    }

    public enum LogCategory
    {
        Info,
        Warn,
        Error,
    }

    public class Game
    {
        private readonly int _roundlength;
        private readonly ILog _log;
        public IList<Player> Players { get; private set; }
        private Round _nextRound;
        private Round _lastRound;
        private readonly IEnumerable<Rule> _rules;
        private readonly long _startTime;
        private int _lastNumber = -1;

        /// <summary>
        /// Starts a new game with the players given and optionaly a best of count
        /// </summary>
        /// <param name="roundlength">The length of each round in seconds</param>
        /// <returns></returns>
        public static Game StartGame(string name, int roundlength = 10, ILog log = null)
        {
            var game = new Game(name, roundlength, log);
            return game;
        }

        private Game(string name, int roundlength, ILog log = null)
        {
            Name = name;
            _roundlength = roundlength;
            _log = log;
            Players = new Collection<Player>();
            _rules = Rule.DefaultRules;
            _startTime = Environment.TickCount;

            CheckRound();
        }

        internal ILog Log
        {
            get { return _log;}
        }

        private long Elapsed()
        {
            return Environment.TickCount - _startTime;
        }

        internal IEnumerable<Rule> Rules
        {
            get { return _rules; }
        }

        public string Name { get; private set; }

        internal Round NextRound 
        {
            get { return _nextRound; }
        }

        /// <summary>
        /// Makes a move for player. 
        /// Throws <see cref="GameException"/> if player 
        /// is not in the game, or player has already made a move this round
        /// </summary>
        /// <param name="player"></param>
        /// <param name="moveType"></param>
        public void MakeMove(Player player, MoveType moveType)
        {
            if (_nextRound == null)
            {
                _log?.WriteLine("No new round started", LogCategory.Error);
                throw new GameException("No new round started");
            }

            _log?.WriteLine($"Player {player.Name} moved {moveType}");
            _nextRound.MakeMove(player, moveType);
        }

        /// <summary>
        /// Player joins the game
        /// </summary>
        /// <param name="player"></param>
        public void Join(Player player)
        {
            _log?.WriteLine($"Player {player.Name} joined");

            Players.Add(player);
        }

        /// <summary>
        /// Player abandons the game
        /// </summary>
        /// <param name="player"></param>
        public void Abandon(Player player)
        {
            _log?.WriteLine($"Player {player.Name} abandoned game");
            Players.Remove(player);
        }

        private void CheckRound()
        {
            var number = (int) (Elapsed()/ (_roundlength * 1000));
            if (number != _lastNumber)
            {
                _log?.WriteLine($"Elapsed rounds {number - _lastNumber}");
                _lastNumber = number;
            }

            if (_nextRound == null)
            {
                _log?.WriteLine("Created first round");
                _nextRound = new Round(this, 0);
            }

            while (number > _nextRound.Number)
            {
                _log?.WriteLine($"Started new round {_nextRound.Number}");
                _nextRound.Finish();
                _lastRound = _nextRound;

                _nextRound = new Round(this, _lastRound.Number + 1);
            }
        }

        public RoundScore[] GameStatus()
        {
            CheckRound();
            return new[] {_lastRound, _nextRound}.Select(round => new RoundScore() {Outcome = round?.Outcome, RoundNumber = round?.Number??-1}).ToArray();
        }
        
    }

    public class RoundScore
    {
        public RoundOutcome[] Outcome;
        public int RoundNumber;
    }
}
