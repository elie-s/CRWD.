using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class GoldenSnitchBehaviour : MonoBehaviour
    {
        [SerializeField] private RectObject area = default;
        [SerializeField] private Transform player = default;
        [SerializeField] private AnimationCurve celerityCurve = default;
        [SerializeField] private float durationRef = 1.0f;
        [SerializeField] private float maxDistance = 1.5f;
        [SerializeField] private float fleeDistance = 2.0f;
        [SerializeField] private float fleeCooldown = 0.5f;

        private Vector2 waypoint;
        private bool canFlee = true;

        void OnEnable()
        {
            if (!player) player = GameObject.Find("Player").transform;
            
        }

        public void StartRoutine()
        {
            StartCoroutine(StartMove());
        }

        public void StopRoutine()
        {
            StopAllCoroutines();
        }

        private IEnumerator StartMove()
        {
            NewWaypoint();

            float timer = 0.0f;
            float duration = (waypoint - (Vector2)transform.position).magnitude / maxDistance * durationRef;
            Vector2 origin = transform.position;


            while (timer < duration)
            {
                transform.position = Vector2.Lerp(origin, waypoint,celerityCurve.Evaluate(timer / duration));

                yield return null;
                timer += Time.deltaTime;
            }

            transform.position = waypoint;
            yield return null;

            StartCoroutine(Move());
        }

        private IEnumerator FleeRoutine()
        {
            canFlee = false;

            yield return new WaitForSeconds(fleeCooldown);

            canFlee = true;
        }

        private IEnumerator Move()
        {
            NewWaypoint();

            float timer = 0.0f;
            float duration = (waypoint - (Vector2)transform.position).magnitude / maxDistance * durationRef;
            Vector2 origin = transform.position;


            while (timer < duration)
            {
                transform.position = Vector2.Lerp(origin, waypoint, celerityCurve.Evaluate(timer / duration));

                if (canFlee && Vector2.Distance(transform.position, player.position) < fleeDistance)
                {
                    StartCoroutine(FleeRoutine());
                    goto jump;
                }

                yield return null;
                timer += Time.deltaTime;
            }

            transform.position = waypoint;
            yield return null;


        jump:
            StartCoroutine(Move());
        }

        private void NewWaypoint()
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > maxDistance)
            {
                do
                {
                    waypoint = (Vector2)transform.position + Random.insideUnitCircle * maxDistance;
                } while (CheckBoundaries(waypoint));
            }
            else
            {
                int count = 0;

                do
                {
                    waypoint = (Vector2)transform.position + Random.insideUnitCircle * maxDistance / 2 + (Vector2)(transform.position - player.position);
                    count++;
                } while (CheckBoundaries(waypoint) && count < 20);

                if (count >= 20)
                {
                    do
                    {
                        waypoint = (Vector2)transform.position + Random.insideUnitCircle * maxDistance;
                    } while (CheckBoundaries(waypoint));
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1.0f, 0.8840241f, 0.168f, 1.0f);

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