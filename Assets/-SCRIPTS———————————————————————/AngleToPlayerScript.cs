using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AngleToPlayerScript : MonoBehaviour
{
    public enum Angle { Face, Right, Left, Back }
    public Angle facing;
    static Transform player;
    static Vector3 leveledPlayerPos;
    static float angle;

    void Awake()
    {
        player = Camera.main.transform.GetComponentInParent<PlayerLook>().transform;
    }

    void Update()
    {
        leveledPlayerPos = player.position;
        leveledPlayerPos.y = transform.position.y;
        angle = Vector3.SignedAngle(transform.forward, (leveledPlayerPos - transform.position), Vector3.up);

        if (angle > 135f || angle < -135f) facing = Angle.Back;
        else if (angle < -45f) facing = Angle.Left;
        else if (angle > 45f) facing = Angle.Right;
        else facing = Angle.Face;
    }
} // FIN DU SCRIPT