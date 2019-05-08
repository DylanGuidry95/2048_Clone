public static class Events
{
    public static OnNoValidMoves NoValidMovesRemain = new OnNoValidMoves();
}

public class OnNoValidMoves : UnityEngine.Events.UnityEvent
{
}
