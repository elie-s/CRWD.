using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.World
{
    [CreateAssetMenu()]
    public class BackgroundSettings : ScriptableObject
    {
        public GameObject elementPrefab = default;
        public float sizeFactor = 0.25f;
        public int amount = 100;
        public AnimationCurve sizeDistribution = default;
        public AnimationCurve lerpDistribution = default;
        public Gradient depthGradient = default;
        public float maxSize = 10.0f;
        public Rect centerRect = new Rect(-4.0f, -2.0f, 8.0f, 4.0f);
        public Rect largeRect = new Rect(-6.5f, -3.5f, 13.0f, 7.0f);
    }
}