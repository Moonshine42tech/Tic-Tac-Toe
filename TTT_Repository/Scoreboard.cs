using System;
using System.Collections.Generic;
using System.Text;
using TTT_Models;

namespace TTT_Repository
{
    public class Scoreboard
    {

        /// <summary>
        /// Updates the game score on the scoreboard:
        /// 1 = Win, 2 = Tie, 3 = Lose
        /// </summary>
        /// /// <param name="scoreboard">current scoreboard</param>
        /// <param name="gameResult">The result of a single game</param>
        public void SetScore(ScoreBoardModel scoreboard, int gameResult)
        {
            try
            {
                switch (gameResult)
                {
                    case 1:
                        scoreboard.Win = (scoreboard.Win + 1);
                        break;

                    case 2:
                        scoreboard.Tie = (scoreboard.Tie + 1);
                        break;

                    case 3:
                        scoreboard.Lose = (scoreboard.Lose + 1);
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

        /// <summary>
        /// Set all values in the scoreboard to 0
        /// </summary>
        /// <param name="scoreboard">current scoreboard</param>
        public void ResetScoreBoard(ScoreBoardModel scoreboard)
        {
            scoreboard.Win = 0;
            scoreboard.Tie = 0;
            scoreboard.Lose = 0;
        }

    }
}
