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
        public RectObject centerRect = default;
        public RectObject largeRect = default;
    }
}