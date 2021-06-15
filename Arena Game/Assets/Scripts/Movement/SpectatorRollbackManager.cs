using System;
using UnityEngine;


public class SpectatorRollbackManager : MonoBehaviour
{
    #region Public.
    /// <summary>
    /// Dispatched when a rollback starts.
    /// </summary>
    public static event Action OnRollbackStart;
    /// <summary>
    /// Dispatched when a rollback ends.
    /// </summary>
    public static event Action OnRollbackEnd;
    #endregion


    /// <summary>
    /// Initializes this script for use. Should only be completed once.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void FirstInitialize()
    {
        GameObject go = new GameObject();
        go.AddComponent<SpectatorRollbackManager>();
        DontDestroyOnLoad(go);
    }

    /// <summary>
    /// Indicates that a rollback has started.
    /// </summary>
    public static void StartRollback()
    {
        OnRollbackStart?.Invoke();
        Physics.SyncTransforms();
        Physics2D.SyncTransforms();
    }

    /// <summary>
    /// Indicates that a rollback has ended.
    /// </summary>
    public static void EndRollback()
    {
        OnRollbackEnd?.Invoke();
    }

}
