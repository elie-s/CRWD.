using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.World
{
    public class BackgroundElementManager : MonoBehaviour
    {
        [SerializeField] private Transform camTransform = default;
        [SerializeField, DrawScriptable] private BackgroundSettings settings = default;

        void Start()
        {
            LoadBackground();
        }

        private void LoadBackground()
        {
            int halfAmount = settings.amount / 2;

            for (int i = 0; i < halfAmount; i++)
            {
                SetElement((float)i / (float)halfAmount, RandomPosition(settings.centerRect.rect));
            }

            for (int i = 0; i < halfAmount; i++)
            {
                SetElement((float)i / (float)halfAmount, RandomPosition(settings.largeRect.rect));
            }
        }

        private void SetElement(float _value, Vector2 _pos)
        {
            ParallaxHandler element = Instantiate(settings.elementPrefab, _pos, Quaternion.identity, transform).GetComponent<ParallaxHandler>();
            float size = 1 + Mathf.Round(settings.sizeDistribution.Evaluate(_value) * settings.maxSize);
            element.Initialize(camTransform, size * settings.sizeFactor, settings.lerpDistribution.Evaluate((size - 1) / settings.maxSize), settings.depthGradient.Evaluate((size - 1) / settings.maxSize));
        }

        private Vector2 RandomPosition(Rect _area)
        {
            return new Vector2(Random.Range(_area.x, _area.x + _area.width), Random.Range(_area.y, _area.y + _area.height));
        }
    }
}