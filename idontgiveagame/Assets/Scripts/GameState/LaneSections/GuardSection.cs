namespace idgag.GameState.LaneSections
{
    public class GuardSection : LaneSection
    {
        public int capacity = 6;

        public override bool IsAllowedToPass()
        {
            return numAi > capacity;
        }
    }
}
