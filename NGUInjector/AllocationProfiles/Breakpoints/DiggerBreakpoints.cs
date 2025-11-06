using NGUInjector.AllocationProfiles.BreakpointTypes;
using NGUInjector.Managers;
using SimpleJSON;
using System.Linq;

namespace NGUInjector.AllocationProfiles.Breakpoints
{
    public class DiggerBreakpoints : BaseBreakpoints<int[]>
    {
        public DiggerBreakpoints() : base() { }

        public DiggerBreakpoints(JSONNode bps) :
            base(bps, (bp) => bp["List"].AsArray.Children.Select(x => x.AsInt).Where(x => x <= 11).ToArray()) { }

        protected override bool PerformSwap(Breakpoint bp)
        {
            if (!LockManager.CanSwap())
                return false;

            if (DiggerManager.EquipDiggers(bp.priorities))
            {
                Main.Log($"Equipping Diggers: {string.Join(", ", bp.priorities)}");
                current = bp;
                return false;
            }
            else
            {
                Main.Log($"Failed to equip Diggers: {string.Join(", ", bp.priorities)}");
            }

            return false;
        }
    }
}
