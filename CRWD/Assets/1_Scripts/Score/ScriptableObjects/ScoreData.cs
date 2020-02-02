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
        public int currentLevelPhaseMax { get; private set; }
        public int currentWorldLevelCount { get; private set; }
        public int currentWorldLevelMax { get; private set; }

        public int scorePhase { get; private set; }
        public int scoreTotal { get; private set; }
        public int repulsorsHit { get; private set; }

        public State state;
        public bool lockScore;

        private int currentPhaseBweeps = 0;


        public void CollectBweep()
        {
            bweepsCount++;
            eventsLinker.onBweepCollected.Invoke();
            eventsLinker.onScoreDataChanged.Invoke();
            if (bweepsCount == phaseBweepsLimit)
            {
                eventsLinker.onPhaseEnd.Invoke();

                if(currentLevelPhaseCount == currentLevelPhaseMax-1)
                {
                    eventsLinker.onLevelEnd.Invoke();

                    if(currentWorldLevelCount == currentWorldLevelMax-1)
                    {
                        Debug.Log("end world");
                        eventsLinker.onWorldEnd.Invoke();
                    }
                }
            }
        }

        public void SetScore(int _seconds)
        {
            if (lockScore) return;

            int bweepsPoint = currentPhaseBweeps * 10 - repulsorsHit;
            int reftime = currentPhaseBweeps * 10;

            float timeModifier = (float)reftime / (float)_seconds * 2;

            scorePhase = Mathf.RoundToInt(timeModifier * bweepsPoint) * 111;
            scoreTotal += scorePhase;
        }

        public void AddPhaseBweepsLimit(int _limit)
        {
            phaseBweepsLimit += _limit;
            currentPhaseBweeps += _limit;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void IncreaseCurrentLevelPhaseCount()
        {
            currentLevelPhaseCount++;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void ResetCurrentLevelPhaseCount()
        {
            currentLevelPhaseCount = 0;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void SetCurrentLevelPhaseMax(int _limit)
        {
            currentLevelPhaseMax = _limit;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void IncreaseCurrentWorldLevelCount()
        {
            currentWorldLevelCount++;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void ResetCurrentWorldLevelCount()
        {
            currentWorldLevelCount = 0;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void SetCurrentWorldMax(int _limit)
        {
            currentWorldLevelMax = _limit;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public void ResetLevelScore()
        {
            currentPhaseBweeps = 0;
            scorePhase = 0;
            repulsorsHit = 0;
        }

        public void UnlockScore()
        {
            lockScore = false;
        }

        public void ResetData()
        {
            lockScore = false;
            repulsorsHit = 0;
            scorePhase = 0;
            scoreTotal = 0;
            currentPhaseBweeps = 0;
            bweepsCount = 0;
            phaseBweepsLimit = 0;
            currentLevelPhaseCount = 0;
            currentLevelPhaseMax = 0;
            currentWorldLevelCount = 0;
            currentWorldLevelMax = 0;
            eventsLinker.onScoreDataChanged.Invoke();
        }

        public enum State { Wait, InGame, Score}
    }
}