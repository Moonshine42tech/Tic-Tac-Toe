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
        /// 1 = X +1 Win, 2 = O +1 Win
        /// </summary>
        /// /// <param name="scoreboard">current scoreboard</param>
        /// <param name="gameResult">The result of a single game</param>
        public void SetScore(ScoreBoard scoreboard, int gameResult)
        {
            try
            {
                switch (gameResult)
                {
                    case 1:
                        scoreboard.X_Score = (scoreboard.X_Score + 1);
                        break;

                    case 2:
                        scoreboard.O_Score = (scoreboard.O_Score + 1);
                        break;

                    default:
                        break;
                }
            }
            catch (NullReferenceException)
            {
                // Catches a null exeption if the 'gameResult' is null
            }
            catch (Exception)
            {
                // Catches most unacountet exeptions
            }

        }


        /// <summary>
        /// Set all values in the scoreboard to 0
        /// </summary>
        /// <param name="scoreboard">current scoreboard</param>
        public void ResetScoreBoard(ScoreBoard scoreboard)
        {
            scoreboard.X_Score = 0;
            scoreboard.O_Score = 0;
        }

    }
}
