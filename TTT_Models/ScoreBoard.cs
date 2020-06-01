using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class ScoreBoard
    {
        public int Win { get; set; }                    // Amount of Wins
        public int Tie { get; set; }                    // Amount of Ties
        public int Lose { get; set; }                   // Amount of Loses

        /// <summary>
        /// Updates the game score on the scoreboard:
        /// 1 = Win, 2 = Tie, 3 = Lose
        /// </summary>
        /// <param name="gameResult">The result of a single game</param>
        public void SetScore(int gameResult)
        {
            try
            {
                switch (gameResult)
                {
                    case 1:
                        Win = (Win + 1);
                        break;

                    case 2:
                        Tie = (Tie + 1);
                        break;

                    case 3:
                        Lose = (Lose + 1);
                        break;

                    default:
                        break;
                }
            }
            catch (NullReferenceException e)
            {
                // Catches a null exeption if the 'gameResult' is null
            }
            catch (Exception e)
            {
                // Catches most unacountet exeptions
            }

        }
    }
}
