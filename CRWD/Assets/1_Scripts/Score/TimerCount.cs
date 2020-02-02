using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CRWD
{
    public class TimerCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI display = default;

        [SerializeField] private float value = 0.0f;
        public int minutes => Mathf.FloorToInt(value / 60);
        public int secondes => Mathf.FloorToInt(value) - minutes * 60;

        private IEnumerator timerRoutine;
        private int minPassed = 0;
        private int secPassed = 0;

        private void Update()
        {
            if (display) display.text = ToString();
        }

        [ContextMenu("Play")]
        public void Play()
        {
            timerRoutine = StartTimer();

            StartCoroutine(timerRoutine);
        }

        [ContextMenu("Stop")]
        public void Stop()
        {
            StopCoroutine(timerRoutine);
        }

        [ContextMenu("Reset")]
        public void Restart()
        {
            minPassed = minutes;
            secPassed = secondes;

            StartCoroutine(timerRoutine);
        }

        public float GetValue()
        {
            return value;
        }

        private IEnumerator StartTimer()
        {
            value = 0.0f;

            while (true)
            {
                yield return null;
                value += Time.deltaTime;
            }
        }

        public override string ToString()
        {
            return ((minutes - minPassed) < 10 ? "0" + (minutes - minPassed).ToString() : (minutes - minPassed).ToString()) + ":" + ((secondes - secPassed) < 10 ? "0" + (secondes - secPassed).ToString() : (secondes - secPassed).ToString());
        }
    }
}