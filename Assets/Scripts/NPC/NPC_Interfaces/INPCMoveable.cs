using UnityEngine;

namespace NPC
{
    public interface INPCMoveable
    {
        public Rigidbody2D NPCRigidBody { get; set; }
        public bool isFacingRight{get; set;}
        public void MoveNPC(Vector2 velocity);
        public void CheckForFacingRightOrLeft(Vector2 velocity);
    }
}

