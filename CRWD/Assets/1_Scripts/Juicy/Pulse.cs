using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.Juicy
{
    public class Pulse : MonoBehaviour
    {
        [SerializeField] private float delay = 0.5f;
        [SerializeField] private bool loop = true;
        [SerializeField] private bool playOnStart = true;
        [SerializeField, DrawScriptable] private PulsationSettings settings = default;
        // Start is called before the first frame update
        void Start()
        {
            if(playOnStart) StartCoroutine(PulseRoutine());
        }

        public void Play()
        {
            StartCoroutine(PulseRoutine());
        }

        private IEnumerator PulseRoutine()
        {
            yield return StartCoroutine(settings.Play(transform));
            yield return new WaitForSeconds(delay);

            if(loop) StartCoroutine(PulseRoutine());
        }

    }
}