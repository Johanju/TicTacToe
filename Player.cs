namespace TicTacToe
{
    public class Player : User
    {
        public Player(int token) : base(token)
        {
            PlayerToken = token;
        }

        public override int[] GetMove(int rounds, int[,] board)
        {
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
                        if (board[col, row] == 0)
                        {
                            return [col, row];
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
        }
    }
}
