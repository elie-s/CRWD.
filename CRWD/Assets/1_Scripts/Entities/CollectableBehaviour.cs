﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    public class CollectableBehaviour : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private UnityEvent onCollected = default;
        [SerializeField] private bool collected = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collected) return;

            if (collision.tag == playerTag)
            {
                Collect();
            }
        }

        [ContextMenu("Collect")]
        public void Collect()
        {
            onCollected.Invoke();
            collected = true;
        }

        public void Activate()
        {
            collected = false;
        }
    }
}