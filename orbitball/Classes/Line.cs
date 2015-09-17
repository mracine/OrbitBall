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
using FarseerPhysics.Factories;

namespace orbitball
{
    class Line
    {
        private Texture2D lineTexture;
        private Vector2 lineSegmentOrigin;

        private Path path;
        private List<Body> bodies;
        private Body lineBody;

        // NOTE: Subdivisions need +1 line segments
        private const int maxLineSegments = 23;
        private int currentLineSegments = 23;
        private float lineSegmentRadius;
        private const float fadeTime = 1.0f;

        public Line()
        {
            //
        }

        public void Initialize(World world, Texture2D lineTexture)
        {
            this.lineTexture = lineTexture;
            this.lineSegmentOrigin = new Vector2(lineTexture.Width / 2, lineTexture.Height / 2);

            path = new Path();
            path.Add(new Vector2(2, 10));
            path.Add(new Vector2(16, 10));
            path.Closed = false;

            // Create path and subdivide bodies
            this.lineSegmentRadius = ConvertUnits.ToSimUnits(lineTexture.Width) / 2;
            bodies = PathManager.EvenlyDistributeShapesAlongPath(world, path, new CircleShape(lineSegmentRadius, 1.0f), BodyType.Static, currentLineSegments);

            this.lineBody = new Body(world);
            PathManager.ConvertPathToEdges(path, lineBody, currentLineSegments);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Body vertex in bodies) 
            {
                //path.GetVertices(currentLineSegments)
                spriteBatch.Draw(lineTexture, ConvertUnits.ToDisplayUnits(vertex.Position), null, Color.White, vertex.Rotation, lineSegmentOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
        }

        public void Update()
        {
            //
        }

        public void BeginDrawing(Point mousePosition)
        {
            //
        }

        public void EndDrawing(Point mousePosition)
        {
            //
        }
    }
}
