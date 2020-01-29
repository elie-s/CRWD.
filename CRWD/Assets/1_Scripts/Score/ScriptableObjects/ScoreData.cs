using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Score/Data")]
    public class ScoreData : ScriptableObject
    {
        [SerializeField] private EventsLinker eventsLinker = default;
        public int bweepsCount { get; private set; }
        public int phaseBweepsLimit { get; private set; }
        public int currentLevelPhaseCount { get; private set; }
        public int currentLevelPhaseLimit { get; private set; }

        public void CollectBweep()
        {
            bweepsCount++;
            eventsLinker.onBweepCollected.Invoke();
            eventsLinker.onScoreDataChanged.Invoke();
            if (bweepsCount == phaseBweepsLimit)
            {
                eventsLinker.onPhaseEnd.Invoke();

                if(currentLevelPhaseCount == currentLevelPhaseLimit)
                {
                    eventsLinker.onLevelEnd.Invoke();
                }
            }
        }

        public void SetPhaseBweepsLimit(int _limit)
        {
            phaseBweepsLimit = _limit;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void AddPhaseBweepsLimit(int _limit)
        {
            phaseBweepsLimit += _limit;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void SetCurrentLevelPhaseLimit(int _limit)
        {
            currentLevelPhaseLimit = _limit;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void ResetData()
        {
            bweepsCount = 0;
            phaseBweepsLimit = 0;
            currentLevelPhaseCount = 0;
            currentLevelPhaseLimit = 0;
            eventsLinker.onScoreDataChanged.Invoke();
        }
    }
}