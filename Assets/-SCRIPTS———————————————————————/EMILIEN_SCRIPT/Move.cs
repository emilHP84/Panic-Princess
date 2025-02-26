using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    public void MovingOnValue(Vector3 vector)
    {
        gameObject.transform.position += vector * speed * Time.deltaTime;
    }
}
