namespace TestProject1
{
    using System;
    using Xunit;
    using Shouldly;
    using Tetris;

    public class RotationTest
    {
        [Fact]
        public void RotationStatusLeft()
        {
            Game game = new Game();
            game.StartGame();

            game.Rotate(Rotation.Left);
            RotationStatus status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees270);

            game.Rotate(Rotation.Left);
            status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees180);

            game.Rotate(Rotation.Left);
            status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees90);

            game.Rotate(Rotation.Left);
            status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees0);
        }
        
        [Fact]
        public void RotationStatusRight()
        {
            Game game = new Game();
            game.StartGame();

            game.Rotate(Rotation.Right);
            RotationStatus status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees90);

            game.Rotate(Rotation.Right);
            status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees180);

            game.Rotate(Rotation.Right);
            status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees270);

            game.Rotate(Rotation.Right);
            status = game.ShapeObject.RotationStatus;
            status.ShouldBe(RotationStatus.Degrees0);
        }

        [Fact]
        public void LeftRotateThunder()
        {
            ShapeObject shapeObject = new ShapeObject(new [,]
            {
                {0, 1},
                {1, 1},
                {1, 0}
            }, 0);
            
            shapeObject.RotateShape(Rotation.Left);
            
            shapeObject.Shape.ShouldBe(new [,]{
                {1, 1, 0},
                {0, 1, 1}
            });
        }
        [Fact]
        public void RightRotateThunder()
        {
            ShapeObject shapeObject = new ShapeObject(new [,]
            {
                {0, 1},
                {1, 1},
                {1, 0}
            }, 0);
            
            shapeObject.RotateShape(Rotation.Right);
            
            shapeObject.Shape.ShouldBe(new [,]{
                {1, 1, 0},
                {0, 1, 1}
            });
        }
        
        [Fact]
        public void LeftRotateHoe()
        {
            ShapeObject shapeObject = new ShapeObject(new [,]
            {
                {0, 1},
                {0, 1},
                {1, 1}
            }, 0);
            
            shapeObject.RotateShape(Rotation.Left);
            
            shapeObject.Shape.ShouldBe(new [,]{
                {1, 1, 1},
                {0, 0, 1}
            });
        }
        
        [Fact]
        public void RightRotateHoe()
        {
            ShapeObject shapeObject = new ShapeObject(new [,]
            {
                {0, 1},
                {0, 1},
                {1, 1}
            }, 0);
            
            shapeObject.RotateShape(Rotation.Right);
            
            shapeObject.Shape.ShouldBe(new [,]{
                {1, 0, 0},
                {1, 1, 1}
            });
        }
        
        [Fact]
        public void RightRotateLine()
        {
            ShapeObject shapeObject = new ShapeObject(new [,]
            {
                {1},
                {1},
                {1},
                {1}
            }, 0);
            
            shapeObject.RotateShape(Rotation.Right);
            
            shapeObject.Shape.ShouldBe(new [,]{
                {1, 1, 1, 1}
            });
        }
    }
}