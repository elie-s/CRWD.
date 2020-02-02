using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace CRWD
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private EventsLinker eventsLinker = default;
        [SerializeField] private ScoreData score = default;
        [SerializeField] private PhaseManager[] levels = default;
        [SerializeField] private TimerCount timer = default;
        [SerializeField] private Image uiBg = default;
        [SerializeField] private TextMeshProUGUI[] uiTexts = default;
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
            timer.Play();
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
            timer.Stop();

            Evaluation evaluation = new Evaluation(settings.delay/2);
            
            while (evaluation.isBelowOne)
            {
                uiBg.color = settings.uiFadeInGradient.Evaluate(evaluation.fraction);
                foreach (var text in uiTexts)
                {
                    text.color = settings.uiTextsFadeInGradient.Evaluate(evaluation.fraction);
                }

                yield return evaluation.YieldIncrement();
            }

            evaluation = new Evaluation(settings.delay / 2);

            while (evaluation.isBelowOne)
            {
                uiBg.color = settings.uiFadeOutGradient.Evaluate(evaluation.fraction);
                foreach (var text in uiTexts)
                {
                    text.color = settings.uiTextsFadeOutGradient.Evaluate(evaluation.fraction);
                }

                yield return evaluation.YieldIncrement();
            }

            eventsLinker.onLevelTransitionEnd.Invoke();
            timer.Restart();
        }
    }
}