using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A bike that leaves a trail as it moves.</para>
    /// <para>The responsibility of bike is to move itself.</para>
    /// </summary>
    public class Bike : Actor
    {
        private List<Actor> segments = new List<Actor>();

        /// <summary>
        /// Constructs a new instance of a Bike.
        /// </summary>
        public Bike(int x)
        {
            PrepareBody(x);
        }

        /// <summary>
        /// Gets the Bike's trail segments.
        /// </summary>
        /// <returns>The trail segments in a List.</returns>
        public List<Actor> GetBody()
        {
            return new List<Actor>(segments.Skip(1).ToArray());
        }

        /// <summary>
        /// Gets the Bike's segment.
        /// </summary>
        /// <returns>The bike segment as an instance of Actor.</returns>
        public Actor GetHead()
        {
            return segments[0];
        }

        /// <summary>
        /// Gets the bike's trail segments (including the bike).
        /// </summary>
        /// <returns>A list of bike trail segments as instances of Actors.</returns>
        public List<Actor> GetSegments()
        {
            return segments;
        }

        /// <summary>
        /// Grows the bike's trail by the given number of segments.
        /// </summary>
        /// <param name="numberOfSegments">The number of segments to grow.</param>
        public void GrowTrail(int numberOfSegments, Color color)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                Actor tail = segments.Last<Actor>();
                Point velocity = tail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = tail.GetPosition().Add(offset);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText("#");
                segment.SetColor(color);
                segments.Add(segment);
            }
        }

        /// <inheritdoc/>
        public override void MoveNext()
        {
            foreach (Actor segment in segments)
            {
                segment.MoveNext();
            }

            for (int i = segments.Count - 1; i > 0; i--)
            {
                Actor trailing = segments[i];
                Actor previous = segments[i - 1];
                Point velocity = previous.GetVelocity();
                trailing.SetVelocity(velocity);
            }
        }

        /// <summary>
        /// Turns the bike in the given direction.
        /// </summary>
        /// <param name="velocity">The given direction.</param>
        public void TurnHead(Point direction)
        {
            segments[0].SetVelocity(direction);
        }

        /// <summary>
        /// Prepares the bike and trail for moving.
        /// </summary>
        private void PrepareBody(int x)
        {
            int y = Constants.MAX_Y / 2;

            for (int i = 0; i < Constants.SNAKE_LENGTH; i++)
            {
                Point position = new Point(x - i * Constants.CELL_SIZE, y);
                Point velocity = new Point(1 * Constants.CELL_SIZE, 0);
                string text = i == 0 ? "8" : "#";
                Color color = i == 0 ? Constants.YELLOW : Constants.GREEN;

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText(text);
                segment.SetColor(color);
                segments.Add(segment);
            }
        }
    }
}