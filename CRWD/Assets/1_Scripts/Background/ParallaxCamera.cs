using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD.World
{
    public class ParallaxCamera : MonoBehaviour
    {
        [SerializeField] private ScoreData score = default;
        [SerializeField] private Transform player = default;
        [SerializeField, Range(0.0f, 0.5f)] private float lerpForce = 0.1f;
        [SerializeField] private Vector3 scorePos = new Vector3(0.0f, -20.0f, -10.0f);
        [SerializeField] private AnimationCurve getToScoreCurve = default;
        [SerializeField] private float endMovementDuration = 2.0f;

        private bool follow = true;
        private bool isMoving = false;

        void Update()
        {
            if (follow) FollowPlayer();
            else if (Input.GetKeyDown(KeyCode.Space)) BackToGame();
        }

        private void FollowPlayer()
        {
            transform.position = Vector2.Lerp(Vector2.zero, player.position, lerpForce);
            transform.position += Vector3.forward * -10.0f;
        }

        public void DisplayScore()
        {
            if (!isMoving) StartCoroutine(DisplayScoreRoutine());
        }

        public void BackToGame()
        {
            if (!isMoving) StartCoroutine(BackToPlayRoutine());
        }

        private IEnumerator DisplayScoreRoutine()
        {
            Debug.Log("DisplayScore");
            score.state = ScoreData.State.Score;
            isMoving = true;
            Evaluation evaluation = new Evaluation(endMovementDuration);
            Vector3 startPos = transform.position;
            follow = false;

            while (evaluation.isBelowOne)
            {
                transform.position = Vector3.Lerp(startPos, scorePos, getToScoreCurve.Evaluate(evaluation.fraction));

                yield return evaluation.YieldIncrement();
            }

            transform.position = scorePos;
            isMoving = false;
        }

        private IEnumerator BackToPlayRoutine()
        {
            Debug.Log("Backgame");
            isMoving = true;
            Evaluation evaluation = new Evaluation(endMovementDuration);           

            while (evaluation.isBelowOne)
            {
                transform.position = Vector3.Lerp(scorePos, (Vector3)Vector2.Lerp(Vector2.zero, player.position, lerpForce) + Vector3.forward * -10.0f, getToScoreCurve.Evaluate(evaluation.fraction));

                yield return evaluation.YieldIncrement();
            }

            transform.position = (Vector3)Vector2.Lerp(Vector2.zero, player.position, lerpForce) + Vector3.forward * -10.0f;

            follow = true;
            isMoving = false;
            score.state = ScoreData.State.Wait;
        }
    }
}