using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSprite : MonoBehaviour
{
    [SerializeField] private SOCharacterBody characterBody;
    [SerializeField] private string bodyPart;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        foreach(BodyPart part in characterBody.characterBodyParts)
        {
            if (part.bodyPart.bodyPartName.Contains("No"))
            {
                spriteRenderer.sprite = null;
            }
        }
    }
}
