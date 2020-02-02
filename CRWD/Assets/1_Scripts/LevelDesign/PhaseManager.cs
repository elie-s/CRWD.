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
        [SerializeField, DrawScriptable] private PhaseSettings settings = default;

        private List<GameObject> instantiatedPhases = new List<GameObject>();

        public IEnumerator PhaseTransitionRoutine()
        {
            eventsLinker.onPhaseTransitionStart.Invoke();

            yield return new WaitForSeconds(settings.delay);

            eventsLinker.onPhaseTransitionEnd.Invoke();
        }

        public void InstantiatePhase()
        {
            if (score.currentLevelPhaseCount >= phases.Length) return;

            GameObject phase = Instantiate(phases[score.currentLevelPhaseCount], transform);
            instantiatedPhases.Add(phase);
            score.AddPhaseBweepsLimit(phase.GetComponentsInChildren<CollectableBehaviour>().Length);
            Debug.Log("count: " + phase.GetComponentsInChildren<CollectableBehaviour>().Length+" - "+score.phaseBweepsLimit);
            eventsLinker.onPhaseStart.Invoke();
        }

        public void SetPhasesLimit()
        {
            score.SetCurrentLevelPhaseMax(phases.Length);
        }

        public void Clean()
        {
            for (int i = 0; i < instantiatedPhases.Count; i++)
            {
                Destroy(instantiatedPhases[i]);
            }

            instantiatedPhases = new List<GameObject>();
        }
    }
}