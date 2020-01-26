using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.Captation
{
    [CreateAssetMenu(menuName = "CRWD/Captation/Settings")]
    public class CaptationSettings : ScriptableObject
    {
        public int webcamIndex = 0;
        public Rect rectangle;
        public int textureSize = 16;
        [Range(-0.5f, 0.5f)] public float ceil = 0.25f;
        [Range(1, 60)] public int rate = 10;

        public bool Resize { get; private set; }
        public bool GrayScale { get; private set; }
        public bool Hollow { get; private set; }

        public void SetTestingValues(bool _resize, bool _grayScale, bool _hollow)
        {
            Resize = _resize;
            GrayScale = _grayScale;
            Hollow = _hollow;
        }
    }
}