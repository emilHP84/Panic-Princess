using UnityEngine;

namespace Scripted
{
    public class PlayerController : MonoBehaviour
    {
        private Move move;
        private Jump jump;

        [SerializeField]private bool isJumping = false;
        private float jumpStartTime;
        [SerializeField] private float jumpDuration = 1f;
        private Vector3 initialPosition;

        private void OnEnable()
        {
            EVENTS.OnJump += Jumping;
        }

        private void OnDisable()
        {
            EVENTS.OnJump -= Jumping;

        }

        private void Start()
        {
            AssignComponent();
        }

        private void AssignComponent()
        {
            move = GetComponent<Move>();
            jump = GetComponent<Jump>();
        }

        private void Jumping() 
        {
            if (!isJumping)
            {
                isJumping = true;
                jumpStartTime = Time.time;
                initialPosition = transform.position;
                jump.StartJumping();
            }
        }

        private void Update()
        {
            if(GAME.MANAGER.CurrentState != State.gameplay)
            {
                return;
            }
            if (move != null)
            {
                move.MovingOnValue(new Vector3(1,0,0));
            }

            if (Input.GetKeyDown(KeyCode.Space)&& !isJumping)
            {
                isJumping = true;
                jumpStartTime = Time.time;
                initialPosition = transform.position;
                jump.StartJumping();
            }

            Vector3 newPosition = transform.position;

            if (isJumping) 
            {
                if (Time.time - jumpStartTime < jumpDuration)
                {
                    jump.DuringJumping(jumpStartTime,jumpDuration, initialPosition);
                }
                else
                {
                    isJumping = false;
                    jump.EndJumping();
                }
            }
        }
    }
}
