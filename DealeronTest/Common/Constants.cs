using Elsheimy.Components.Linears;

namespace DealeronTest.Common
{
    public static class Constants
    {
        //Standard rotation matrices pre-calculated using 90 degrees
        // [ cos(90) -sin(90) ]
        // [ sin(90)  cos(90) ]
        public static readonly Matrix RightRotationMatrix = new Matrix(new double[,] { { 0, -1 }, { 1, 0 } });

        // [ cos(90) sin(90) ]
        // [ -sin(90)  cos(90) ]
        public static readonly Matrix LeftRotationMatrix = new Matrix(new double[,] { { 0, 1 }, { -1, 0 } });

        public static readonly Dictionary<Compass, Matrix> Directions = new Dictionary<Compass, Matrix> {
            {Compass.N, new Matrix(new double[,] { { 0.0, 1.0 } }) },
            {Compass.E, new Matrix(new double[,] { { 1.0, 0.0 } }) },
            {Compass.S, new Matrix(new double[,] { { 0.0, -1.0} }) },
            {Compass.W, new Matrix(new double[,] { { -1.0, 0.0} }) }
        };

        //Never Eat Soggy Wheaties
        public enum Compass
        {
            N,
            E,
            S,
            W
        }
    }
}
