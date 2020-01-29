using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class PhaseManager : MonoBehaviour
    {
        [SerializeField] private ScoreData score = default;
        [SerializeField] private GameObject[] phases = default;

        public GameObject SetPhase(Transform _parent)
        {
            index++;
            GameObject phase = Instantiate(phases[index], _parent);
            score.AddPhaseBweepsLimit(phase.GetComponentsInChildren<CollectableBehaviour>().Length);

            return phase;
        }

        private void Update()
        {
            
        }
    }
}