using DealeronTest.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DealeronTest.Objects.Tests
{
    [TestClass()]
    public class RoverTests
    {
        [TestMethod()]
        public void Create_Valid_Rover()
        {
            //arrange
            var originX = 1;
            var originY = 2;
            var originDirectionLower = "n";
            var originDirectionUpper = "N";
            var originLowerString = $"{originX} {originY} {originDirectionLower}";
            var originUpperStrng = $"{originX} {originY} {originDirectionUpper}";
            var grid = new Grid("2 2");

            //act
            var roverLower = new Rover(originLowerString, grid);
            var roverUpper = new Rover(originUpperStrng, grid);

            //assert
            Assert.AreEqual(originX, roverLower.GetLocation()[0, 0]);
            Assert.AreEqual(originY, roverLower.GetLocation()[0, 1]);
            Assert.AreEqual(originX, roverUpper.GetLocation()[0, 0]);
            Assert.AreEqual(originY, roverUpper.GetLocation()[0, 1]);
            Assert.AreEqual(Constants.Compass.N, roverUpper.GetDirection());
            Assert.AreEqual(Constants.Compass.N, roverLower.GetDirection());
        }

        [TestMethod()]
        public void Create_Invalid_Rover_Throw_Exceptions()
        {
            //arrange
            var validOriginX = 1;
            var validOriginY = 2;
            var validOriginDirection = "S";
            var invalidOriginDirection = "Z";
            var invalidOriginX = "A";
            var invalidOriginY = "B";
            var invalidOriginXNeg = -1;
            var invalidOriginYNeg = -2;
            var invalidOriginXMax = 10;
            var invalidOriginYMax = 11;
            var grid = new Grid("5 5");

            //act and assert
            Assert.ThrowsException<ArgumentNullException>(() => new Rover("", grid));
            Assert.ThrowsException<ArgumentNullException>(() => new Rover(null, grid));
            Assert.ThrowsException<ArgumentException>(() => new Rover($"{validOriginX} {validOriginY} {validOriginDirection} {validOriginX}", grid));
            Assert.ThrowsException<ArgumentException>(() => new Rover($"{validOriginX} {validOriginY} {invalidOriginDirection}", grid));
            Assert.ThrowsException<ArgumentException>(() => new Rover($"{validOriginX} {invalidOriginY} {validOriginDirection}", grid));
            Assert.ThrowsException<ArgumentException>(() => new Rover($"{invalidOriginX} {validOriginY} {validOriginDirection}", grid));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover($"{validOriginX} {invalidOriginYNeg} {validOriginDirection}", grid));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover($"{validOriginX} {invalidOriginYMax} {validOriginDirection}", grid));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover($"{invalidOriginXMax} {validOriginY} {validOriginDirection}", grid));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover($"{invalidOriginXNeg} {validOriginY} {validOriginDirection}", grid));
        }

        [TestMethod()]
        public void SetPath_Invalid_Paths_Throw_Exceptions()
        {
            //arrange
            var grid = new Grid("5 5");
            var rover = new Rover("0 0 N", grid);

            //act and assert
            Assert.ThrowsException<ArgumentNullException>(() => rover.SetPath(null));
            Assert.ThrowsException<ArgumentNullException>(() => rover.SetPath(""));
            Assert.ThrowsException<ArgumentException>(() => rover.SetPath("ABC"));
            Assert.ThrowsException<ArgumentException>(() => rover.SetPath("abc"));
        }


        [TestMethod()]
        public void Rotate_Right()
        {
            //arrange
            var grid = new Grid("5 5");
            var rover = new Rover("0 0 N", grid);

            //act and assert
            rover.SetPath("R");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.E, rover.GetDirection());

            rover.SetPath("R");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.S, rover.GetDirection());

            rover.SetPath("R");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.W, rover.GetDirection());

            rover.SetPath("R");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.N, rover.GetDirection());
        }

        [TestMethod()]
        public void Rotate_Left()
        {
            //arrange
            var grid = new Grid("5 5");
            var rover = new Rover("0 0 N", grid);

            //act and assert
            rover.SetPath("L");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.W, rover.GetDirection());

            rover.SetPath("L");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.S, rover.GetDirection());

            rover.SetPath("L");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.E, rover.GetDirection());

            rover.SetPath("L");
            rover.Run(grid);

            Assert.AreEqual(Constants.Compass.N, rover.GetDirection());
        }

        [TestMethod()]
        public void Move_Valid()
        {
            //arrange
            var grid = new Grid("5 5");
            var rover = new Rover("0 0 N", grid);
            rover.SetPath("M");

            //act
            rover.Run(grid);

            //Assert
            Assert.AreEqual(0, rover.GetLocation()[0, 0]);
            Assert.AreEqual(1, rover.GetLocation()[0, 1]);
            Assert.AreEqual(Constants.Compass.N, rover.GetDirection());
        }

        [TestMethod()]
        public void Move_Invalid_Throw_Exception()
        {
            //arrange
            var grid = new Grid("5 5");
            var rover1 = new Rover("5 5 N", grid);
            var rover2 = new Rover("5 5 E", grid);
            var rover3 = new Rover("0 0 W", grid);
            var rover4 = new Rover("0 0 S", grid);
            rover1.SetPath("M");
            rover2.SetPath("M");
            rover3.SetPath("M");
            rover4.SetPath("M");

            //act and assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => rover1.Run(grid));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => rover2.Run(grid));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => rover3.Run(grid));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => rover4.Run(grid));
        }

        [TestMethod()]
        public void Full_Path()
        {
            //arrange
            var grid = new Grid("5 5");
            var rover1 = new Rover("1 2 N", grid);
            rover1.SetPath("LMLMLMLMM");
            var rover2 = new Rover("3 3 E", grid);
            rover2.SetPath("MMRMMRMRRM");

            //act
            rover1.Run(grid);
            rover2.Run(grid);

            //Assert
            Assert.AreEqual(1, rover1.GetLocation()[0, 0]);
            Assert.AreEqual(3, rover1.GetLocation()[0, 1]);
            Assert.AreEqual(Constants.Compass.N, rover1.GetDirection());
            Assert.AreEqual(5, rover2.GetLocation()[0, 0]);
            Assert.AreEqual(1, rover2.GetLocation()[0, 1]);
            Assert.AreEqual(Constants.Compass.E, rover2.GetDirection());
        }
    }
}