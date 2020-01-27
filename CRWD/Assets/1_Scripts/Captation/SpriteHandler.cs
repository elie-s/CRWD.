using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.Captation
{
    public class SpriteHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sRenderer = default;
        [SerializeField] private CaptationData data = default;

        private void OnEnable()
        {
            data.OnCaptationUpdateRegister(OnCaptationUpdate);
        }

        private void OnDisable()
        {
            data.OnCaptationUpdateUnregister(OnCaptationUpdate);
        }

        void Start()
        {

        }

        void OnCaptationUpdate()
        {
            data.SetSprite();
            sRenderer.sprite = data.sprite;
        }
    }
}