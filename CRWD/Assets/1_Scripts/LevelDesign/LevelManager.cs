using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRWD
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private EventsLinker eventsLinker = default;
        [SerializeField] private ScoreData score = default;
        [SerializeField] private PhaseManager phases = default;


    }
}