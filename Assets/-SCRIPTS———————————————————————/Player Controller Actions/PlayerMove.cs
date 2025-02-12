using UnityEngine;
using Rewired;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerLook))]
public class PlayerMove : MonoBehaviour
{
    [Range(0,3)][SerializeField] int playerID = 0;
    public bool CanRun{get{return canRun;}set{canRun=value;}}
    bool canRun=false;
    public bool CanFall{get{return canFall;}set{canFall=value;}}
    bool canFall=false;
    [Range(0,100f)][SerializeField] float maxSpeed = 5f;
    Vector2 inputs;
    Vector3 movement, movementRelativeToCam;
    CharacterController character => GetComponent<CharacterController>();
    PlayerLook lookScript => GetComponent<PlayerLook>();
    Player player;


    void Awake()
    {
        EVENTS.OnGameplay += EnableMoveSet;
        EVENTS.OnGameplayExit += DisableMoveSet;
    }

    void OnDestroy()
    {
        EVENTS.OnGameplay -= EnableMoveSet;
        EVENTS.OnGameplayExit -= DisableMoveSet;
    }

    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    void EnableMoveSet()
    {
        CanRun = CanFall = true;
    }

    void DisableMoveSet()
    {
        CanRun = CanFall = false;
    }



    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
        movementRelativeToCam = lookScript.HorizontalPivot.right *movement.x;
        movementRelativeToCam += lookScript.HorizontalPivot.forward * movement.z;
        movementRelativeToCam += lookScript.HorizontalPivot.up * movement.y;
        character.Move(movementRelativeToCam*Time.deltaTime);
    }

    void GetInputs()
    {
        inputs.x = player.GetAxis("MoveHorizontal");
        inputs.y = player.GetAxis("MoveVertical");
        if (inputs.sqrMagnitude>1f) inputs.Normalize();
    }

    void VerticalMovement()
    {
        movement.y= CanFall ? -60f : 0;
    }

    void HorizontalMovement()
    {
        GetInputs();
        movement.x = CanRun ? inputs.x * maxSpeed : 0;
        movement.z = CanRun ? inputs.y * maxSpeed : 0;
    }


} // SCRIPT END
