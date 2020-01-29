using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Scripted Behaviours/Gradient Routine")]
    public class ScriptedGradientRoutine : ScriptedBehaviourRoutine
    {
        [SerializeField] private float duration = 0.0f;
        [SerializeField] private Gradient gradient = default;

        public override IEnumerator Play(Component _component)
        {
            SpriteRenderer sRenderer = _component as SpriteRenderer;
            float timer = 0.0f;

            while (timer < duration)
            {
                sRenderer.color = gradient.Evaluate(timer / duration);
                yield return null;
                timer += Time.deltaTime;
            }

            sRenderer.color = gradient.Evaluate(1.0f);
        }

    }
}