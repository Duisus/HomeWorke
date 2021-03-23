using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Trajectory
{
    public class TrajectoryCalculatorWithResistance
    {
        private const float G = 9.81f;

        public float StartSpeed { get; }
        public float AngleInRad { get; }
        public PointF StartPoint { get; }
        public float ResistanceCoefficient { get; }
        public float Mass { get; }

        public TrajectoryCalculatorWithResistance( // TODO create builder or configuration class
            PointF startPoint, float startSpeed, float angleInDeg, float mass, float resistanceCoefficient)
        {
            StartSpeed = startSpeed;
            AngleInRad = angleInDeg * (float) Math.PI / 180;
            StartPoint = startPoint;
            ResistanceCoefficient = resistanceCoefficient;
            Mass = mass;
        }

        public IEnumerable<TrajectoryPoint> GetPoints(float timeIntervalInSeconds)
        {
            var currentTime = 0.0f;
            var currentPoint = StartPoint;
            var currentSpeed = MathVector.CreateWithModuleAndAngle(StartSpeed, AngleInRad);

            while (true) //TODO refactor
            {
                yield return new TrajectoryPoint(currentTime, currentPoint);

                currentTime += timeIntervalInSeconds;
                currentPoint = CalculateNextPoint(
                    timeIntervalInSeconds, currentPoint, currentSpeed);
                currentSpeed = CalculateNextSpeed(
                    currentSpeed, timeIntervalInSeconds, CalculateWindImpact(currentTime));

                if (currentPoint.Y < 0)
                {
                    var lastPoint = new PointF(currentPoint.X, 0);
                    yield return new TrajectoryPoint(currentTime, lastPoint);
                    yield break;
                }
            }
        }

        private MathVector CalculateNextSpeed(MathVector currentSpeed, float dt, float windImpact)
        {
            var nextSpeedX = currentSpeed.X * (1 - dt * windImpact / Mass);
            var nextSpeedY = currentSpeed.Y - dt * (G + windImpact * currentSpeed.Y / Mass);

            return new MathVector(nextSpeedX, nextSpeedY);
        }

        private float CalculateWindImpact(float time) => ResistanceCoefficient * time + 0.1f;

        private PointF CalculateNextPoint(float dt, PointF currentPoint, MathVector currentSpeed)
        {
            var nextX = currentPoint.X + dt * currentSpeed.X;
            var nextY = currentPoint.Y + dt * currentSpeed.Y;

            return new PointF(
                (float) Math.Round(nextX, 4),
                (float) Math.Round(nextY, 4));
        }
    }
}