using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace idgag.GameState.LaneSections
{
    public class ExitSection : LaneSection
    {
        public override bool IsAllowedToPass()
        {
            return false;
        }
    }
}
