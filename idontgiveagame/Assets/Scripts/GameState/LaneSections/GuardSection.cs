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
