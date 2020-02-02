using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    public class EventsManager : MonoBehaviour
    {
        [SerializeField] private EventsLinker eventsLinker = default;
        [SerializeField] private UnityEvent onLevelStart = default;
        [SerializeField] private UnityEvent onLevelEnd = default;
        [SerializeField] private UnityEvent onPhaseStart = default;
        [SerializeField] private UnityEvent onPhaseEnd = default;
        [SerializeField] private UnityEvent onPhaseTransitionStart = default;
        [SerializeField] private UnityEvent onPhaseTransitionEnd = default;
        [SerializeField] private UnityEvent onBweepCollected = default;
        [SerializeField] private UnityEvent onRepulsorContact = default;
        [SerializeField] private UnityEvent onScoreDataChanged = default;
        [SerializeField] private UnityEvent onLevelTransitionStart = default;
        [SerializeField] private UnityEvent onLevelTransitionEnd = default;
        [SerializeField] private UnityEvent onWorldStart = default;
        [SerializeField] private UnityEvent onWorldEnd = default;

        private void Awake()
        {
            eventsLinker.Link(onLevelStart,
                              onLevelEnd,
                              onPhaseStart,
                              onPhaseEnd,
                              onPhaseTransitionStart,
                              onPhaseTransitionEnd,
                              onBweepCollected,
                              onRepulsorContact,
                              onScoreDataChanged, 
                              onLevelTransitionStart,
                              onLevelTransitionEnd,
                              onWorldStart,
                              onWorldEnd);
        }
    }
}