using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Scripted Behaviours/Scale Routine")]
    public class ScriptedScaleRoutine : ScriptedBehaviourRoutine
    {
        [SerializeField] private float duration = 0.0f;
        [SerializeField] private float endScale = 1.0f;
        [SerializeField] private AnimationCurve scaleCurve = default;

        public override IEnumerator Play(Component _component)
        {
            Transform transform = _component as Transform;
            float timer = 0.0f;
            float startScale = transform.localScale.x;

            while (timer < duration)
            {
                transform.localScale = Vector3.one * (startScale + scaleCurve.Evaluate(timer / duration) * (endScale - startScale));

                yield return null;
                timer += Time.deltaTime;
            }

            transform.localScale = Vector3.one * endScale;
        }
    }
}