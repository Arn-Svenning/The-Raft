using Player;
using RaftObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfOutsideRaft : MonoBehaviour
{
    private PlayerCharacter player;
    private Raft raft;

    private SpriteRenderer raftSpriteRenderer;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerCharacter>();
        raft = FindFirstObjectByType<Raft>();

        raftSpriteRenderer = raft.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(IsOutsideBounds(player.transform, raftSpriteRenderer))
        {
            moveToCenter(player.transform, raftSpriteRenderer);
        }
    }
    bool IsOutsideBounds(Transform target, SpriteRenderer spriteRenderer)
    {
        Bounds bounds = spriteRenderer.bounds;
        return !bounds.Contains(target.position);
    }
    private void moveToCenter(Transform target, SpriteRenderer spriteRenderer)
    {
        Bounds bounds = spriteRenderer.bounds;
        target.position = bounds.center;
    }

}
