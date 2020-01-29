using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CRWD
{
    public class PostprocessManager : MonoBehaviour
    {
        [SerializeField] private AnimationCurve bloomCurve = default;
        [SerializeField] private float bloomMax = 40.0f;
        [SerializeField] private float feedbackDuration = 0.2f;
        [SerializeField, DrawScriptable] private PostProcessProfile postProcess = default;

        private IEnumerator BloomBweepRoutine()
        {
            float timer = 0.0f;
            Bloom bloom = postProcess.settings[0] as Bloom;
            LensDistortion lens = postProcess.settings[1] as LensDistortion;

            float baseIntensity = 4.0f;
            float baseLens = 0.0f;

            while (timer < feedbackDuration)
            {
                bloom.intensity.value = Mathf.Lerp(baseIntensity, bloomMax, bloomCurve.Evaluate(timer / feedbackDuration));
                lens.intensity.value = Mathf.Lerp(baseLens, 20, bloomCurve.Evaluate(timer / feedbackDuration));

                yield return null;
                timer += Time.deltaTime;
            }

            bloom.intensity.value = baseIntensity;
            lens.intensity.value = baseLens;
        }

        public void BweepFeedback()
        {
            StartCoroutine(BloomBweepRoutine());
        }
    }
}