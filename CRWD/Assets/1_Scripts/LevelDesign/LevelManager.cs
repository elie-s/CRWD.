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
        [SerializeField] private TextMeshProUGUI[] mainUI = default;
        [SerializeField] private Image uiBweep = default;
        [SerializeField, DrawScriptable] private LevelSettings settings = default;

        PhaseManager currentLevel => levels[score.currentWorldLevelCount];

        private void Start()
        {
            //StartWorld();
            //StartLevel();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N)) ForceEndPhase();
        }

        public void StartWorld()
        {
            eventsLinker.onWorldStart.Invoke();
            score.ResetData();
            score.SetCurrentWorldMax(levels.Length);
            timer.Play();
            score.state = ScoreData.State.InGame;
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
            if(score.currentWorldLevelCount < score.currentLevelPhaseMax-1) StartCoroutine(LevelTransitionRoutine());
        }

        public void ForceEndPhase()
        {
            score.lockScore = true;
            CollectableBehaviour[] collectables = FindObjectsOfType<CollectableBehaviour>();

            foreach (CollectableBehaviour collectable in collectables)
            {
                collectable.Collect();
            }
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

            uiBg.color = settings.uiFadeOutGradient.Evaluate(1.0f);
            foreach (var text in uiTexts)
            {
                text.color = settings.uiTextsFadeOutGradient.Evaluate(1.0f);
            }

            eventsLinker.onLevelTransitionEnd.Invoke();
            timer.Restart();
        }
        
        public void CleanLevels()
        {
            StartCoroutine(CleanLevelsRoutine());
        }

        public IEnumerator CleanLevelsRoutine()
        {
            yield return new WaitForSeconds(settings.cleanDelay);

            foreach (PhaseManager phaseManager in levels)
            {
                phaseManager.Clean();
            }
        }

        public void FadeInUI()
        {
            StartCoroutine(FadeInUIRoutine());
        }

        private IEnumerator FadeInUIRoutine()
        {
            Evaluation evaluation = new Evaluation(settings.mainUIFadeDuration);

            while (evaluation.isBelowOne)
            {
                uiBweep.color = settings.uiTextsFadeInGradient.Evaluate(evaluation.fraction);
                foreach (TextMeshProUGUI element in mainUI)
                {
                    element.color = settings.uiTextsFadeInGradient.Evaluate(evaluation.fraction);
                }

                yield return evaluation.YieldIncrement();
            }

            uiBweep.color = settings.uiTextsFadeInGradient.Evaluate(1.0f);
            foreach (TextMeshProUGUI element in mainUI)
            {
                element.color = settings.uiTextsFadeInGradient.Evaluate(1.0f);
            }
        }

        public void FadeOutUI()
        {
            StartCoroutine(FadeOutUIRoutine());
        }

        private IEnumerator FadeOutUIRoutine()
        {
            Evaluation evaluation = new Evaluation(settings.mainUIFadeDuration);

            while (evaluation.isBelowOne)
            {
                uiBweep.color = settings.uiTextsFadeOutGradient.Evaluate(evaluation.fraction);
                foreach (TextMeshProUGUI element in mainUI)
                {
                    element.color = settings.uiTextsFadeOutGradient.Evaluate(evaluation.fraction);
                }

                yield return evaluation.YieldIncrement();
            }

            uiBweep.color = settings.uiTextsFadeOutGradient.Evaluate(1.0f);
            foreach (TextMeshProUGUI element in mainUI)
            {
                element.color = settings.uiTextsFadeOutGradient.Evaluate(1.0f);
            }
        }

        public void UpdateLevelClearedDisplay()
        {
            uiTexts[0].text = "LEVEL " + (score.currentWorldLevelCount +1) + "/" + score.currentWorldLevelMax + " CLEARED!";
        }
    }
}