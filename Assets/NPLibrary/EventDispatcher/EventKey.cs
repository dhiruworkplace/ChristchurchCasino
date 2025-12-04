
public enum EventKey {
    #region Application
    OnApplicationPause,
    OnApplicationResume,
    #endregion

    #region Currencies
    OnCoinChanged,
    OnGemChanged,
    #endregion

    #region State
    MANAGER_INIT,
    MANAGER_LOSE,
    MANAGER_REPLAY,
    MANAGER_NEXT,
    MANAGER_STARTGAME,
    MANAGER_WIN,
    MANAGER_REVIVE,
    SFX_CHANGE,
    SOUND_CHANGE,
    #endregion

    LOAD_LEVEL,
    UPGRADE,
}