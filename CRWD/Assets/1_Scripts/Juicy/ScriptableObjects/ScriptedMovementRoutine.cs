using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    [CreateAssetMenu(menuName = "CRWD/Scripted Behaviours/Movement Routine")]

    public class ScriptedMovementRoutine : ScriptedBehaviourRoutine
    {
        [SerializeField] private float duration = 1.0f;
        [SerializeField] private AnimationCurve movementCurve = default;
        [SerializeField] private Direction direction = default;
        [SerializeField] private Mode mode = default;
        [SerializeField, HideInInspector] private string nameToFind = "";
        [SerializeField, HideInInspector] private Rect randomInside = default;
        [SerializeField, HideInInspector] private Vector2 position = default;

        public override IEnumerator Play(Component _component)
        {
            Transform transform = _component as Transform;
            Vector2 origin = transform.position;
            Vector2 destination = transform.position;
            float timer = 0.0f;

            if(direction == Direction.From)
            {
                switch (mode)
                {
                    case Mode.FindName:
                        origin = GameObject.Find(nameToFind).transform.position;
                        break;
                    case Mode.RandomPosition:
                        origin = RandomInsideRect();
                        break;
                    case Mode.SetPosition:
                        origin = position;
                        break;
                }
            }
            else if (direction == Direction.To)
            {
                switch (mode)
                {
                    case Mode.FindName:
                        destination = GameObject.Find(nameToFind).transform.position;
                        break;
                    case Mode.RandomPosition:
                        destination = RandomInsideRect();
                        break;
                    case Mode.SetPosition:
                        destination = position;
                        break;
                }
            }

            while (timer < duration)
            {
                transform.position = Vector2.Lerp(origin, destination, movementCurve.Evaluate(timer / duration));

                yield return null;
                timer += Time.deltaTime;
            }

            transform.position = destination;
        }

        private Vector2 RandomInsideRect()
        {
            float x = randomInside.x + Random.Range(0.0f, randomInside.width);
            float y = randomInside.y + Random.Range(0.0f, randomInside.height);

            return new Vector2(x, y);
        }

        public enum Direction { From, To}
        public enum Mode { FindName, RandomPosition, SetPosition}
    }
}