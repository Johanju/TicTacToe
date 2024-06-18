using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Game
    {
        public int[,] board = {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }};
        public int round = 0;
        public int currentPlayer;
        public bool Gameover = false;
        public string winner = "";
        private Dictionary<int, User> Users = new Dictionary<int, User>();
        // Player with X-symbol is always 1 and O-symbol is -1
        private readonly Dictionary<int, string> Tokens = new Dictionary<int, string>{{ -1, "O" }, { 0, " " }, { 1, "X" }};

        public Game(int starter, User[] users)
        {
            currentPlayer = starter;
            Users = new Dictionary<int, User> { 
                { users[0].PlayerToken, users[0] }, 
                { users[1].PlayerToken, users[1] }};
        }

        public void PrintBoard()
        {
            string b = "";
            for (int i = 0; i < 2; i++)
            {
                b += string.Format(" {0} | {1} | {2}\n", Tokens[board[i, 0]], Tokens[board[i, 1]], Tokens[board[i, 2]]);
                b += "-----------\n";
            }
            b += string.Format(" {0} | {1} | {2}", Tokens[board[2, 0]], Tokens[board[2, 1]], Tokens[board[2, 2]]);
            Console.WriteLine(b);
        }

        public bool CheckWin(int col, int row) // Sloppy Win checker, only checks if the lastest player won
        {
            int tempval = currentPlayer * 3;
            int colsum = 0;
            int rowsum = 0;
            int diagsum = 0;
            int rdiagsum = 0; // reverse diag
            for (int i = -2; i < 3; i++)
            {
                if (ValidTile(col + i, row))
                {
                    colsum += board[col + i, row];
                }
                if (ValidTile(col, row + i))
                {
                    rowsum += board[col, row + i];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                diagsum += board[i, i];
                rdiagsum += board[i, 2 - i];
            }
            if (colsum == tempval || rowsum == tempval || diagsum == tempval || rdiagsum == tempval)
            {
                winner = Tokens[currentPlayer];
                Gameover = true;
                return true;
            }
            return false;
        }

        public void ChangeTurn()
        {
            currentPlayer = -currentPlayer;
            /*
            if (currentPlayer == -1) 
            {
                currentPlayer = 1;
                return;
            }
            currentPlayer = -1;
            */
        }

        public bool ValidTile(int col, int row)
        {
            if (3 > col & col > -1 & 3 > row & row > -1)
            {
                return true;
            }
            return false; 
        }

        public bool Move(int col, int row)
        {
            if (board[col, row] != 0)
            {
                return false;
            }
            board[col, row] = currentPlayer;
            round += 1;
            return true;
        }

        public void Restart()
        {
            board = new int[3, 3]{
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }};
            winner = "";
            Gameover = false;
            currentPlayer = -currentPlayer;
            round = 0;
        }

        public void Play()
        {
            while (!Gameover)
            {
                Console.Clear();
                Console.WriteLine(string.Format("Player {0} turn : Round {1}", Tokens[currentPlayer], round));
                PrintBoard();
                int[] move = Users[currentPlayer].GetMove(round, board);
                Move(move[0], move[1]);
                if (CheckWin(move[0], move[1]))
                {
                    Console.Clear();
                    Console.WriteLine(string.Format("Player {0} Won!", Tokens[currentPlayer]));
                    PrintBoard();
                    break;
                }
                else if (round == 9)
                {
                    Console.Clear();
                    Console.WriteLine("Draw!");
                    PrintBoard();
                    break;
                }
                ChangeTurn();
            }
        }
    }
}
