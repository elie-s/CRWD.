using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CRWD
{
    public class DisplayScore : MonoBehaviour
    {
        [SerializeField] private ScoreData score = default;
        [SerializeField] private TextMeshProUGUI levelScore = default;
        [SerializeField] private TextMeshProUGUI totalScore = default;

        void Update()
        {
            levelScore.text = score.scorePhase.ToString();
            totalScore.text = score.scoreTotal.ToString();
        }
    }
}