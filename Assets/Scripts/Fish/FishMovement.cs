using UnityEngine;
using System.Collections;
using RaftObject;

namespace Fish
{
    /// <summary>
    /// This script moves the fishes and destroys them when their staycountdown has reached 0
    /// </summary>
    public class FishMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private float directionChangeInterval = 2f;

        private float targetAngle;
        private bool isNearRaft = false;
        private GameObject raft;

        private float fishStayDuration = 60f;
        private float fishStayCountdown;
        private bool isFishStayCountdownOver = false;

        private void Start()
        {
            raft = FindAnyObjectByType<Raft>().gameObject;
            StartCoroutine(ChangeDirectionRoutine());

            fishStayCountdown = fishStayDuration;
        }

        private void Update()
        {
            if (!isFishStayCountdownOver)
            {
                MoveFish();
            }
            else
            {
                StopAllCoroutines();
                FishSwimAwayAndDestroy();
            }

            fishStayCountdown -= Time.deltaTime;

            if (fishStayCountdown <= 0)
            {
                isFishStayCountdownOver = true;
            }
        }

        public void MoveFish()
        {
            StayCloseToRaft();
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }

        private IEnumerator ChangeDirectionRoutine()
        {
            while (true)
            {
                if (isFishStayCountdownOver) yield return null;

                targetAngle = Random.Range(0f, 360f);
                yield return new WaitForSeconds(directionChangeInterval);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Raft"))
            {
                isNearRaft = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Raft"))
            {
                isNearRaft = false;
            }
        }

        /// <summary>
        /// When the fish is near the raft, move it randomly
        /// When the fish is not near the raft it moves directly towards the raft until timer has gone down
        /// </summary>
        private void StayCloseToRaft()
        {
            if (isFishStayCountdownOver) return;

            if (isNearRaft)
            {
                float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {

                Vector2 directionTowardRaft = (raft.transform.position - transform.position).normalized;
                float targetAngle = Mathf.Atan2(directionTowardRaft.y, directionTowardRaft.x) * Mathf.Rad2Deg;

                float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        /// <summary>
        /// When fish cooldown is over, the fish swims away outside the screen and gets destroyed
        /// </summary>
        private void FishSwimAwayAndDestroy()
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;

            // Convert fish world position to viewport coordinates (0 to 1 range)
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

            // Check if the fish is completely outside the screen bounds
            if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
            {
                FishSpawner.Instance.DestroyFish(gameObject);
            }
        }

    }
}



