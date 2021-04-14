using System.Drawing;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;
using Trajectory;

namespace TrajectoryTests
{
    public class TrajectoryCalculatorTests
    {
        private const float G = 9.81f;
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
        public void GetPoints_FirstPointIsStartPointFromCtor()
        {
            var startPoint = new PointF(10.3f, 15);
            calculator = new TrajectoryCalculator(
                startPoint, 40.31f, 30, 3.1f, 1);

            calculator.GetPoints(0.1f).First().Coords
                .Should().Be(startPoint);
        }

        [Test]
        public void GetPoints_LastPointHasZeroYCoordinate()
        {
            calculator.GetPoints(0.1f).Last().Coords.Y
                .Should().Be(0);
        }

        [Test]
        public void GetPoint_LastPointHasXCoordinateThatEqualToFlightRange()
        {
            calculator.GetPoints(0.01f).Last().Coords.X
                .Should().BeApproximately(5, 0.05f); // TODO refactor
        }

        [Test]
        public void GetPoint_HighestPointHasCorrectYCoordinate()
        {
            calculator.GetPoints(0.01f).Max(trajPoint => trajPoint.Coords.Y)
                .Should().BeApproximately(1.75f, 0.05f); // TODO refactor
        }

        [Test]
        [UseReporter(typeof(VisualStudioReporter))]
        public void ApprovalTest()
        {
            var resultPoints = calculator.GetPoints(0.1f);
            var strFromAllPoints = string.Join(
                '\n', resultPoints.Select(p => p.ToString()));
            
            Approvals.Verify(strFromAllPoints);
        }
    }
}