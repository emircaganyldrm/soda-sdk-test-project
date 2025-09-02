using System;

public static class EventManager
{


    //---------------------------------------------------------------------------------
    #region Level Events
    public static Action StartLevel { get; internal set; }
    public static Action RestartLevel { get; internal set; }
    #endregion


    //---------------------------------------------------------------------------------
    #region Game Manager Events
    public static Action LevelCompleted { get; internal set; }
    #endregion


    //---------------------------------------------------------------------------------
    #region Position Events
    //---------------------------------------------------------------------------------
    public static Func<float> GetPlayerZPosition { get; internal set; }
    #endregion
}
