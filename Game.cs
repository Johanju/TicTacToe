using System;
using System.Collections.Generic;
using System.Linq;
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
        // Player with X-symbol is always 1 and O-symbol is -1
        private Dictionary<int, string> Tokens = new Dictionary<int, string>{{ -1, "O" }, { 0, " " }, { 1, "X" }};

        public Game(int startPlayer)
        {
            currentPlayer = startPlayer;
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

        public bool CheckWin(int col, int row) // Sloppy Win checker
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
                return true;
            }
            return false;
        }

        public void ChangeTurn()
        {
            if (currentPlayer == -1) 
            {
                currentPlayer = 1;
                return;
            }
            currentPlayer = -1;
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
            //Console.WriteLine(string.Format("Trying tile: {0} | Index: [{1}, {2}] | IsValid: {3}", tile, col, row, ValidTile(col, row)));
            if (board[col, row] != 0)
            {
                return false;
            }
            board[col, row] = currentPlayer;
            round += 1;
            return true;
        }

        public void Start()
        {
            while (!Gameover)
            {
                Console.Clear();
                Console.WriteLine(string.Format("Player {0} turn", Tokens[currentPlayer]));
                PrintBoard();
                int tile;
                int col;
                int row;
                do
                {
                    Console.Write("Choose a tile: ");
                    string val = Console.ReadLine();
                    if (val != null && val.Length == 1)
                    {
                        try
                        {
                            tile = Convert.ToInt32(val);
                            tile -= 1;
                            col = tile / 3;
                            row = tile - (col * 3);
                            if (Move(col, row))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("You have to input a empty tile between 1 - 9");
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Inputed tile has to be a number");
                            throw;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You have to input a empty tile between 1 - 9");
                    }
                }
                while (true);
                if (round > 4)
                {
                    if (CheckWin(col, row))
                    {
                        Console.Clear();
                        PrintBoard();
                        Console.WriteLine(string.Format("Player {0} WON!", Tokens[currentPlayer]));
                        Gameover = true;
                    }
                }
                ChangeTurn();
            }
        }
    }
}
