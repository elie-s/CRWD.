using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    public class SpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sRenderer    = default;
        [SerializeField] private ScriptedBehaviourRoutine[] animations;
        [SerializeField] private UnityEvent onAllAnimationsEnd = default;

        private bool[] finished;

        private void OnEnable()
        {
            SetResultsArray();
            onAllAnimationsEnd.AddListener(SetResultsArray);
            StartEnabling();
        }

        void Update()
        {
            if (CheckResults()) onAllAnimationsEnd.Invoke();
        }

        public void StartEnabling()
        {
            for (int i = 0; i < animations.Length; i++)
            {
                if (animations[i] is ScriptedGradientRoutine) StartCoroutine(PlayAnimation(animations[i].Play(sRenderer), i));
                else if (animations[i] is ScriptedMovementRoutine) StartCoroutine(PlayAnimation(animations[i].Play(transform), i));
                else if (animations[i] is ScriptedScaleRoutine) StartCoroutine(PlayAnimation(animations[i].Play(transform), i));
            }
        }

        private IEnumerator PlayAnimation(IEnumerator _animation, int _index)
        {
            yield return StartCoroutine(_animation);

            finished[_index] = true;
        }

        private void SetResultsArray()
        {
            finished = new bool[animations.Length];

            for (int i = 0; i < finished.Length; i++)
            {
                finished[i] = false;
            }
        }

        private bool CheckResults()
        {
            foreach (bool result in finished)
            {
                if (!result) return false;
            }

            return true;
        }
    }
}