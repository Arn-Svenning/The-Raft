using UnityEngine;

namespace Fish
{
    [RequireComponent(typeof(FishMovement))]
    public class Fish : MonoBehaviour
    {
        #region Fish Components

        private FishMovement fishMovement;

        #endregion
        void Start()
        {
            fishMovement = GetComponent<FishMovement>();
        }

        void Update()
        {
            fishMovement.MoveFish();
        }
    }
}

