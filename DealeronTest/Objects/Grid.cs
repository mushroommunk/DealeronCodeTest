using DealeronTest.Common;
using DealeronTest.Localizations;

namespace DealeronTest.Objects
{
    public class Grid
    {
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        public Grid() { }

        public Grid(int maxX, int maxY)
        {
            SetGridSize($"{maxX} {maxY}");
        }

        public Grid(string gridSize)
        {
            SetGridSize(gridSize);
        }

        public void SetGridSize(string gridSize)
        {

            var sizes = gridSize.InputStringToArgs();

            if (sizes?.Length != 2)
            {
                throw new ArgumentException(Prompts.GridBothMaxError);
            }

            if (int.TryParse(sizes[0], out var maxx))
            {
                if (maxx < 0)
                {
                    throw new ArgumentOutOfRangeException(Prompts.GridCoordNegative);
                }

                MaxX = maxx;
            }
            else
            {
                throw new ArgumentException(Prompts.GridMaxXError);
            }

            if (int.TryParse(sizes[1], out var maxy))
            {
                if (maxy < 0)
                {
                    throw new ArgumentOutOfRangeException(Prompts.GridCoordNegative);
                }

                MaxY = maxy;
            }
            else
            {
                throw new ArgumentException(Prompts.GridMaxYError);
            }
        }
    }
}
