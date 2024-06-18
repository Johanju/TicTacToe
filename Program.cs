namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game CurrentGame = ProcessSettings(GetSettings());
            while (true)
            {
                CurrentGame.Play();
                Console.WriteLine("1: Restart\n2: Quit");
                if(ProcessInput(2) == 1)
                {
                    CurrentGame.Restart();
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }





            List<int> GetSettings() // 
            {
                List<int> inputs = new List<int>();
                Console.WriteLine("Welcome!\nChoose your desired settings");
                Console.WriteLine(@"1: Play versus Bot
2: Bot versus Bot");
                inputs.Add(ProcessInput(2));
                Console.Clear();
                return inputs;
            }

            int ProcessInput(int NumberOfInputs)
            {
                int choice;
                do
                {
                    Console.Write(": ");
                    string val = Console.ReadLine();
                    if (val.Length < 1 || val.Length > NumberOfInputs)
                    {
                        Console.WriteLine("You have to choose a number on the menu");
                    }
                    else
                    {
                        if (int.TryParse(val, out choice))
                        {
                            if (0 < choice && choice <= NumberOfInputs)
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("It has to be a number");
                        }
                    }
                }
                while (true);
                return choice;
            }

            Game ProcessSettings(List<int> inputs)
            {
                Random rng = new Random();
                int[] tokens = [-1, 1];
                int UserToken = tokens[rng.Next(2)];
                User[] users;
                if (inputs[0] == 1)
                {
                    users = [new Player(UserToken), new MinMax(-UserToken)];
                }   
                else
                {
                    users = [new MinMax(UserToken), new MinMax(-UserToken)];
                }
                return new Game(tokens[rng.Next(2)], users);
            }
        }
    }
}
