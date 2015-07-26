using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WayOfLifeWPF.Models;

namespace WayOfLifeWPF.ViewModels
{
    public class GameOfLifeViewModel
    {
        public Thread GameThread { get; private set; }
        public bool Running { get; set; }

        private World World { get; set; }
        private World PreviousWorld { get; set; }
        private TaskFactory TaskFactory { get; set; }
        
        public GameOfLifeViewModel(World world) 
        {
            TaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());

            World = world;
            PreviousWorld = new World(world.XRows, world.YRows)
            {
                Grid2DArray = world.CopyGrid2DArray(),
                Grid = world.Grid
            };

            GameThread = new Thread(GameOfLife);
        }

        public void Start()
        {
            Running = true;
            GameThread.Start();
        }

        private void GameOfLife()
        {
            while (Running)
            {
                for (int i = 0; i < PreviousWorld.Grid2DArray.GetLength(0); i++)
                {
                    for (int j = 0; j < PreviousWorld.Grid2DArray.GetLength(1); j++)
                    {
                        int aliveNeighbours = CheckAliveNeighbours(i, j);

                        // Death cell becomes alive
                        if (!PreviousWorld.Grid2DArray[i, j] && aliveNeighbours == 3)
                        {
                            PreviousWorld.Grid2DArray[i, j] = true;
                        }
                        // Alive cell dies
                        else if (PreviousWorld.Grid2DArray[i, j] && (aliveNeighbours > 3 || aliveNeighbours < 2))
                        {
                            PreviousWorld.Grid2DArray[i, j] = false;
                        }
                    }
                }

                TaskFactory.StartNew(SetWorldVisuals); Thread.Sleep(300);
            }
        }

        private void SetWorldVisuals()
        {
            int count = 0;

            for (int i = 0; i < World.Grid2DArray.GetLength(0); i++)
            {
                for (int j = 0; j < World.Grid2DArray.GetLength(1); j++)
                {
                    World.Grid[count].Visibility = PreviousWorld.Grid2DArray[i, j] ? 
                        Visibility.Visible : 
                        Visibility.Hidden;

                    World.Grid2DArray[i, j] = PreviousWorld.Grid2DArray[i, j];

                    count++;
                }
            }

            PreviousWorld = new World(World.XRows, World.YRows)
            {
                Grid2DArray = World.CopyGrid2DArray(),
                Grid = World.Grid
            };
        }

        private int CheckAliveNeighbours(int xPos, int yPos)
        {
            int aliveCells = 0;

            for (int i = 0; i < 9; i++)
            {
                int x = (i % 3) - 1;
                int y = (i / 3) - 1;

                if (x == 0 && y == 0) continue;

                int xx = xPos + x;
                int yy = yPos + y;

                if (xx < 0 || yy < 0) continue;

                if (xx < World.XRows && yy < World.YRows && World.Grid2DArray[xx, yy])
                {
                    aliveCells++;
                }
            }

            return aliveCells;
        }
    }
}