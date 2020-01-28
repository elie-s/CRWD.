using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class MovementBehaviour : MonoBehaviour, IRepulsable
    {
        [SerializeField] private bool repulsable = false;
        [SerializeField] private bool playOnAwake = false;
        [SerializeField, DrawScriptable] private MovementSettings settings = default;

        private IEnumerator routine;

        private void Awake()
        {
            if (playOnAwake) StartRoutine();
        }

        public void StartRoutine()
        {
            if (!settings) return;
            routine = settings.MovementRoutine(transform, transform.position);
            StartCoroutine(routine);
        }

        public void StopRoutine()
        {
            if (!settings) return;
            StopCoroutine(routine);
        }

        public void Repulse(Vector2 _direction, float _force)
        {
            if (!repulsable) return;
        }
    }
}