using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when a bike
    /// collides with the trail of the other bike.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false)
            {
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Sets the game over flag if a bike collides with the other bikes trail.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Bike bike = (Bike)cast.GetFirstActor("bike");
            Bike bike2 = (Bike)cast.GetFirstActor("bike2");
            Actor head = bike.GetHead();
            Actor head2 = bike2.GetHead();
            List<Actor> trail = bike.GetBody();
            List<Actor> trail2 = bike2.GetBody();

            foreach (Actor segment in trail)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                }
            }
            
            foreach (Actor segment in trail2)
            {
                if (segment.GetPosition().Equals(head.GetPosition()))
                {
                    isGameOver = true;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (isGameOver == true)
            {
                Bike bike = (Bike)cast.GetFirstActor("bike");
                List<Actor> segments = bike.GetSegments();
                Bike bike2 = (Bike)cast.GetFirstActor("bike2");
                List<Actor> segments2 = bike2.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                    bike.SetColor(Constants.WHITE);
                }

                foreach (Actor segment in segments2)
                {
                    segment.SetColor(Constants.WHITE);
                    bike2.SetColor(Constants.WHITE);
                }
            }
        }

    }
}