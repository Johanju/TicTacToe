namespace TicTacToe
{
    public  class User
    {
        public int PlayerToken;
        public User(int token) 
        {
            PlayerToken = token;
        }
        
        public virtual int[] GetMove(int rounds, int[,] board)
        {
            return [-5, -5];
        }
    }
}
