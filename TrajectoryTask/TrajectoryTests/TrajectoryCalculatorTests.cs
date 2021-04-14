using System.Drawing;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;
using TrajectoryClasses;

namespace TrajectoryTests
{
    public class TrajectoryCalculatorTests
    {
        private TrajectoryCalculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new TrajectoryCalculator(
                new PointF(0, 0),
                50,
                30,
                1.2f,
                0.4f);
        }

        [Test]
        public void CalculateTrajectory_FirstPointIsStartPointFromCtor()
        {
            var startPoint = new PointF(10.3f, 15);
            calculator = new TrajectoryCalculator(
                startPoint, 40.31f, 30, 3.1f, 1);

            calculator.CalculateTrajectory(0.1f).MoveStates.First().Coords
                .Should().Be(startPoint);
        }

        [Test]
        public void CalculateTrajectory_LastPointHasZeroYCoordinate()
        {
            calculator.CalculateTrajectory(0.1f).MoveStates.Last().Coords.Y
                .Should().Be(0);
        }

        [Test]
        public void CalculateTrajectory_TrajectoryHasCorrectDistance()
        {
            calculator.CalculateTrajectory(0.01f).Distance
                .Should().BeApproximately(97.6f, 0.05f); // TODO refactor
        }

        [Test]
        public void GetPoints_TrajectoryHasCorrectMaxHeight()
        {
            calculator.CalculateTrajectory(0.01f).MaxHeight
                .Should().BeApproximately(20.8f, 0.05f); // TODO refactor
        }

        [Test]
        [UseReporter(typeof(VisualStudioReporter))]
        public void ApprovalTest()
        {
            var trajectory = calculator.CalculateTrajectory(0.1f);
            var strFromAllPoints = string.Join(
                '\n', trajectory.MoveStates.Select(state => state.ToString()));
            
            Approvals.Verify(strFromAllPoints);
        }
    }
}