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

        /// <summary>
        /// Farseer physics
        /// </summary>
        private Body circleBody;
        private CircleShape circleShape;
        private Fixture fixture;

        private Vector2 circleOrigin;

        public Ball ()
        {
            //
        }

        public void Initialize(World world, Texture2D ballTexture, Vector2 position)
        {
            this.ballTexture = ballTexture;
            this.circleOrigin = new Vector2(ballTexture.Width / 2.0f, ballTexture.Height / 2.0f);

            this.circleBody = BodyFactory.CreateBody(world, position);
            //this.circleBody = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(96 / 2.0f), 1.0f, position);
            this.circleBody.BodyType = BodyType.Dynamic;

            // Restitution = bounce
            this.circleBody.Restitution = 0.3f;
            this.circleBody.Friction = 0.5f;

            // Create a new circle shape with radius 3.125m = 200px
            this.circleShape = new CircleShape(ballTexture.Width / 64.0f, 1.0f);

            // Attach the body and shape together via fixture
            fixture = circleBody.CreateFixture(circleShape);
            fixture.OnCollision += BallOnCollision;
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
