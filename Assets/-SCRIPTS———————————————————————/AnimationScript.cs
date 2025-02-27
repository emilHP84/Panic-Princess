using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public enum LoopType { None, Repeat, Yoyo }
    [SerializeField] LoopType loopType;
    [SerializeField][Range(0.1f, 12f)] float speed = 4f;
    float chrono = 0;
    int index = 0;
    bool reverse = false;
    bool paused = false;
    IAnimable anim;
    bool error = false;

    void Awake()
    {
        if (TryGetComponent<IAnimable>(out IAnimable inter))
            anim = inter;
        else Debug.Log("Erreur : " + transform.name + " n'a pas d'animation");
    }

    void OnEnable()
    {
        if (anim == null) Debug.Log("Erreur : " + transform.name + " n'a pas d'animation");
        chrono = 0;
        index = 0;
        paused = false;
        SetSprite();
    }

    void SetSprite()
    {
        if (anim.SetSprite(index) == false)
        {
            if (index < 0)
            {
                reverse = false;
                index = 1;
                SetSprite();
            }
            else switch (loopType)
                {
                    case LoopType.None: paused = true; break;
                    case LoopType.Repeat: index = 0; SetSprite(); break;
                    case LoopType.Yoyo: index--; reverse = true; SetSprite(); break;
                }
        }
    }




    void Update()
    {
        if (paused == false) chrono += Time.deltaTime;
        if (chrono > 1f / speed)
        {
            chrono = 0;
            if (reverse) index--; else index++;
            SetSprite();
        }
    }
} // FIN DU SCRIPT

public interface IAnimable
{
    public bool SetSprite(int index);
}
