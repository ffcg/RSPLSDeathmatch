using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace FFCG.RSPLS.DeathMatch.Domain.Test
{
    public class ConsoleLog : ILog
    {
        public void Write(string text, LogCategory category = LogCategory.Info)
        {
            Console.WriteLine($"{text}");
        }

        public void WriteLine(string text, LogCategory category = LogCategory.Info)
        {
            Console.WriteLine($"[{category}]\t{text}");
        }
    }

    public class Game_tests
    {
        [Test]
        public void Game_rocks_beats_scissor()
        {
            var game = Game.StartGame("Test game", 5);

            var fred = new Player("Fred");
            var bob = new Player("Bob");

            game.Join(fred);
            game.Join(bob);

            game.MakeMove(fred, MoveType.Rock);
            game.MakeMove(bob, MoveType.Scissors);

            var gameStatus = game.GameStatus();
            while (gameStatus[0].RoundNumber == -1)
            {
                Thread.Sleep(100);
                gameStatus = game.GameStatus();
                System.Diagnostics.Debug.Write("z");
            }

            var fredOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == fred);
            var bobOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == bob);

            fredOutcome.Frags.Should().Be(1);
            fredOutcome.Deaths.Should().Be(0);
            bobOutcome.Frags.Should().Be(0);
            bobOutcome.Deaths.Should().Be(1);
        }

        [Test]
        public void Game_rocks_beats_scissor_beats_paper()
        {
            var game = Game.StartGame("Test game", 5);

            var fred = new Player("Fred");
            var bob = new Player("Bob");
            var hank = new Player("Hank");

            game.Join(fred);
            game.Join(bob);
            game.Join(hank);

            game.MakeMove(fred, MoveType.Rock);
            game.MakeMove(bob, MoveType.Scissors);
            game.MakeMove(hank, MoveType.Paper);

            var gameStatus = game.GameStatus();
            while (gameStatus[0].RoundNumber == -1)
            {
                Thread.Sleep(100);
                gameStatus = game.GameStatus();
                System.Diagnostics.Debug.Write("z");
            }
            System.Diagnostics.Debug.WriteLine("...");
        

            var fredOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == fred);
            var bobOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == bob);
            var hankOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == hank);

            fredOutcome.Frags.Should().Be(1);
            fredOutcome.Deaths.Should().Be(1);
            bobOutcome.Frags.Should().Be(1);
            bobOutcome.Deaths.Should().Be(1);
            hankOutcome.Frags.Should().Be(1);
            bobOutcome.Deaths.Should().Be(1);
        }

        [Test]
        public void Game_rocks_beats_scissor_beats_paper_disproves_spock()
        {
            var game = Game.StartGame("Test game", 5, new ConsoleLog());

            var fred = new Player("Fred");
            var bob = new Player("Bob");
            var hank = new Player("Hank");
            var dante = new Player("Dante");

            game.Join(fred);
            game.Join(bob);
            game.Join(hank);
            game.Join(dante);

            game.MakeMove(fred, MoveType.Rock);
            game.MakeMove(bob, MoveType.Scissors);
            game.MakeMove(hank, MoveType.Paper);
            game.MakeMove(dante, MoveType.Spock);

            var gameStatus = game.GameStatus();
            while (gameStatus[0].RoundNumber == -1)
            {
                Thread.Sleep(100);
                gameStatus = game.GameStatus();
                System.Diagnostics.Debug.Write("z");
            }
            System.Diagnostics.Debug.WriteLine("...");


            var fredOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == fred);
            var bobOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == bob);
            var hankOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == hank);
            var danteOutcome = gameStatus[0].Outcome.FirstOrDefault(outcome => outcome.Player == dante);

            fredOutcome.Frags.Should().Be(1); // Crushes scissor
            fredOutcome.Deaths.Should().Be(2);  // Spock vaporizes, Paper covers
            bobOutcome.Frags.Should().Be(1); // Scissors cuts
            bobOutcome.Deaths.Should().Be(2); // Rock crushes, Spock smashes
            hankOutcome.Frags.Should().Be(2); // covers rock, disproves spock
            hankOutcome.Deaths.Should().Be(1);
            danteOutcome.Frags.Should().Be(2);
            danteOutcome.Deaths.Should().Be(1);
        }
    }
}
