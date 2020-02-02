using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRWD.Captation
{
    public class CalibrationHandler : MonoBehaviour
    {
        [SerializeField] private bool resize = false;
        [SerializeField] private bool grayScale = false;
        [SerializeField] private bool hollow = false;
        [SerializeField] private float speed = 50.0f;
        [SerializeField] private RawImage wholeWebcam = default;
        [SerializeField] private RectTransform captationRect = default;
        [SerializeField] private RawImage captationArea = default;
        [SerializeField] private CaptationData data = default;
        [SerializeField] private Slider pixelization = default;
        [SerializeField] private Slider ceilSlider = default;
        [SerializeField] private Slider webcamIndex = default;
        [SerializeField, DrawScriptable] private CaptationSettings settings = default;

        void Update()
        {
            settings.SetTestingValues(resize, grayScale, hollow);
            Display();
            AreaHandler();
            if (Input.GetKeyDown(KeyCode.R)) ResetArea();
            PixelSlider();
            CeilHandler();
        }

        private void Display()
        {
            wholeWebcam.rectTransform.sizeDelta = new Vector2(data.webcam.width, data.webcam.height);
            wholeWebcam.rectTransform.anchoredPosition = new Vector2(-data.webcam.width * 0.5f, -data.webcam.height * 0.5f);

            if (data.camFail)
            {
                wholeWebcam.texture = new Texture2D(data.webcam.width, data.webcam.height);
            }
            else
            {
                wholeWebcam.texture = data.webcam;
            }

            captationRect.sizeDelta = new Vector2(settings.rectangle.width, settings.rectangle.height);
            captationRect.anchoredPosition = new Vector2(-settings.rectangle.width * 0.5f + settings.rectangle.x, -settings.rectangle.height * 0.5f + settings.rectangle.y);
            captationArea.texture = data.texture;
        }

        public void UpdateWebcamDevice()
        {
            settings.webcamIndex = (int)webcamIndex.value;
            data.SetWebcam(settings.webcamIndex);
        }

        public void Resize()
        {
            resize = !resize;
        }

        public void Shape()
        {
            grayScale = !grayScale;
        }

        public void FullTexture()
        {
            hollow = !hollow;
        }

        public void AreaHandler()
        {
            float x = Input.GetKey(KeyCode.LeftArrow) ? -Time.deltaTime * speed : (Input.GetKey(KeyCode.RightArrow) ? Time.deltaTime * speed : 0.0f);
            float y = Input.GetKey(KeyCode.DownArrow) ? -Time.deltaTime * speed : (Input.GetKey(KeyCode.UpArrow) ? Time.deltaTime * speed : 0.0f);
            float scale = Input.GetKey(KeyCode.KeypadPlus) ? Time.deltaTime * speed / 2.0f : (Input.GetKey(KeyCode.KeypadMinus) ? -Time.deltaTime * speed / 2.0f : 0.0f);

            settings.rectangle.Set(settings.rectangle.x + x, settings.rectangle.y + y, settings.rectangle.width + scale, settings.rectangle.height + scale);
        }

        public void ResetArea()
        {
            settings.rectangle.Set(0.0f, 0.0f, 100.0f, 100.0f);
        }

        public void PixelSlider()
        {
            settings.textureSize = 16 + (int)pixelization.value * 8;
        }

        public void CeilHandler()
        {
            settings.ceil = ceilSlider.value;
        }
    }
}