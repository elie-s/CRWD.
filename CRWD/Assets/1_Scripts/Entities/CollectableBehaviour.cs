using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    public class CollectableBehaviour : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private UnityEvent onCollected = default;
        private bool collected = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collected) return;

            if (collision.tag == playerTag)
            {
                Collect();
            }
        }

        [ContextMenu("Collect")]
        private void Collect()
        {
            onCollected.Invoke();
            collected = true;
        }
    }
}