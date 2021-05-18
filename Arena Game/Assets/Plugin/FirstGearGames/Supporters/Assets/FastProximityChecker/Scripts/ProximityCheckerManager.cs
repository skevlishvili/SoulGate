﻿using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.NetworkProximities
{


    public class ProximityCheckerManager : MonoBehaviour
    {

        #region Private.
        /// <summary>
        /// Singleton reference to this script.
        /// </summary>
        private static ProximityCheckerManager _instance;
        /// <summary>
        /// Current active checkers.
        /// </summary>
        private List<FastProximityChecker> _checkers = new List<FastProximityChecker>();
        /// <summary>
        /// Index in Checkers to start on next cycle.
        /// </summary>
        private int _nextCheckerIndex = 0;
        #endregion

        #region Const.
        /// <summary>
        /// Frames to spread updates over. Lower values will result in faster updates but will cost more performance.
        /// </summary>
        public const int TARGET_FPS = 60;
        #endregion

        private void Awake()
        {
            _instance = this;
        }


        private void Update()
        {
            if (!NetworkServer.active)
                return;

            UpdateCheckers();
        }

        /// <summary>
        /// Initializes this script for use. Should only be completed once.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void FirstInitialize()
        {
            GameObject go = new GameObject();
            go.name = "ProximityCheckerManager";
            go.AddComponent<ProximityCheckerManager>();
            DontDestroyOnLoad(go);
        }

        /// <summary>
        /// Adds a BetterProximityChecker to collection.
        /// </summary>
        /// <param name="checker"></param>
        public static void AddChecker(FastProximityChecker checker)
        {
            _instance._checkers.Add(checker);
        }

        /// <summary>
        /// Removes a BetterProximityChecker from collection.
        /// </summary>
        /// <param name="checker"></param>
        public static void RemoveChecker(FastProximityChecker checker)
        {
            int index = _instance._checkers.IndexOf(checker);
            if (index != -1)
            {
                if (index < _instance._nextCheckerIndex)
                    _instance._nextCheckerIndex--;

                _instance._checkers.RemoveAt(index);
            }
        }

        /// <summary>
        /// Updates checkers.
        /// </summary>
        private void UpdateCheckers()
        {
            /* Multiply required frames based on connection count. This will
             * reduce how quickly observers update slightly but will drastically
             * improve performance. */
            float fpsMultiplier = 1f + (float)(NetworkServer.connections.Count * 0.01f);
            /* Performing one additional iteration would
            * likely be quicker than casting two ints
            * to a float. */
            int iterations = (_checkers.Count / (int)(TARGET_FPS * fpsMultiplier)) + 1;            
            if (iterations > _checkers.Count)
                iterations = _checkers.Count;

            //Index to perform a check on.
            int checkerIndex = 0;
            /* Run the number of calculated iterations.
             * This is spaced out over frames to prevent
             * fps spikes. */
            for (int i = 0; i < iterations; i++)
            {
                checkerIndex = _nextCheckerIndex + i;
                if (checkerIndex >= _checkers.Count)
                    checkerIndex -= _checkers.Count;
                _checkers[checkerIndex].PerformCheck();
            }

            _nextCheckerIndex = (checkerIndex + 1);
        }

    }


}