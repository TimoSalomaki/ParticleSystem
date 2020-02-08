using System;
using Microsoft.Xna.Framework;

namespace ParticleSystem.Helpers
{
    public static class Vector2Helper
    {
        public static float GetAngleInDegrees(Vector2 oldLocation, Vector2 newLocation)
        {
            var directionVector = newLocation - oldLocation;

            return (float)Math.Atan2(directionVector.Y, directionVector.X);
        }

        public static float GetAngleInDegrees(Vector2 velocity)
        {
            return (float)Math.Atan2(velocity.Y, velocity.X);
        }

        public static float GetAngleInRadians(Vector2 oldLocation, Vector2 newLocation)
        {
            return MathHelper.ToRadians(GetAngleInDegrees(oldLocation, newLocation));
        }

        public static float GetAngleInRadians(Vector2 velocity)
        {
            return MathHelper.ToRadians(GetAngleInDegrees(velocity));
        }
    }
}
