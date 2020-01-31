using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Movements/Golden Snitch")]
    public class GoldenSnitchMovement : MovementSettings
    {
        [SerializeField] private RectObject area = default;
        [SerializeField] private AnimationCurve celerityCurve = default;
        [SerializeField] private float durationRef = 1.0f;
        [SerializeField] private float maxDistance = 1.5f;

        // Start is called before the first frame update

        public override IEnumerator MovementRoutine(Transform _transform, Vector2 _origin)
        {
            Transform player = GameObject.Find("Player").transform;
            MovementBehaviour movement = _transform.gameObject.GetComponent<MovementBehaviour>();

            Vector2 waypoint = NewWaypoint(_transform, player);

            float timer = 0.0f;
            float duration = (waypoint - (Vector2)_transform.position).magnitude / maxDistance * durationRef;
            Vector2 origin = _transform.position;


            while (timer < duration)
            {
                _transform.position = Vector2.Lerp(origin, waypoint, celerityCurve.Evaluate(timer / duration));

                yield return null;
                timer += Time.deltaTime;
            }

            _transform.position = waypoint;
            yield return null;

            movement.StartCoroutine(Move(movement, _transform, player));
        }


        private IEnumerator Move(MovementBehaviour _movementBehaviour, Transform _transform, Transform _player)
        {
            bool canFlee = true;
            Vector2 waypoint = NewWaypoint(_transform, _player);

            float timer = 0.0f;
            float duration = (waypoint - (Vector2)_transform.position).magnitude / maxDistance * durationRef;
            Vector2 origin = _transform.position;


            while (timer < duration)
            {
                _transform.position = Vector2.Lerp(origin, waypoint, celerityCurve.Evaluate(timer / duration));

                if (canFlee && Vector2.Distance(_transform.position, _player.position) < maxDistance)
                {
                    _movementBehaviour.StartCoroutine(FleeRoutine());
                    goto jump;
                }

                yield return null;
                timer += Time.deltaTime;
            }

            _transform.position = waypoint;
            yield return null;


        jump:
            _movementBehaviour.StartCoroutine(Move(_movementBehaviour, _transform, _player));

             IEnumerator FleeRoutine()
            {
                canFlee = false;

                yield return new WaitForSeconds(0.5f);

                canFlee = true;
            }
        }

        private Vector2 NewWaypoint(Transform _transform, Transform _player)
        {
            Vector2 result = Vector2.zero;
            float distance = Vector3.Distance(_transform.position, _player.position);

            if (distance > maxDistance)
            {
                do
                {
                    result = (Vector2)_transform.position + Random.insideUnitCircle * maxDistance;
                } while (CheckBoundaries(result));
            }
            else
            {
                int count = 0;

                do
                {
                    result = (Vector2)_transform.position + Random.insideUnitCircle * maxDistance / 2 + (Vector2)(_transform.position - _player.position);
                    count++;
                } while (CheckBoundaries(result) && count < 20);

                if (count >= 20)
                {
                    do
                    {
                        result = (Vector2)_transform.position + Random.insideUnitCircle * maxDistance;
                    } while (CheckBoundaries(result));
                }
            }

            return result;
        }

        public override void DrawGizmos(Transform _transform)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawLine(new Vector3(area.rect.x, area.rect.y), new Vector3(area.rect.x, area.rect.y + area.rect.height));
            Gizmos.DrawLine(new Vector3(area.rect.x + area.rect.width, area.rect.y + area.rect.height), new Vector3(area.rect.x, area.rect.y + area.rect.height));
            Gizmos.DrawLine(new Vector3(area.rect.x + area.rect.width, area.rect.y + area.rect.height), new Vector3(area.rect.x + area.rect.width, area.rect.y));
            Gizmos.DrawLine(new Vector3(area.rect.x, area.rect.y), new Vector3(area.rect.x + area.rect.width, area.rect.y));
        }

        public override void DrawGizmos(Vector2 _origin)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawLine(new Vector3(area.rect.x, area.rect.y), new Vector3(area.rect.x, area.rect.y + area.rect.height));
            Gizmos.DrawLine(new Vector3(area.rect.x + area.rect.width, area.rect.y + area.rect.height), new Vector3(area.rect.x, area.rect.y + area.rect.height));
            Gizmos.DrawLine(new Vector3(area.rect.x + area.rect.width, area.rect.y + area.rect.height), new Vector3(area.rect.x + area.rect.width, area.rect.y));
            Gizmos.DrawLine(new Vector3(area.rect.x, area.rect.y), new Vector3(area.rect.x + area.rect.width, area.rect.y));
        }

        private bool CheckBoundaries(Vector2 _pos)
        {
            bool result = _pos.x < area.rect.x || _pos.x > area.rect.x + area.rect.width || _pos.y < area.rect.y || _pos.y > area.rect.y + area.rect.height;
            return result;
        }
    }
}
