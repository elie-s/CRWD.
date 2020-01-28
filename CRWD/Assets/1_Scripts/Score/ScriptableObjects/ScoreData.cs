using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Score/Data")]
    public class ScoreData : ScriptableObject
    {
        public int bweepsCount { get; private set; }
        public int phaseBweepsLimit { get; private set; }

        private UnityEvent onBweepCollected = default;
        private UnityEvent onPhaseCompleted = default;
        private UnityEvent onDataChanged = default;

        public void LinkEvents(UnityEvent _onBweepCollected, UnityEvent _onPhaseCompleted, UnityEvent _onDataChanged)
        {
            onBweepCollected = _onBweepCollected;
            onPhaseCompleted = _onPhaseCompleted;
            onDataChanged = _onDataChanged;
        }

        public void CollectBweep()
        {
            bweepsCount++;
            onBweepCollected.Invoke();
            onDataChanged.Invoke();
            if (bweepsCount == phaseBweepsLimit) onPhaseCompleted.Invoke();
        }

        public void SetPhaseBweepsLimit(int _limit)
        {
            phaseBweepsLimit = _limit;
            onDataChanged.Invoke();
        }

        public void ResetData()
        {
            bweepsCount = 0;
            phaseBweepsLimit = 0;
            onDataChanged.Invoke();
        }
    }
}