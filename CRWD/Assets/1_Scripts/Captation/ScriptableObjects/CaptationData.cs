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
        public Vector2 direction;
        public int capted;
        public float ratio;
        public bool nullDetection;
        public bool camSet;
        public bool camFail;

        //private List<System.Action> onCaptationUpdate;

        public void Init(CaptationSettings _settings)
        {
            //onCaptationUpdate = new List<System.Action>();
            texture = new Texture2D((int)_settings.rectangle.width, (int)_settings.rectangle.height);
            SetWebcam(_settings.webcamIndex);
        }

        public void SetWebcam(int _index)
        {
            if (_index >= WebCamTexture.devices.Length)
            {
                Debug.Log("Webcam out of range.");
                webcam.Stop();
                camFail = true;
                return;
            }

            if (camSet) webcam.Stop();

            webcam = new WebCamTexture(WebCamTexture.devices[_index].name);
            webcam.Play();
            camSet = true;
            camFail = false;
        }

        //public void OnCaptationUpdateUnregister(System.Action _action)
        //{
        //    onCaptationUpdate.Remove(_action);
        //}

        //public void OnCaptationUpdateCall()
        //{
        //    foreach (System.Action action in onCaptationUpdate)
        //    {
        //        action();
        //    }
        //}

        public void SetSprite()
        {
            if (!nullDetection)
            {
                sprite = CreateSPrite(texture);
                sprite.texture.filterMode = FilterMode.Point;
                sprite.texture.Apply();

                direction = PivotDirection(sprite);
            }
            else
            {
                direction = Vector2.zero;
                Debug.LogWarning("Webcam no longer detects any shape !");
            }
        }

        private Sprite CreateSPrite(Texture2D _texture)
        {
            Vector2 pivot = Pivot(_texture);

            return Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), pivot, 100, 0, SpriteMeshType.FullRect, Vector4.one, true);
        }

        private Vector2 Pivot(Texture2D _texture)
        {
            Vector2 result = Vector2.zero;
            Color[] colors = _texture.GetPixels();

            int width = _texture.width;
            int height = _texture.height;
            int amount = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (colors[x + y * width].a > 0.5f)
                    {
                        result += new Vector2(x, y);
                        amount++;
                    }
                }
            }

            capted = amount;
            ratio = (float)amount / (float)(width * height);

            result /= amount;
            result = new Vector2(result.x / width, result.y / height);

            return result;
        }

        private Vector2 PivotDirection(Sprite _sprite)
        {
            Vector2 pivot = _sprite.pivot;
            Rect rect = _sprite.rect;
            float average = (Mathf.Abs((pivot.x / rect.width * 2 - 1)) + Mathf.Abs(pivot.y / rect.height * 2 - 1));

            return new Vector2(pivot.x * 2 - rect.width, pivot.y * 2 - rect.height).normalized * average;
        }
    }
}