using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class PlayerController : MonoBehaviour, IRepulsable
    {
        [SerializeField] private Captation.CaptationData data = default;
        [SerializeField, DrawScriptable] private PlayerSettings settings = default;

        private IEnumerator repulsion;

        private void OnEnable()
        {
            repulsion = RepulsionRoutine(Vector2.zero, 0.0f);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 direction = new Vector3(data.direction.x, data.direction.y, 0.0f);
            transform.position += direction * Time.deltaTime * settings.speed * settings.ratioSizeSpeedCurve.Evaluate(data.ratio);

            ClampArea();
        }

        private void ClampArea()
        {
            float x = transform.position.x; //Mathf.Clamp(transform.position.x, area.xMax, area.xMin);
            float y = transform.position.y; // Mathf.Clamp(transform.position.y, area.yMax, area.yMin);

            if (x < settings.clampedArea.x) x = settings.clampedArea.x;
            else if (x > settings.clampedArea.x + settings.clampedArea.width) x = settings.clampedArea.x + settings.clampedArea.width;

            if (y < settings.clampedArea.y) y = settings.clampedArea.y;
            else if (y > settings.clampedArea.y + settings.clampedArea.height) y = settings.clampedArea.y + settings.clampedArea.height;

            transform.position = new Vector2(x, y);
        }

        public void Repulse(Vector2 _direction, float _force)
        {
            StopCoroutine(repulsion);
            repulsion = RepulsionRoutine(_direction, _force);
            StartCoroutine(repulsion);
        }

        private IEnumerator RepulsionRoutine(Vector2 _direction, float _force)
        {
            float timer = 0.0f;

            while (timer < settings.repulsionDuration)
            {
                transform.position += (Vector3)_direction * Time.deltaTime * _force * settings.repulsionCurve.Evaluate(timer / settings.repulsionDuration);

                yield return null;
                timer += Time.deltaTime;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Rect area = settings.clampedArea;
            Gizmos.color = Color.red;

            Gizmos.DrawLine(new Vector3(area.x, area.y), new Vector3(area.x, area.y + area.height));
            Gizmos.DrawLine(new Vector3(area.x + area.width, area.y + area.height), new Vector3(area.x, area.y + area.height));
            Gizmos.DrawLine(new Vector3(area.x + area.width, area.y + area.height), new Vector3(area.x + area.width, area.y));
            Gizmos.DrawLine(new Vector3(area.x, area.y), new Vector3(area.x + area.width, area.y));
        }
    }
}