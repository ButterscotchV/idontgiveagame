using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idgag.GameState.LaneSections
{
    public class GuardSection : LaneSection
    {
        public override bool IsAllowedToPass()
        {
            return numAi > 6;
        }
    }
}
