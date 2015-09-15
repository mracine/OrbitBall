using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;

namespace orbitball
{
    class Line
    {
        private Path path;
        private const int maxLineSegments = 22;
        private const float fadeTime = 1.0f;

        public Line()
        {
            //
        }

        public void Initialize(World world)
        {
            path = new Path();
            path.Add(new Vector2(0, 10));
            path.Add(new Vector2(2.5f, 7.5f));
            path.Add(new Vector2(10, 9));
            path.Add(new Vector2(7.5f, 0.5f));
            path.Add(new Vector2(-2.5f, 7));
            path.Closed = false;

            List<Shape> shapes = new List<Shape>(2);
            shapes.Add(new PolygonShape(PolygonTools.CreateRectangle(0.5f, 0.5f, new Vector2(-0.1f, 0), 0), 1.0f));
            shapes.Add(new CircleShape(0.5f, 1.0f));

            List<Body> bodies = PathManager.EvenlyDistributeShapesAlongPath(world, path, shapes, BodyType.Dynamic, 20, 1);
            PathManager.AttachBodiesWithRevoluteJoint(world, bodies, new Vector2(0, 0.5f), new Vector2(0, -0.5f), true, true);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(path, ConvertUnits.ToDisplayUnits(new Vector2(0, 10)), null, Color.ForestGreen, 0.0f, new Vector2(0, 10), 1.0f, SpriteEffects.None, 0.0f);
        }

        public void Update()
        {
            //
        }
    }
}
