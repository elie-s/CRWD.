﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Movements/Round")]
    public class CircleMovement : MovementSettings
    {
        [SerializeField] private float radius = 1.0f;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private bool clockwise = false;

        public override IEnumerator MovementRoutine(Transform _transform, Vector2 _origin)
        {
            float angle = 0.0f;

            while (true)
            {
                _transform.position = new Vector2((float)System.Math.Round(radius * Mathf.Cos(angle * Mathf.Deg2Rad), 6) + _origin.x, (float)System.Math.Round(radius * Mathf.Sin(angle * Mathf.Deg2Rad), 6) + _origin.y);
                IncrementAngle(ref angle);
                yield return null;
            }
        }

        private void IncrementAngle(ref float _angle)
        {

            if (!clockwise)
            {
                _angle += Time.deltaTime * speed;

                if (_angle > 180) _angle = (_angle - 180) - 180;
            }
            else
            {
                _angle -= Time.deltaTime * speed;

                if (_angle < -180) _angle = _angle+360;
            }
        }

        public override void DrawGizmos(Transform _transform)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(_transform.position, radius);
        }

        public override void DrawGizmos(Vector2 _origin)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(_origin, radius);
        }
    }
}