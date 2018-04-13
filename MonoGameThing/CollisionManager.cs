using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameThing
{
    class CollisionManager
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 center;

        public float top;
        public float bottom;
        public float left;
        public float right;

        public CollisionManager(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void UpdatePosition(Vector2 newPos)
        {
            position = newPos;

            center.X = position.X + texture.Width / 2;
            center.Y = position.Y + texture.Height / 2;

            top = position.Y;
            bottom = position.Y + texture.Height;
            left = position.X;
            right = position.X + texture.Width;
        }

        public bool CheckCollision(CollisionManager collisionObject)
        {

            if (((center.X > collisionObject.left) && (center.X < collisionObject.right)) &&
                ((center.Y > collisionObject.top) && (center.Y < collisionObject.bottom)))
            {
                return true;
            }

            return false;
        }
    }
}
