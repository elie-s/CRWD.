using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CRWD
{
    public class CaptationWebcamIndexDisplay : MonoBehaviour
    {
        [SerializeField] private string baseText = "Webcam device index: ";
        [SerializeField] private Captation.CaptationSettings settings = default;
        [SerializeField] private TextMeshProUGUI field = default;

        // Update is called once per frame
        void Update()
        {
            field.text = baseText + settings.webcamIndex;
        }
    }
}