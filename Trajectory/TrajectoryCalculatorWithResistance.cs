﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Trajectory
{
    public class TrajectoryCalculatorWithResistance
    {
        public float StartSpeed { get; }  // TODO Maybe Vector2 ???
        public float AngleInRad { get; }
        public PointF StartPoint { get; }
        public float ResistanceCoefficient { get; }
        public float Mass { get; }

        private const float G = 9.81f;

        public TrajectoryCalculatorWithResistance(  // TODO create builder or configuration class
            float startSpeed, float angleInDeg, float mass, PointF startPoint, float resistanceCoefficient)
        {
            StartSpeed = startSpeed;
            AngleInRad = angleInDeg * (float) Math.PI / 180;
            StartPoint = startPoint;
            ResistanceCoefficient = resistanceCoefficient;  // TODO in equation - k(t) ???
            Mass = mass;
        }

        public IEnumerable<TrajectoryPoint> GetPoints(float timeIntervalInSeconds)
        {
            var currentTime = 0.0f;
            var currentPoint = StartPoint;
            var currentSpeed = new Vector2(  //todo use something instead Vector2 ???
                StartSpeed * (float) Math.Cos(AngleInRad),
                StartSpeed * (float) Math.Sin(AngleInRad));

            while (currentPoint.Y >= 0)
            {
                yield return new TrajectoryPoint(currentTime, currentPoint);
                
                currentTime += timeIntervalInSeconds;
                currentPoint = CalculateNextPoint(
                    timeIntervalInSeconds, currentPoint, currentSpeed);
                currentSpeed = CalculateNextSpeed(
                    currentSpeed, timeIntervalInSeconds);
            }
        }

        private Vector2 CalculateNextSpeed(Vector2 currentSpeed, float dt)
        {
            var nextSpeedX = currentSpeed.X * (1 - dt * ResistanceCoefficient / Mass);
            var nextSpeedY = currentSpeed.Y - dt * (G + ResistanceCoefficient * currentSpeed.Y / Mass);

            return new Vector2(nextSpeedX, nextSpeedY);
        }

        private PointF CalculateNextPoint(float dt, PointF currentPoint, Vector2 currentSpeed)
        {
            var nextX = currentPoint.X + dt * currentSpeed.X;
            var nextY = currentPoint.Y + dt * currentSpeed.Y;

            return new PointF(
                (float)Math.Round(nextX, 4),
                (float)Math.Round(nextY, 4));
        }
    }
}