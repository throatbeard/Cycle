using System.Collections.Generic;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public DrawActorsAction(VideoService videoService)
        {
            this.videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Bike bike = (Bike)cast.GetFirstActor("bike");
            Bike bike2 = (Bike)cast.GetFirstActor("bike2");
            List<Actor> segments = bike.GetSegments();
            List<Actor> segments2 = bike2.GetSegments();
           // Actor score = cast.GetFirstActor("score");
            //Actor food = cast.GetFirstActor("food");
            List<Actor> messages = cast.GetActors("messages");
            
            videoService.ClearBuffer();
            videoService.DrawActors(segments);
            videoService.DrawActors(segments2);
            //videoService.DrawActor(score);
            //videoService.DrawActor(food);
            videoService.DrawActors(messages);
            videoService.FlushBuffer();

            bike.GrowTrail(1, bike.GetColor());
            bike2.GrowTrail(1, bike2.GetColor());
        }
    }
}