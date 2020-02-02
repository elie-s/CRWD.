using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/LD/Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        public float delay = 1.5f;
        public Gradient uiFadeInGradient = default;
        public Gradient uiFadeOutGradient = default;
        public Gradient uiTextsFadeInGradient = default;
        public Gradient uiTextsFadeOutGradient = default;
        public float mainUIFadeDuration = 1.0f;
        public float cleanDelay = 2.0f;
    }
}