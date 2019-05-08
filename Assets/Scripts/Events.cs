public static class Events
{
    public static OnNoValidMoves NoValidMovesRemain = new OnNoValidMoves();
    public static OnRestartGame RestartGame = new OnRestartGame();
    public static OnVictory Victory = new OnVictory();
}

public class OnNoValidMoves : UnityEngine.Events.UnityEvent
{ }

public class OnRestartGame : UnityEngine.Events.UnityEvent
{ }

public class OnVictory : UnityEngine.Events.UnityEvent
{ }