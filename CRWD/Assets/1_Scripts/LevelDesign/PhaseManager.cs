using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class PhaseManager : MonoBehaviour
    {
        [SerializeField] private EventsLinker eventsLinker = default;
        [SerializeField] private ScoreData score = default;
        [SerializeField] private GameObject[] phases = default;
        [SerializeField, DrawScriptable] private PhaseSettings settings;

        public IEnumerator SetPhase(Transform _parent)
        {
            eventsLinker.onPhaseTransitionStart.Invoke();

            yield return new WaitForSeconds(settings.delay);

            eventsLinker.onPhaseTransitionEnd.Invoke();

            GameObject phase = Instantiate(phases[score.currentLevelPhaseCount], _parent);
            score.AddPhaseBweepsLimit(phase.GetComponentsInChildren<CollectableBehaviour>().Length);
        }

        public void SetPhasesLimit()
        {
            score.SetCurrentLevelPhaseMax(phases.Length);
        }
    }
}