using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public abstract class MovementSettings : ScriptableObject
    {
        public abstract IEnumerator MovementRoutine(Transform _transform, Vector2 _origin);

        public abstract void DrawGizmos(Transform _transform);
        public abstract void DrawGizmos(Vector2 _origin);
    }
}