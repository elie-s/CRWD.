using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Repulsion/Settings")]
    public class RepulsionSettings : ScriptableObject
    {
        public float duration;
        public AnimationCurve curve;

        public IEnumerator RepulsionRoutine(Transform _transform, Vector2 _direction, float _force)
        {
            float timer = 0.0f;

            while (timer < duration)
            {
                _transform.position += (Vector3)_direction * Time.deltaTime * _force * curve.Evaluate(timer / duration);

                yield return null;
                timer += Time.deltaTime;
            }
        }
    }
}