using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Player/Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement")]
        public RectObject clampedArea;
        public AnimationCurve ratioSizeSpeedCurve;
        public Gradient directionGradient;
        public float speed;
        [Header("Repulsion")]
        [DrawScriptable] public RepulsionSettings repulsion;
        
    }
}