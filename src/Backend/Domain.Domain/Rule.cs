using System.Collections.Generic;
using System.Linq;

namespace FFCG.RSPLS.DeathMatch.Domain
{
    public class Rule
    {
        public static IEnumerable<Rule> DefaultRules => new Rule[]
        {
            new Rule(MoveType.Rock, MoveOutcomeType.Crushes, MoveType.Lizard),
            new Rule(MoveType.Lizard, MoveOutcomeType.Poisons, MoveType.Spock),
            new Rule(MoveType.Spock, MoveOutcomeType.Smashes, MoveType.Scissors),
            new Rule(MoveType.Scissors, MoveOutcomeType.Cuts, MoveType.Paper),
            new Rule(MoveType.Paper, MoveOutcomeType.Covers, MoveType.Rock),
            new Rule(MoveType.Rock, MoveOutcomeType.Crushes, MoveType.Scissors),
            new Rule(MoveType.Scissors, MoveOutcomeType.Decapitates, MoveType.Lizard),
            new Rule(MoveType.Lizard, MoveOutcomeType.Eats, MoveType.Paper),
            new Rule(MoveType.Paper, MoveOutcomeType.Disproves, MoveType.Spock),
            new Rule(MoveType.Spock, MoveOutcomeType.Vaporizes, MoveType.Rock)
        };

        public Rule(MoveType move1, MoveOutcomeType outcome, MoveType move2)
        {
            Move1 = move1;
            Outcome = outcome;
            Move2 = move2;
        }

        public MoveType Move1 { get; set; }
        public MoveType Move2 { get; set; }
        public MoveOutcomeType Outcome { get; set; }
    }

    public static class RuleExtensions
    {
        public static Rule GetWinningRule(this IEnumerable<Rule> rules, MoveType move1, MoveType move2)
        {
            return rules.FirstOrDefault(
                rule =>
                    (rule.Move1 == move1) &&
                    (rule.Move2 == move2));
        }
    }
}