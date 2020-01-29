﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class MovementBehaviour : MonoBehaviour, IRepulsable
    {
        [SerializeField] private bool playOnAwake = false;
        [SerializeField] private RepulsionSettings repulse = default;
        [SerializeField, DrawScriptable] private MovementSettings settings = default;

        private IEnumerator loop;
        private IEnumerator routine;
        private IEnumerator repulsion;

        private void Awake()
        {
            if (playOnAwake) StartRoutine();
        }

        public void StartRoutine()
        {
            if (!settings) return;
            routine = settings.MovementRoutine(transform, transform.position);
            loop = LoopRoutine();
            StartCoroutine(loop);
        }

        private IEnumerator LoopRoutine()
        {
            yield return StartCoroutine(routine);

            StartCoroutine(loop);
        }

        public void StopRoutine()
        {
            if (!settings) return;
            StopCoroutine(routine);
            StopCoroutine(loop);
        }

        public void Repulse(Vector2 _direction, float _force)
        {
            if (!repulse) return;

            StopCoroutine(repulsion);
            repulsion = repulse.RepulsionRoutine(transform, _direction, _force);
            StartCoroutine(repulsion);
        }
    }
}