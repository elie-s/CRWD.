using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private EventsLinker eventsLinker = default;
        [SerializeField] private ScoreData score = default;
        [SerializeField] private PhaseManager[] levels = default;
        [SerializeField, DrawScriptable] private LevelSettings settings = default;

        PhaseManager currentLevel => levels[score.currentWorldLevelCount];

        private void Start()
        {
            //StartWorld();
            //StartLevel();
        }

        public void StartWorld()
        {
            score.ResetData();
            score.SetCurrentWorldMax(levels.Length);
        }

        public void StartLevel()
        {
            score.ResetCurrentLevelPhaseCount();
            currentLevel.SetPhasesLimit();
            eventsLinker.onLevelStart.Invoke();
            currentLevel.InstantiatePhase();
        }

        public void NextPhase()
        {
            score.IncreaseCurrentLevelPhaseCount();
            currentLevel.InstantiatePhase();
        }

        public void NextLevel()
        {
            score.IncreaseCurrentWorldLevelCount();
            StartLevel();
        }

        public void PhaseTransition()
        {
            if (score.currentLevelPhaseCount < score.currentLevelPhaseMax) StartCoroutine(currentLevel.PhaseTransitionRoutine());
        }

        public void LevelTransition()
        {
            StartCoroutine(LevelTransitionRoutine());
        }

        private IEnumerator LevelTransitionRoutine()
        {
            eventsLinker.onLevelTransitionStart.Invoke();

            yield return new WaitForSeconds(settings.delay);

            eventsLinker.onLevelTransitionEnd.Invoke();
        }
    }
}