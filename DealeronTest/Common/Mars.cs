using DealeronTest.Localizations;
using DealeronTest.Objects;

namespace DealeronTest.Common
{
    public static class Mars
    {
        private static bool GridAssigned = false;

        private static bool IsRoverOrigin = true;

        private static Grid Grid = new Grid();

        private static List<Rover> Rovers = new List<Rover> { };

        public static void ProcessInput(string input)
        {
            if (!GridAssigned)
            {
                try
                {
                    Grid.SetGridSize(input);
                    GridAssigned = true;
                    Console.Write(Prompts.OriginInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(Prompts.PressEnter);
                    Console.ReadLine();
                }
            }
            else
            {
                if (IsRoverOrigin)
                {
                    try
                    {
                        Rovers.Add(new Rover(input, Grid));
                        IsRoverOrigin = !IsRoverOrigin;
                        Console.Write(Prompts.Path);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(Prompts.PressEnter);
                        Console.ReadLine();
                        Console.Write(Prompts.OriginInput);
                    }
                }
                else
                {
                    try
                    {
                        Rovers.Last().SetPath(input);
                        IsRoverOrigin = !IsRoverOrigin;
                        Console.Write(Prompts.OriginInput);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(Prompts.PressEnter);
                        Console.ReadLine();
                        Console.Write(Prompts.Path);
                    }
                }
            }
        }

        public static void Reset()
        {
            GridAssigned = false;
            IsRoverOrigin = true;
            Grid = new Grid();
            Rovers = new List<Rover>();
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.WriteLine(Prompts.Menu);
        }

        public static void Run()
        {
            foreach (Rover rover in Rovers)
            {
                try
                {
                    rover.Run(Grid);
                    Console.WriteLine(string.Format(Prompts.RoverResponse, rover.GetLocation(), rover.GetDirection()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(Prompts.PressEnter);
                    Console.WriteLine(Prompts.SafetyRestart);
                    Console.ReadLine();
                    Reset();
                }
            }
        }

        internal static bool GetGridAssigned()
        {
            return GridAssigned;
        }
    }
}
