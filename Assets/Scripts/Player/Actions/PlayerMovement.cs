using UnityEngine;

namespace Player
{
    /// <summary>
    /// This class has the functionality of the playerMovement
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        public void MovePlayer(Vector2 movementInput)
        {
            movementInput = movementInput.normalized;

            Vector2 movementVector = movementInput * movementSpeed * Time.deltaTime;

            transform.position += (Vector3)movementVector;
        }
    }
}

