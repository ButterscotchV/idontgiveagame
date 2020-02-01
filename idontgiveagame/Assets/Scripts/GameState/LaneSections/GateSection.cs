using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace idgag.GameState.LaneSections
{
    public class GateSection : LaneSection
    {
        public override bool IsAllowedToPass()
        {
            return numAi > 10;
        }
    }
}
