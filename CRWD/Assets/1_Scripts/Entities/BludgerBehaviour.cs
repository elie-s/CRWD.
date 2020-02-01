using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class BludgerBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer render = default;
        [SerializeField] private AudioSource sfx = default;
        [SerializeField] private TrailRenderer trail = default;
        [SerializeField, DrawScriptable] private BludgerSettings settings = default;

        private Transform player = default;
        private Vector2 direction = Vector2.zero;
        private Vector3 startScale = Vector3.zero;
        private float spinningSpeed = 0.0f;
        private float speed = 0.0f;
        private float gradientValue = 0.0f;


        void OnEnable()
        {
            if (!player) player = GameObject.Find("Player").transform;
            render.color = settings.chargeGradient.Evaluate(0.0f);
            startScale = transform.localScale;
        }

        private void Update()
        {
            Spin();
            TrailHandler();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                StopRoutine();
                StartCoroutine(CollisionRoutine(collision.GetComponent<IRepulsable>(), (collision.transform.position - transform.position).normalized));
                sfx?.Play();
            }
        }

        private IEnumerator CollisionRoutine(IRepulsable _repulsable, Vector2 _direction)
        {
            Evaluation evaluation = new Evaluation(settings.slowdownDuration);
            float refSpeed = speed;
            Vector3 refScale = transform.localScale;
            float refSpinningSpeed = spinningSpeed;
            float refGradient = gradientValue;

            _repulsable?.Repulse(_direction, settings.baseForce + speed * settings.speedAsForceModifier);

            while (evaluation.isBelowOne)
            {
                float slowdown = settings.slowdownCurve.Evaluate(evaluation.fraction);
                speed = Mathf.Lerp(refSpeed, 0.0f, slowdown);
                spinningSpeed = Mathf.Lerp(refSpinningSpeed, 0.0f, slowdown);
                gradientValue = Mathf.Lerp(refGradient, 0.0f, slowdown);
                render.color = settings.chargeGradient.Evaluate(gradientValue);
                transform.localScale = Vector3.Lerp(refScale, startScale, slowdown);

                yield return evaluation.YieldIncrement();
            }

            StartCoroutine(ChargingRoutine());
        }

        [ContextMenu("Start Routine")]
        public void StartRoutine()
        {
            StartCoroutine(ChargingRoutine());
        }

        public void StopRoutine()
        {
            StopAllCoroutines();
        }

        private IEnumerator ChargingRoutine()
        {
            Evaluation evaluation = new Evaluation(settings.chargingDuration);

            while (evaluation.isBelowOne)
            {
                float spin = settings.rotationAccelerationCurve.Evaluate(evaluation.fraction);
                spinningSpeed = spin * settings.rotatingSpeed;
                render.color = settings.chargeGradient.Evaluate(spin);
                gradientValue = spin;
                transform.localScale = Vector3.Lerp(startScale, Vector3.one * settings.chargedScaleFactor, spin);

                yield return evaluation.YieldIncrement();
            }

            spinningSpeed = settings.rotationAccelerationCurve.Evaluate(1.0f) * settings.rotatingSpeed;
            render.color = settings.chargeGradient.Evaluate(1.0f);
            gradientValue = 1.0f;
            transform.localScale = Vector3.one * settings.chargedScaleFactor;

            StartCoroutine(RushRoutine());
        }

        private IEnumerator RushRoutine()
        {
            Evaluation evaluation = new Evaluation(settings.rushDistance);
            direction = (player.position - transform.position).normalized;
            float distance = 0.0f;
            speed = settings.rushDistance / settings.rushDuration;

            while (evaluation.isBelowOne)
            {
                float celerity = settings.celerityCurve.Evaluate(evaluation.fraction);

                if(Vector2.Angle(direction, (player.position - transform.position).normalized) < settings.bearingCorrectionFOV)
                    direction = Vector2.Lerp(direction, (player.position - transform.position).normalized, settings.bearingCorrection);

                distance = speed * Time.deltaTime * celerity;
                transform.position += (Vector3)direction * distance;

                transform.localScale = Vector3.Lerp(Vector3.one * settings.chargedScaleFactor, startScale, 1.0f - celerity);
                render.color = settings.chargeGradient.Evaluate(celerity);
                gradientValue = celerity;
                spinningSpeed = celerity * settings.rotatingSpeed;

                yield return evaluation.YieldIncrement(distance);
            }

            transform.localScale = startScale;
            render.color = settings.chargeGradient.Evaluate(0.0f);
            gradientValue = 0.0f;
            spinningSpeed = 0.0f;

            StartCoroutine(IdleRoutine());
        }

        private IEnumerator IdleRoutine()
        {
            Evaluation evaluation = new Evaluation(settings.idleDuration);

            while (evaluation.isBelowOne)
            {
                yield return evaluation.YieldIncrement();
            }

            StartCoroutine(ChargingRoutine());
        }

        private void Spin()
        {
            render.transform.Rotate(Vector3.forward, spinningSpeed);
        }

        private void TrailHandler()
        {
            //trail.colorGradient.colorKeys[0] = new GradientColorKey(render.color, 0.0f);
            //trail.widthCurve.keys[0] = new Keyframe(0.0f, transform.localScale.x);

            //trail.ren
        }
    }
}