using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD.Captation
{
    public class CaptationHandler : MonoBehaviour
    {
        [SerializeField] private bool isPlayScene = false;
        [SerializeField] private CaptationData data = default;
        [SerializeField] private UnityEvent onCaptationUpdate = default;
        [SerializeField, DrawScriptable] private CaptationSettings settings = default;

        private bool nullDetection = false;

        private void OnEnable()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            data.Init(settings);
            if (isPlayScene) settings.SetTestingValues(true, true, false);

            StartCoroutine(UpdateTextureRoutine());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator UpdateTextureRoutine()
        {
            UpdateTexture();
            //data.OnCaptationUpdateCall();
            if(!nullDetection) onCaptationUpdate.Invoke();

            yield return new WaitForSeconds((1.0f / settings.rate));

            StartCoroutine(UpdateTextureRoutine());
        }

        public void UpdateTexture()
        {
            GetWebcamTexture();
            ApplyModifications();
        }

        private void GetWebcamTexture()
        {
            Rect tmpRect = new Rect(data.webcam.width / 2 - settings.rectangle.width / 2 + settings.rectangle.x,
                                    data.webcam.height / 2 - settings.rectangle.height / 2 + settings.rectangle.y,
                                    settings.rectangle.width,
                                    settings.rectangle.height);
            data.texture = new Texture2D((int)tmpRect.width, (int)tmpRect.height);
            data.texture.SetPixels(data.webcam.GetPixels((int)tmpRect.x, (int)tmpRect.y, (int)tmpRect.width, (int)tmpRect.height));
            data.texture.Apply();
        }

        private void ApplyModifications()
        {
            if (settings.Resize) TextureScale.Point(data.texture, settings.textureSize, settings.textureSize);
            if (settings.GrayScale) data.texture.SetPixels(ConvertContrasts(data.texture.GetPixels()));
            data.texture.filterMode = FilterMode.Point;
            data.texture.Apply();
        }

        private Color[] ConvertContrasts(Color[] _colors)
        {
            nullDetection = true;
            Color[] result = new Color[_colors.Length];
            float[] values = new float[_colors.Length];

            float average = 0.0f;

            for (int i = 0; i < result.Length; i++)
            {
                float value = (_colors[i].r + _colors[i].b + _colors[i].g) / 3.0f;

                average += value;

                values[i] = value;
            }

            average /= result.Length;

            for (int i = 0; i < result.Length; i++)
            {
                float value = values[i] > average - settings.ceil ? 0.0f : 1.0f;
                value = settings.Hollow ? 1.0f - value : value;
                result[i] = new Color(value, value, value, settings.Hollow ? 1.0f : value);
                if (value > 0.5f) nullDetection = false;
            }

            return result;
        }

        private void OnApplicationQuit()
        {
            data.camSet = false;
        }
    }
}