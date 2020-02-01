using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idgag.GameState
{
    enum FuckBucketTarget
    {
        Economy,
        Environment
    }

    public class FuckBucket
    {
        FuckBucketTarget target;
        int numFucks = 0;
    }
}
