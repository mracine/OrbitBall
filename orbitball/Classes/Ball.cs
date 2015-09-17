using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;

namespace orbitball
{
    class Ball
    {
        private Texture2D ballTexture;
        private Body circleBody;

        private Vector2 circleOrigin;

        public Ball ()
        {
            //
        }

        public void Initialize(World world, Texture2D ballTexture, Vector2 position)
        {
            this.ballTexture = ballTexture;
            this.circleOrigin = new Vector2(ballTexture.Width / 2, ballTexture.Height / 2);

            this.circleBody = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(ballTexture.Width) / 2, 1.0f, position);
            this.circleBody.BodyType = BodyType.Dynamic;

            // Restitution = bounce
            this.circleBody.Restitution = 1.0f;
            this.circleBody.Friction = 0.5f;

            // Event handlers
            this.circleBody.OnCollision += BallOnCollision;
        }

        public bool BallOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTexture, ConvertUnits.ToDisplayUnits(circleBody.Position), null, Color.White, circleBody.Rotation, circleOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }

        public void Update()
        {
            //
        }
    }
}
