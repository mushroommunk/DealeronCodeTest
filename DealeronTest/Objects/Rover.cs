using DealeronTest.Common;
using DealeronTest.Localizations;
using Elsheimy.Components.Linears;
using System.Text.RegularExpressions;

namespace DealeronTest.Objects
{
    public class Rover
    {
        private Matrix Location;
        private Matrix Direction;
        private string? Path;

        public Rover(string origin, Grid grid)
        {
            if (string.IsNullOrWhiteSpace(origin))
            {
                throw new ArgumentNullException(nameof(origin));
            }

            var originCoordsAndDirection = origin.InputStringToArgs();

            if (originCoordsAndDirection?.Length != 3)
            {
                throw new ArgumentException(Prompts.RoverLocationAndDirectionError);
            }

            if (int.TryParse(originCoordsAndDirection[0], out var xCoord))
            {

                if (int.TryParse(originCoordsAndDirection[1], out var yCoord))
                {
                    TestInBounds(xCoord, yCoord, grid);

                    if (Enum.TryParse(originCoordsAndDirection[2].ToUpper(), out Constants.Compass direction))
                    {
                        Location = new Matrix(new double[,] { { xCoord, yCoord } });
                        Direction = Constants.Directions[direction];
                    }
                    else
                    {
                        throw new ArgumentException(Prompts.RoverDirectionError);
                    }
                }
                else
                {
                    throw new ArgumentException(Prompts.RoverYError);
                }
            }
            else
            {
                throw new ArgumentException(Prompts.RoverXError);
            }
        }

        public Matrix GetLocation()
        {
            return Location;
        }

        public Constants.Compass GetDirection()
        {
            var direction = Constants.Directions.Where(x => x.Value == Direction).FirstOrDefault().Key;
            return direction;
        }

        public void SetPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var normalizedPath = path.ToUpper();

            if (Regex.IsMatch(normalizedPath, "[LMR]+"))
            {
                Path = normalizedPath;
            }
            else
            {
                throw new ArgumentException(Prompts.InvalidPathInstruction);
            }
        }

        private void Rotate(Matrix rotation)
        {
            Direction = Direction.Multiply(rotation);
        }

        private void Move(Grid grid)
        {
            var newLocation = Location.Add(Direction);
            TestInBounds(newLocation[0, 0], newLocation[0, 1], grid);
            Location = newLocation;
        }

        private void TestInBounds(double xCoord, double yCoord, Grid grid)
        {
            if (xCoord > grid.MaxX)
            {
                throw new ArgumentOutOfRangeException(Prompts.RoverXMaxError);
            }

            if (yCoord > grid.MaxY)
            {
                throw new ArgumentOutOfRangeException(Prompts.RoverYMaxError);
            }

            if (xCoord < 0)
            {
                throw new ArgumentOutOfRangeException(Prompts.RoverXMinError);
            }

            if (yCoord < 0)
            {
                throw new ArgumentOutOfRangeException(Prompts.RoverYMinError);
            }
        }

        public void Run(Grid grid)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                return;
            }

            foreach (char instruction in Path)
            {
                switch (instruction)
                {
                    case 'M':
                        Move(grid);
                        break;

                    case 'L':
                        Rotate(Constants.LeftRotationMatrix);
                        break;

                    case 'R':
                        Rotate(Constants.RightRotationMatrix);
                        break;

                    default:
                        throw new ArgumentException(Prompts.InvalidPathInstruction);
                }
            }

            Path = null;
        }
    }
}
