using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Movements/Linear")]
    public class LinearMovement : MovementSettings
    {
        [SerializeField] private AnimationCurve curve = default;
        [SerializeField] private float duration = 1.0f;
        [SerializeField] private float delay = 0.0f;
        [SerializeField] private Vector2 direction = default;
        [SerializeField] private float range = 1.0f;

        //Private
        bool startedMoving = false;

        public override IEnumerator MovementRoutine(Transform _transform, Vector2 _origin)
        {
            if(!startedMoving)
            {
                yield return new WaitForSeconds(delay);
                startedMoving = true;
            }

            Vector2 startPos = _origin;
            Vector2 endPos = _origin + direction.normalized * range;
            float timer = 0.0f;

            while (timer < duration)
            {
                _transform.position = Vector2.Lerp(startPos, endPos, curve.Evaluate(timer / duration));

                yield return null;
                timer += Time.deltaTime;
            }

            _transform.position = endPos;

            yield return new WaitForSeconds(delay);

            timer = 0.0f;

            while (timer < duration)
            {
                _transform.position = Vector3.Lerp(endPos, startPos, curve.Evaluate(timer / duration));

                yield return null;
                timer += Time.deltaTime;
            }

            _transform.position = startPos;
        }

        public override void DrawGizmos(Transform _transform)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawLine(_transform.position, _transform.position + (Vector3)direction * range);
        }

        public override void DrawGizmos(Vector2 _origin)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawLine(_origin, _origin + direction * range);
        }
    }
}