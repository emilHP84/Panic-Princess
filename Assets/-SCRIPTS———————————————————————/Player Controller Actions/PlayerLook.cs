using UnityEngine;
using Rewired;

public class PlayerLook : MonoBehaviour
{
    [Range(0,3)][SerializeField] int playerID = 0;
    [SerializeField] Vector2 lookSpeed = Vector2.one;
    [SerializeField] Vector2 mouseSensitivity = Vector2.one *0.1f;
    [Range(0,90f)][SerializeField]float maxVerticalAngle = 85f;
    public bool CanLook{get{return canLook;}set{canLook=value;}}
    bool canLook = false;
    public Transform HorizontalPivot {get{return horizontalPivot;}}
    public Transform VerticalPivot {get{return verticalPivot;}}
    [SerializeField]Transform horizontalPivot,verticalPivot;
    float xAngle,yAngle = 0;
    Player player;
    float inputX,inputY;
    bool MouseInput
    {
    get{
        if (player.id==0 && (ReInput.controllers.Mouse.GetAxisRaw(0)!=0 || ReInput.controllers.Mouse.GetAxisRaw(1)!=0)) return true;
        else return false;
        }
    }

    void Awake()
    {
        EVENTS.OnGameplay += EnableLook;
        EVENTS.OnGameplayExit += DisableLook;
        EVENTS.OnMouseSensitivityChange += ChangeMouseSensitivity;
    }

    void OnDestroy()
    {
        EVENTS.OnGameplay -= EnableLook;
        EVENTS.OnGameplayExit -= DisableLook;
        EVENTS.OnMouseSensitivityChange -= ChangeMouseSensitivity;
    }



    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }


    void EnableLook()
    {
        CanLook = true;
    }

    void DisableLook()
    {
        CanLook = false;
    }

    void ChangeMouseSensitivity(float desired)
    {
        mouseSensitivity = Vector2.one * desired;
    }


    void Update()
    {
        if (CanLook)
        {
            if (horizontalPivot) GetHorizontalAngle();
            if (verticalPivot) GetVerticalAngle();
            ApplyToCamera();
        }
    }

    void GetHorizontalAngle()
    {
        inputX = player.GetAxis("LookHorizontal") * lookSpeed.x;
        if (MouseInput) inputX *= mouseSensitivity.x;
        xAngle +=inputX;
        while (xAngle>360f) xAngle-=360f;
        while (xAngle<-360f) xAngle -= 360f;
    }

    void GetVerticalAngle()
    {
        inputY = -player.GetAxis("LookVertical") * lookSpeed.y;
        if (MouseInput) inputY *= mouseSensitivity.y;
        yAngle += inputY;
        yAngle = Mathf.Clamp(yAngle,-maxVerticalAngle,maxVerticalAngle);
    }

    void ApplyToCamera()
    {
        horizontalPivot.localEulerAngles = Vector3.up * xAngle;
        verticalPivot.localEulerAngles = Vector3.right * yAngle;
    }




} // SCRIPT END
