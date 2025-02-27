using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Animation3D : MonoBehaviour, IAnimable
{
    [SerializeField] Texture[] spritesFace,spritesBack,spritesLeft,spritesRight;
    MeshRenderer randy;
    int textureID = 0;
    AngleToPlayerScript angleScript;
    Texture[] angleSprites;
    public GameObject epee;

    void Awake()
    {
        angleScript = GetComponentInParent<AngleToPlayerScript>();
        if (spritesFace.Length<1 || spritesBack.Length<1 || spritesLeft.Length<1 || spritesRight.Length<1) Destroy(this);
        randy = GetComponent<MeshRenderer>();
        textureID = Shader.PropertyToID("_BaseMap");
    }

    public bool SetSprite(int index)
    {
        switch (angleScript.facing)
        {
            case AngleToPlayerScript.Angle.Face: angleSprites = spritesFace; epee.SetActive(true); break;
            case AngleToPlayerScript.Angle.Back: angleSprites = spritesBack; epee.SetActive(false); break;
            case AngleToPlayerScript.Angle.Left: angleSprites = spritesLeft; epee.SetActive(false); break;
            case AngleToPlayerScript.Angle.Right: angleSprites = spritesRight;epee.SetActive(false); break;
        }

        if (index>=angleSprites.Length || index<0) return false;
        randy.material.SetTexture(textureID, angleSprites[index]);
        return true;
    }
} // FIN DU SCRIPT
