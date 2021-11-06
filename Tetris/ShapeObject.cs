namespace Tetris
{
    using System;

    public class ShapeObject
    {
        public int[,] Shape { get; private set; }

        public int LocationX { get; set; }

        public int ShapeWidth { get; private set; }

        public int ShapeHeight { get; private set; }
        public int LocationY { get; set; }

        public RotationStatus RotationStatus { get; private set; }

        public ShapeObject(int[,] shape, int locationX)
        {
            Shape = shape;
            RotationStatus = RotationStatus.Degrees0;
            LocationX = locationX;
            LocationY = 0;
            CalculateShapeSize();
        }
        
        public void RotateShape(Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.Left:
                    if (RotationStatus == RotationStatus.Degrees0)
                        RotationStatus = RotationStatus.Degrees270;
                    else
                        RotationStatus--;
                    RotateLeft();
                    break;
                case Rotation.Right:
                    if (RotationStatus == RotationStatus.Degrees270)
                        RotationStatus = RotationStatus.Degrees0;
                    else
                        RotationStatus++;
                    RotateRight();
                    break;
                default:
                    throw new Exception("Wrong rotation");
            }
        }

        private void RotateRight()
        {
            int[,] newShape = new int[ShapeWidth, ShapeHeight];
            
            for(int i = 0; i < ShapeHeight; i++)
                for (int j = 0; j < ShapeWidth; j++)
                    newShape[j, ShapeHeight - i - 1] = Shape[i, j];

            Shape = newShape;
            CalculateShapeSize();
        }

        private void RotateLeft()
        {
            int[,] newShape = new int[ShapeWidth, ShapeHeight];
            
            for(int i = 0; i < ShapeHeight; i++)
            for (int j = 0; j < ShapeWidth; j++)
                newShape[ShapeWidth - j - 1, i] = Shape[i, j];

            Shape = newShape;
            CalculateShapeSize();
        }

        private void CalculateShapeSize()
        {
            ShapeHeight = Shape.GetLength(0);
            ShapeWidth = Shape.Length / ShapeHeight;
        }
    }
}