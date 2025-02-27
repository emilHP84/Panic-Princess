using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Animation2D : MonoBehaviour, IAnimable
{
    SpriteRenderer randy;
    Sprite[] spritesFace, spritesBack, spritesLeft, spritesRight;
    Sprite[] angleSprites;
    AngleToPlayerScript angleScript;

    void Awake()
    {
        angleScript = GetComponentInParent<AngleToPlayerScript>();
        if (spritesFace.Length < 1 || spritesBack.Length < 1 || spritesLeft.Length < 1 || spritesRight.Length < 1) Destroy(this);
        randy = GetComponent<SpriteRenderer>();
    }

    public bool SetSprite(int index)
    {
        switch (angleScript.facing)
        {
            case AngleToPlayerScript.Angle.Face: angleSprites = spritesFace; break;
            case AngleToPlayerScript.Angle.Back: angleSprites = spritesBack; break;
            case AngleToPlayerScript.Angle.Left: angleSprites = spritesLeft; break;
            case AngleToPlayerScript.Angle.Right: angleSprites = spritesRight; break;
        }


        if (index >= angleSprites.Length || index < 0) return false;
        randy.sprite = angleSprites[index];
        return true;
    }
} // FIN DU SCRIPT
