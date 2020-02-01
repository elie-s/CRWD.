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

        private void Awake()
        {
            score.ResetData();
        }

        public void UpdateScoreDisplayed()
        {
            displayBweeps.text = score.bweepsCount + " <color=#FFFFFFBB>/" + score.phaseBweepsLimit + "</color>";
        }
    }
}