using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    //[CreateAssetMenu(menuName = "CRWD/EventLinker")]
    public class EventsLinker : ScriptableObject
    {
        public UnityEvent onLevelStart { get; private set; }
        public UnityEvent onLevelEnd { get; private set; }
        public UnityEvent onPhaseStart { get; private set; }
        public UnityEvent onPhaseEnd { get; private set; }
        public UnityEvent onPhaseTransitionStart { get; private set; }
        public UnityEvent onPhaseTransitionEnd { get; private set; }
        public UnityEvent onBweepCollected { get; private set; }
        public UnityEvent onRepulsorContact { get; private set; }
        public UnityEvent onScoreDataChanged { get; private set; }
        public UnityEvent onLevelTransitionStart { get; private set; }
        public UnityEvent onLevelTransitionEnd { get; private set; }
        public UnityEvent onWorldStart { get; private set; }
        public UnityEvent onWorldEnd { get; private set; }

        public void Link(params UnityEvent[] _events)
        {
            onLevelStart = _events[0];
            onLevelEnd = _events[1];
            onPhaseStart = _events[2];
            onPhaseEnd = _events[3];
            onPhaseTransitionStart = _events[4];
            onPhaseTransitionEnd = _events[5];
            onBweepCollected = _events[6];
            onRepulsorContact = _events[7];
            onScoreDataChanged = _events[8];
            onLevelTransitionStart = _events[9];
            onLevelTransitionEnd = _events[10];
            onWorldStart = _events[11];
            onWorldEnd = _events[12];
        }
    }
}