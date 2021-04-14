using System.Collections.Generic;
using System.Linq;

namespace Trajectory
{
    public class Trajectory
    {
        public IEnumerable<MoveState> MoveStates { get; }
        
        public float MaxHeight => MoveStates.Max(state => state.Coords.Y);
        
        public float Distance => MoveStates.Last().Coords.X;
        
        public float FlightTimeInSeconds => MoveStates.Last().TimeInSeconds;

        public Trajectory(IEnumerable<MoveState> moveStates)
        {
            MoveStates = moveStates;
        }
    }
}