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
        [SerializeField] private RawImage wholeWebcam = default;
        [SerializeField] private RawImage captationArea = default;
        [SerializeField] private CaptationData data = default;
        [SerializeField, DrawScriptable] private CaptationSettings settings = default;

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            settings.SetTestingValues(resize, grayScale, hollow);
            Display();
        }

        private void Display()
        {
            wholeWebcam.rectTransform.sizeDelta = new Vector2(data.webcam.width, data.webcam.height);
            wholeWebcam.rectTransform.anchoredPosition = new Vector2(-data.webcam.width * 0.5f, -data.webcam.height * 0.5f);
            wholeWebcam.texture = data.webcam;

            captationArea.rectTransform.sizeDelta = new Vector2(settings.rectangle.width, settings.rectangle.height);
            captationArea.rectTransform.anchoredPosition = new Vector2(-settings.rectangle.width * 0.5f + settings.rectangle.x, -settings.rectangle.height * 0.5f + settings.rectangle.y);
            captationArea.texture = data.texture;
        }
    }
}