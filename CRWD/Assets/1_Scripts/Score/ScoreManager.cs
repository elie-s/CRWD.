using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace CRWD
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private ScoreData score = default;
        [SerializeField] private TextMeshProUGUI displayBweeps = default;
        [SerializeField] private UnityEvent onBweepCollected = default;
        [SerializeField] private UnityEvent onPhaseCompleted = default;
        [SerializeField] private UnityEvent onDataChanged = default;

        private void Awake()
        {
            score.LinkEvents(onBweepCollected, onPhaseCompleted, onDataChanged);
            score.ResetData();
            score.SetPhaseBweepsLimit(2);
        }

        public void UpdateScoreDisplayed()
        {
            displayBweeps.text = score.bweepsCount + " <color=#FFFFFFBB>/" + score.phaseBweepsLimit + "</color>";
        }
    }
}