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

            if (x < settings.clampedArea.rect.x) x = settings.clampedArea.rect.x;
            else if (x > settings.clampedArea.rect.x + settings.clampedArea.rect.width) x = settings.clampedArea.rect.x + settings.clampedArea.rect.width;

            if (y < settings.clampedArea.rect.y) y = settings.clampedArea.rect.y;
            else if (y > settings.clampedArea.rect.y + settings.clampedArea.rect.height) y = settings.clampedArea.rect.y + settings.clampedArea.rect.height;

            transform.position = new Vector2(x, y);
        }

        public void Repulse(Vector2 _direction, float _force)
        {
            StopCoroutine(repulsion);
            repulsion = settings.repulsion.RepulsionRoutine(transform, _direction, _force);
            StartCoroutine(repulsion);
        }

        private void OnDrawGizmosSelected()
        {
            Rect area = settings.clampedArea.rect;
            Gizmos.color = Color.red;

            Gizmos.DrawLine(new Vector3(area.x, area.y), new Vector3(area.x, area.y + area.height));
            Gizmos.DrawLine(new Vector3(area.x + area.width, area.y + area.height), new Vector3(area.x, area.y + area.height));
            Gizmos.DrawLine(new Vector3(area.x + area.width, area.y + area.height), new Vector3(area.x + area.width, area.y));
            Gizmos.DrawLine(new Vector3(area.x, area.y), new Vector3(area.x + area.width, area.y));
        }
    }
}