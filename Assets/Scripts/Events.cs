public static class Events
{
    public static OnNoValidMoves NoValidMovesRemain = new OnNoValidMoves();
    public static OnRestartGame RestartGame = new OnRestartGame();
    public static OnVictory Victory = new OnVictory();
    public static OnCellMerged MergedCells = new OnCellMerged();
    public static OnScoreUpdate ScoreUpdate = new OnScoreUpdate();
    public static DisplayScore ScoreDisplay = new DisplayScore();
}

public class OnNoValidMoves : UnityEngine.Events.UnityEvent
{ }

public class OnRestartGame : UnityEngine.Events.UnityEvent
{ }

public class OnVictory : UnityEngine.Events.UnityEvent
{ }

public class OnCellMerged : UnityEngine.Events.UnityEvent<UnityEngine.Vector3, int>
{ }

public class OnScoreUpdate : UnityEngine.Events.UnityEvent<int>
{ }

public class DisplayScore : UnityEngine.Events.UnityEvent<int>
{ }