using System.Text;
using System.Threading;

namespace Tetris
{
    using System;

    public class Game
    {
        private const int Height = 20;
        private const int Width = 10;

        public readonly int[,] _display = new int[Height, Width];
        public bool CanPlay = true;

        public ShapeObject ShapeObject;
        public int Points;

        private readonly System.Timers.Timer _blockTimer = new(500);
        private readonly System.Timers.Timer _refreshTimer = new(75);

        private void InitBoard()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int k = 0; k < Width; k++)
                {
                    _display[i, k] = 0;
                }

                Points = 0;
            }

            Console.CursorVisible = false;
        }

        private void CheckFullRows()
        {
            for (int i = Height - 1; i >= 0; i--)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (j + 1 == Width)
                    {
                        Points++;
                        MakeRowClear(i);
                        CopyRowToRowBelow(i);
                    }

                    if (_display[i, j] != 0)
                        continue;
                    break;
                }
            }
        }




        private void MakeRowClear(int row)
        {
            for (int i = 0; i < Width; i++)
            {
                _display[row, i] = 0;
            }
        }

        private void CopyRowToRowBelow(int row)
        {
            while (row > 0)
            {
                for (int i = 0; i < Width; i++)
                {
                    _display[row, i] = _display[row - 1, i];
                }

                row--;
            }
            MakeRowClear(0);
        }


        private void Display()
        {
            int[,] displayTab = new int[Height, Width];

            for (int i = 0; i < Width; i++)
            {
                for (int k = 0; k < Height; k++)
                {
                    displayTab[k, i] = _display[k, i];
                }
            }

            for (int i = 0; i < ShapeObject.ShapeHeight; i++)
            {
                for (int j = 0; j < ShapeObject.ShapeWidth; j++)
                {
                    if (ShapeObject.LocationY + i >= Height || ShapeObject.LocationX + j >= Width)
                        break;
                    if(_display[ShapeObject.LocationY + i, ShapeObject.LocationX + j] == 0)
                        displayTab[ShapeObject.LocationY + i, ShapeObject.LocationX + j] = ShapeObject.Shape[i, j];
                }
            }

            StringBuilder sb = new();
            string row = "";
            for (int i = 0; i < Height; i++)
            {
                row += "[";
                for (int k = 0; k < Width; k++)
                    row += " " + (displayTab[i, k] == 1 ? "1" : " ");
                row += "]\n";
                sb.Append(row);
                row = "";
            }
            sb.Append("Score: " + Points);
            Console.Write(sb);
        }

        public void StartGame()
        {
            InitBoard();
            InitShape();

            
            _blockTimer.Elapsed += delegate
            {
                MoveFigureDownAndCheckFullRows();
            };

            
            _blockTimer.Elapsed += delegate
            {
                Console.CursorTop = 0;
                Console.CursorLeft = 0;
                Display();
            };

            _refreshTimer.Start();
            _blockTimer.Start();
        }

        public void StopGame()
        {
            _blockTimer.Stop();
            _refreshTimer.Stop();
            Thread.Sleep(1000);
            
        }

        public bool IsFigureAbleToMoveRight()
        {
            if (ShapeObject.LocationX + ShapeObject.ShapeWidth >= Width)
                return false;
            for (int i = 0; i < ShapeObject.ShapeHeight; i++)
            {
                if (ShapeObject.LocationX + ShapeObject.ShapeWidth == Width)
                    return false;
                if(ShapeObject.LocationX + ShapeObject.ShapeWidth < Width)
                    if (_display[ShapeObject.LocationY + i, ShapeObject.LocationX + ShapeObject.ShapeWidth] == 1)
                        return false;
            }

            return true;
        }

        public bool IsFigureAbleToMoveLeft()
        {
            if (ShapeObject.LocationX <= 0)
                return false;
            for (int i = 0; i < ShapeObject.ShapeHeight; i++)
            {
                if (ShapeObject.LocationX == 0)
                    return false;
                if (_display[ShapeObject.LocationY + i, ShapeObject.LocationX - 1] == 1)
                    return false;
            }

            return true;
        }

        public void MoveRight()
        {
            
            ShapeObject.LocationX++;
        }

        public void MoveLeft()
        {
            
            ShapeObject.LocationX--;
        }

        public void MoveFigureDownAndCheckFullRows()
        {
            if (IsFigureAbleToMoveDown())
            {
                ShapeObject.LocationY++;
                CheckFullRows();
            }
            else
            {
                if (CanSpawnNewFigure())
                {
                    DockFigure();
                    InitShape();
                }
                else
                {
                    CanPlay = false;
                    StopGame();
                }
            }
        }

        public void DockFigure()
        {
            for (int i = 0; i < ShapeObject.ShapeHeight; i++)
            {
                for (int j = 0; j < ShapeObject.ShapeWidth; j++)
                {
                    if (ShapeObject.LocationY + i >= Height || ShapeObject.LocationX + j >= Width)
                        break;
                    if (_display[ShapeObject.LocationY + i, ShapeObject.LocationX + j] == 0)
                        _display[ShapeObject.LocationY + i, ShapeObject.LocationX + j] = ShapeObject.Shape[i, j];
                }
            }

        }

        private bool CanSpawnNewFigure()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 3; k++)
                    if (_display[i, Width / 2 + k] == 1)
                        return false;
            }
            return true;
        }

        public bool IsFigureAbleToRotate()
        {
            if (ShapeObject.ShapeHeight + ShapeObject.LocationX > Width)
                return false;
            for (int i = 0; i < ShapeObject.ShapeHeight; i++)
            {
                for (int j = 0; j < ShapeObject.ShapeWidth; j++)
                {
                    if (_display[ShapeObject.LocationY + j, ShapeObject.LocationX + i] == 1)
                        return false;
                }
            }
            return true;
        }

        private bool IsFigureAbleToMoveDown()
        {
            for (int i = 0; i < ShapeObject.ShapeHeight; i++)
            {
                for (int j = 0; j < ShapeObject.ShapeWidth; j++)
                {
                    if (ShapeObject.LocationY + i + 1 >= Height)
                        return false;
                    if (_display[ShapeObject.LocationY + i + 1, ShapeObject.LocationX + j] == 0)
                        continue;
                    if (ShapeObject.Shape[i, j] == 0)
                        continue;

                    return false;
                }
            }

            return true;
        }

        public void Rotate(Rotation rotation)
        {
            ShapeObject.RotateShape(rotation);
        }

        private void InitShape()
        {
            int[,] shape = Shapes.GetRandomShape();

            ShapeObject = new ShapeObject(shape, Width / 2);

        }
    }
}