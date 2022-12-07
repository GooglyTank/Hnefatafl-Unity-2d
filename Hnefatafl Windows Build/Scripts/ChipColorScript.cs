using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipColorScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite startingSprite;
    public Sprite selectedSprite;

    void Start()
    {
        startingSprite = spriteRenderer.sprite;
    }

    void ChangeSprite()
    {
        if (spriteRenderer.sprite = startingSprite) {
         spriteRenderer.sprite = selectedSprite; 
        } else {
            spriteRenderer.sprite = startingSprite;
        }
    }
}
