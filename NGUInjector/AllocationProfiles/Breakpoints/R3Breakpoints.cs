using NGUInjector.AllocationProfiles.BreakpointTypes;
using SimpleJSON;
using System.Linq;

namespace NGUInjector.AllocationProfiles.Breakpoints
{
    public class R3Breakpoints : BaseBreakpoints<ResourceBreakpoint[]>
    {
        public R3Breakpoints() : base() { }

        public R3Breakpoints(JSONNode bps) :
            base(bps, (bp) => ResourceBreakpoint.ParseBreakpointArray(bp["Priorities"], ResourceType.R3).ToArray()) { }

        protected override bool PerformSwap(Breakpoint bp)
        {
            var temp = bp.priorities.Where(x => x.IsValid()).ToList();
            if (temp.Count == 0)
                return false;
            RemoveR3();
            foreach (var prio in temp)
            {
                prio.UpdateMaxAllocation();
                prio.Allocate();
            }
            _character.hacksController.refreshMenu();
            return false;
        }

        private void RemoveR3() => _character.hacksController.removeAllR3();
    }
}
