using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.Captation
{
    public class SpriteHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sRenderer = default;
        [SerializeField] private CaptationData data = default;


        public void OnCaptationUpdate()
        {
            data.SetSprite();
            sRenderer.sprite = data.sprite;
        }
    }
}