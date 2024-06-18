namespace TicTacToe
{
    public class MinMax : User
    {
        public MinMax(int token) : base(token) 
        { 
            PlayerToken = token;
        }
        public override int[] GetMove(int rounds, int[,] board) 
        {
            List<int> Evaluations = new List<int>();
            List<int[]> Moves = new List<int[]>();
            for (int col = 0; col < 3; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (board[col, row] == 0)
                    {
                        Moves.Add([col, row]);
                        board[col, row] = PlayerToken;
                        Evaluations.Add(MiniMax(rounds + 1, board, false, int.MinValue, int.MaxValue));
                        board[col, row] = 0;
                    }
                }
            }
            int BestMoveIndex = 0;
            for (int i = 0; i < Evaluations.Count; i++)
            {
                if (Evaluations[i] > Evaluations[BestMoveIndex])
                {
                    BestMoveIndex = i;
                }
                //Console.WriteLine(string.Format("Move: [{0}, {1}] has a Value of {2}", Moves[i][0], Moves[i][1], Evaluations[i]));
            }
            return Moves[BestMoveIndex];
        }
        public int MiniMax(int rounds, int[,] board, bool IsMaxing, int alpha, int beta) // Returns calculated move
        {
            int boardVal = Evaluate(rounds, board);
            if (rounds == 9 || boardVal != -5)
            {
                return boardVal;
            } // returns current value if game is over or rounds equals 9
            if (IsMaxing)
            {
                int maxVal = int.MinValue;
                int Valuated;
                for (int col = 0; col < 3; col++)
                {
                    for (int row = 0; row < 3; row++)
                    {
                        if (board[col, row] == 0)
                        {
                            board[col, row] = PlayerToken;
                            Valuated = MiniMax(rounds + 1, board, !IsMaxing, alpha, beta);
                            if (Valuated > maxVal)
                            {
                                maxVal = Valuated;
                                alpha = maxVal;
                            }
                            board[col, row] = 0;
                            if (beta <= alpha)
                            {
                                break;
                            }
                        }
                    }
                }
                return maxVal;
            }
            else
            {
                int minVal = int.MaxValue;
                int Valuated;
                for (int col = 0; col < 3; col++)
                {
                    for (int row = 0; row < 3; row++)
                    {
                        if (board[col, row] == 0)
                        {
                            board[col, row] = -PlayerToken;
                            Valuated = MiniMax(rounds + 1, board, !IsMaxing, alpha, beta);
                            if (Valuated < minVal)
                            {
                                minVal = Valuated;
                                beta = minVal;
                            }
                            board[col, row] = 0;
                            if (beta <= alpha)
                            {
                                break;
                            }
                        }
                    }
                }
                return minVal;
            }
        }

        public int Evaluate(int rounds, int[,] board) // Using magic square to check if someone has won and determine board value
        {
            int diagsum = 0;
            int rdiagsum = 0;
            int playersum = 3 * PlayerToken;
            for (int i = 0; i < 3; i++)
            {
                int sum = board[0, i] + board[1, i] + board[2, i]; // Checking columns
                if (playersum == sum)
                {
                    return 1;
                }
                else if ( -playersum == sum)
                {
                    return -1;
                }
                sum = board[i, 0] + board[i, 1] + board[i, 2]; // Checking rows
                if (playersum == sum)
                {
                    return 1;
                }
                else if (-playersum == sum)
                {
                    return -1;
                }
            }
            // Checking both diagonals
            for (int i = 0; i < 3; i++)
            {
                diagsum += board[i, i];
                rdiagsum += board[i, 2 - i];
            }
            if (playersum == diagsum || playersum == rdiagsum)
            {
                return 1;
            }
            else if (-playersum == diagsum || -playersum == rdiagsum)
            {
                return -1;
            }
            // All spots are filled and no one won
            if (rounds == 9)
            {
                return 0;
            }
            return -5; // returning -5 if game is not over
        }
    }
}
