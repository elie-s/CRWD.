using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.Captation
{
    [CreateAssetMenu(menuName = "CRWD/Captation/Data")]
    public class CaptationData : ScriptableObject
    {
        public Texture2D texture;
        public Sprite sprite;
        public WebCamTexture webcam;

        public void SetWebcam(int _index)
        {
            webcam = new WebCamTexture(WebCamTexture.devices[_index].name);
            webcam.Play();
        }
    }
}