using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DealeronTest.Objects.Tests
{
    [TestClass()]
    public class GridTests
    {
        [TestMethod()]
        public void Set_Valid_Grid_Coords()
        {
            //arrange
            var maxX = 1;
            var maxY = 2;
            var gridCreationString = $"{maxX} {maxY}";
            var grid2 = new Grid();

            //act
            var grid1 = new Grid(maxX, maxY);
            grid2.SetGridSize(gridCreationString);
            var grid3 = new Grid(gridCreationString);

            //assert
            Assert.AreEqual(maxX, grid1.MaxX);
            Assert.AreEqual(maxY, grid1.MaxY);
            Assert.AreEqual(maxX, grid2.MaxX);
            Assert.AreEqual(maxY, grid2.MaxY);
            Assert.AreEqual(maxX, grid3.MaxX);
            Assert.AreEqual(maxY, grid3.MaxY);
        }

        [TestMethod()]
        public void Set_Invalid_Grid_Coords_Throws_Exceptions()
        {
            //arrange
            var validMaxX = 1;
            var validMaxY = 2;
            var invalidX = "A";
            var invalidY = "B";
            var invalidMaxXNeg = -1;
            var invalidMaxYNeg = -2;
            var grid = new Grid();

            //act and assert
            Assert.ThrowsException<ArgumentNullException>(() => grid.SetGridSize(null));
            Assert.ThrowsException<ArgumentNullException>(() => grid.SetGridSize(""));
            Assert.ThrowsException<ArgumentException>(() => grid.SetGridSize($"{validMaxX} {validMaxY} {validMaxY}"));
            Assert.ThrowsException<ArgumentException>(() => grid.SetGridSize($"{invalidX} {validMaxY}"));
            Assert.ThrowsException<ArgumentException>(() => grid.SetGridSize($"{validMaxX} {invalidY}"));
            Assert.ThrowsException<ArgumentException>(() => grid.SetGridSize($"{invalidX} {invalidY}"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grid.SetGridSize($"{validMaxX} {invalidMaxYNeg}"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grid.SetGridSize($"{invalidMaxXNeg} {validMaxY}"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => grid.SetGridSize($"{invalidMaxXNeg} {invalidMaxYNeg}"));
        }
    }
}