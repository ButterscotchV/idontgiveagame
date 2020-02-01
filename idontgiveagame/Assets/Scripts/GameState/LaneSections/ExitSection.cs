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
