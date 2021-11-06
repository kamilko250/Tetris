namespace Tetris
{
    using System;
    using System.Collections.Generic;

    public static class Shapes
    {
        private static readonly Random Random = new();
        public static  int[,] GetRandomShape() => ShapesList[Random.Next() % ShapesList.Count];

        private static readonly List<int[,]> ShapesList = new()
        {
            new [,]
            {
                {0, 1},
                {1, 1},
                {1, 0}
            },
            new [,]
            {
                {1},
                {1},
                {1},
                {1}
            },
            new [,]
            {
                {1, 0},
                {1, 1},
                {0, 1}
            },
            new [,]
            {
                {0, 1, 0},
                {1, 1, 1}
            },
            new [,]
            {
                {1, 1},
                {0, 1},
                {0, 1}
            },
            new [,]
            {
                {1, 1},
                {1, 0},
                {1, 0}
            }
        };
    }
}