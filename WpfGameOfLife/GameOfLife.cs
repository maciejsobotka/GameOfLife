using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfGameOfLife
{
    public class GameOfLife
    {
        private Random random = new Random();
        private int[,] World = new int[22, 22];        
        private int[,] World_Future = new int[22, 22];
        
        public void GenerateWorld()
        {
            for (int i = 1; i < 21; i++)
                for (int j = 1; j < 21; j++)
                {
                    World[i, j] = random.Next(0, 2);
                }
            World_Future = World;
        }
        public bool IsCellAlive(int i, int j)
        {
            if (World[i, j] == 0)
                return false;
            if (World[i, j] == 1)
                return true;
            return false;
        }
        public void Iteration()
        {
            int a;
            World_Future = World;
            for (int i = 1; i < 21; i++)
                for (int j = 1; j < 21; j++)
                {
                    a = 0;
                    a = World[i - 1, j - 1] + World[i, j - 1] + World[i + 1, j - 1] + World[i - 1, j]
                        + World[i + 1, j] + World[i - 1, j + 1] + World[i, j + 1] + World[i + 1, j + 1];
                    CellUpdate(i, j, a);
                }
            World = World_Future;
        }
        private void CellUpdate(int i, int j, int a)
        {
            if (a < 2 || a > 3)
                World_Future[i, j] = 0;
            if (a == 3)
                if (World[i, j] == 0)
                    World_Future[i, j] = 1;
        }
    }
}
