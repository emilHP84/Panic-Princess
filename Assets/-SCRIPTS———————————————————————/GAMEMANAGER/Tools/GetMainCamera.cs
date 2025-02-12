using UnityEngine;

public class GetMainCamera : MonoBehaviour
{
    void LateUpdate()
    {
        GAME.MANAGER.gameCam.position = transform.position;
        GAME.MANAGER.gameCam.rotation = transform.rotation;
    }
} // SCRIPT END
