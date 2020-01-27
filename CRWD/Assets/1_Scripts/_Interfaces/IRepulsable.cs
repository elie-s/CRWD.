using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public interface IRepulsable
    {
        void Repulse(Vector2 _direction, float _force);
    }
}