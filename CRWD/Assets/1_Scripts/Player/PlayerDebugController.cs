using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class PlayerDebugController : MonoBehaviour
    {
        [SerializeField] private Captation.CaptationSettings captationSettings;
        [SerializeField] private float speed = 40.0f;

        private Rect refRect;

        private void OnEnable()
        {
            refRect = captationSettings.rectangle;
        }

        private void OnDisable()
        {
            captationSettings.rectangle = refRect;
        }

        private void Update()
        {
            DebugController();
        }

        private void DebugController()
        {
            float x = Input.GetKey(KeyCode.LeftArrow) ? Time.deltaTime * speed : (Input.GetKey(KeyCode.RightArrow) ? -Time.deltaTime * speed : 0.0f);
            float y = Input.GetKey(KeyCode.DownArrow) ? Time.deltaTime * speed : (Input.GetKey(KeyCode.UpArrow) ? -Time.deltaTime * speed : 0.0f);

            captationSettings.rectangle.Set(captationSettings.rectangle.x + x, captationSettings.rectangle.y + y, captationSettings.rectangle.width, captationSettings.rectangle.height);
        }
    }
}