using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameThing
{
    class CollisionManager
    {
        private Texture2D texture;
        private Vector2 position;

        public float top;
        public float bottom;
        public float left;
        public float right;

        public CollisionManager(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void UpdateSides()
        {
            top = position.Y;
            bottom = position.Y + texture.Height;
            left = position.X;
            right = position.X + texture.Width;
        }

        public void UpdatePosition(Vector2 newPos)
        {
            position = newPos;
        }

        public bool CheckCollision(CollisionManager collisionObject)
        {
            UpdateSides();
            collisionObject.UpdateSides();

            if (right > collisionObject.left && right < collisionObject.right ||
                left > collisionObject.left && left < collisionObject.right ||
                top > collisionObject.bottom && top < collisionObject.top ||
                bottom > collisionObject.top && bottom < collisionObject.bottom)
            {
                return true;
            }

            return false;
        }
    }
}
