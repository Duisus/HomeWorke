using System;

namespace Trajectory
{
    public class MathVector
    {
        public float X { get; }
        public float Y { get; }

        public MathVector(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public static MathVector CreateWithModuleAndAngle(float module, float AngleInRad)
        {
            var x = module * (float) Math.Cos(AngleInRad);
            var y = module * (float) Math.Sin(AngleInRad);

            return new MathVector(x, y);
        }
    }
}