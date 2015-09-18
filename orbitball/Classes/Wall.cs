using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace orbitball
{
    class Wall
    {
        private Texture2D wallTexture;
        private Body wallBody;
        private Vector2 textureOrigin;

        public Wall()
        {
            //
        }

        public void Initialize(World world, Texture2D wallTexture, Vector2 wallPosition)
        {
            this.wallTexture = wallTexture;
            this.textureOrigin = new Vector2(wallTexture.Width / 2, wallTexture.Height / 2);
            this.wallBody = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(wallTexture.Width), ConvertUnits.ToSimUnits(wallTexture.Height), 1.0f, wallPosition);
            this.wallBody.BodyType = BodyType.Static;
            this.wallBody.Restitution = 1.0f;
            this.wallBody.Rotation = (float) Math.PI / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(wallTexture, ConvertUnits.ToDisplayUnits(wallBody.Position), null, Color.White, wallBody.Rotation, textureOrigin, 1f, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            //
        }
    }
}
