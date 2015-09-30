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
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;

namespace orbitball
{
    class Line
    {
        private Texture2D lineTexture;
        private Vector2 lineTextureCenter;

        private List<Body> bodies;

        // NOTE: Subdivisions need +1 line segments
        private const int maxLineSegments = 23;
        private const float segmentRadius = 0.1f;
        private const float fadeTime = 1.0f;

        private bool isDrawing;
        public bool IsDrawing { get { return isDrawing; } }

        public Line()
        {
            //
        }

        public void Initialize(World world, Texture2D lineTexture)
        {
            this.lineTexture = lineTexture;
            this.lineTextureCenter = new Vector2(lineTexture.Width / 2, lineTexture.Height / 2);

            bodies = new List<Body>();
        }

        public bool LineOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Body vertex in bodies) 
            {
                //path.GetVertices(currentLineSegments)
                spriteBatch.Draw(lineTexture, ConvertUnits.ToDisplayUnits(vertex.Position), null, Color.White, vertex.Rotation, lineTextureCenter, 1.0f, SpriteEffects.None, 0.0f);
            }
        }

        public void DrawBorder(SpriteBatch spriteBatch)
        {
            foreach (Body vertex in bodies)
            {
                //path.GetVertices(currentLineSegments)
                spriteBatch.Draw(lineTexture, ConvertUnits.ToDisplayUnits(vertex.Position), null, Color.Black, vertex.Rotation, lineTextureCenter, 1.1f, SpriteEffects.None, 0.0f);
            }
        }

        public void Update(World world, Vector2 mousePosition)
        {
            // Re-create line if cursor has changed
            double distance = GetDistance(mousePosition, bodies[bodies.Count - 1].Position);
            if(distance >= segmentRadius)
            {
                ChangeLine(world, mousePosition, distance);
            }
        }

        public void BeginDrawing(World world, Vector2 mousePosition)
        {
            isDrawing = true;
            bodies.Add(BodyFactory.CreateCircle(world, segmentRadius, 1.0f, mousePosition));
        }

        public void EndDrawing(Vector2 mousePosition)
        {
            isDrawing = false;
            //Vector2 first = bodies[0].Position;
            //Vector2 last = bodies[bodies.Count - 1].Position;

            //bodies.Clear();
        }

        private void ChangeLine(World world, Vector2 mousePosition, double distance)
        {
            // Add appropriate number of bodies
            double segmentDistance = segmentRadius * 2;
            int newLineSegments = (int) Math.Round(distance / segmentDistance, 2, MidpointRounding.AwayFromZero);
            Vector2 direction = mousePosition - bodies[bodies.Count - 1].Position;
            direction.Normalize();

            for (int i = 0; i < newLineSegments; i++)
            {
                Vector2 newPoint = bodies[bodies.Count - 1].Position + Vector2.Multiply(direction, (float) segmentDistance);
                bodies.Add(BodyFactory.CreateCircle(world, segmentRadius, 1.0f, newPoint));
            }

            // Remove appropriate number of bodies
            if(bodies.Count >= maxLineSegments)
            {
                for(int i = 0; i < newLineSegments; i++)
                {
                    Body first = bodies[0];
                    bodies.RemoveAt(0);
                    first.Dispose();
                }
            }
        }

        private static double GetDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
