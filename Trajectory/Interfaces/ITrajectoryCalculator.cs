using System.Collections.Generic;

namespace Trajectory.Interfaces
{
    public interface ITrajectoryCalculator
    {
        IEnumerable<TrajectoryPoint> GetPoints(float timeIntervalInSeconds);
    }
}