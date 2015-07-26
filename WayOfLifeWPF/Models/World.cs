using System.Windows.Shapes;

namespace WayOfLifeWPF.Models
{
    public class World
    {
        public Rectangle[] Grid { get; set; }
        public bool[,] Grid2DArray { get; set; }
        public int XRows { get; set; }
        public int YRows { get; set; }

        public World(int xRows, int yRows) 
        {
            Grid = new Rectangle[xRows * yRows];
            Grid2DArray = new bool[xRows, yRows];
            XRows = xRows;
            YRows = yRows;
        }

        public bool[,] CopyGrid2DArray()
        {
            var newGridArray = new bool[XRows, YRows];

            for (int i = 0; i < Grid2DArray.GetLength(0); i++)
            {
                for (int j = 0; j < Grid2DArray.GetLength(1); j++)
                {
                    newGridArray[i, j] = Grid2DArray[i, j];
                }
            }

            return newGridArray;
        }
    }
}