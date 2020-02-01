using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Repulsors/Bludger Settings")]
    public class BludgerSettings : ScriptableObject
    {
        [Header("General")]
        public float rotatingSpeed = 5.0f;
        public Gradient chargeGradient = default;
        [Header("Charging State")]
        public float chargingDuration = 5.0f;
        public AnimationCurve rotationAccelerationCurve = default;
        [Range(0.01f, 1.0f)] public float chargedScaleFactor = 0.5f;
        [Header("Rushing State")]
        public AnimationCurve celerityCurve = default;
        public float rushDuration = 2.0f;
        public float rushDistance = 15.0f;
        public float bearingCorrectionFOV = 20.0f;
        [Range(0.0f, 0.25f)] public float bearingCorrection = default; 
        [Header("Idle State")]
        public float idleDuration = 1.5f;
        [Header("Collision")]
        public AnimationCurve feedbackCurve = default;
        public float feedbackDuration = 0.4f;
        public float slowdownDuration = 1.0f;
        public AnimationCurve slowdownCurve = default;
        public float speedAsForceModifier = 1.0f;
        public float baseForce = 5.0f;

    }
}