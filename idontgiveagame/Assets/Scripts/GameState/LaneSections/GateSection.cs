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
