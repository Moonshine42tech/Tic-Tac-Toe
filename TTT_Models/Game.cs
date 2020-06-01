using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public bool SingleOrMultiPlayer { get; set; }       // What type of game is selected
        public int AmountOfGames { get; set; }              
        public bool Player1Turn { get; set; } = true;       // Used to evaluate what players turn it is.

        /// <summary>
        /// Changes the turn between the players using a bool 'Player1Turn'
        /// </summary>
        public void TurnChange()
        {
            try
            {
                if (Player1Turn == true)
                {
                    Player1Turn = false;
                }
                else
                {
                    Player1Turn = true;
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
