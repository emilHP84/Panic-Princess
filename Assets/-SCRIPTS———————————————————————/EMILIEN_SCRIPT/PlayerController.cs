using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Scripted
{
    public class PlayerController : MonoBehaviour
    {
        private Move move;
        private Jump jump;

        [SerializeField] private bool isJumping = false;
        private float jumpStartTime;
        [SerializeField] private float jumpDuration = 1f;

        [SerializeField] private AudioSource footStepSource;
        [SerializeField] private GameObject runAnim;
        [SerializeField] private GameObject jumpAnim;

        [SerializeField] private List<ParticleSystem> vfxTrails = new List<ParticleSystem>();
        [SerializeField] private GameObject FX_jump;
        [SerializeField] private GameObject FX_Endjump;

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
                jump.StartJumping(footStepSource, jumpAnim, runAnim);
            }
        }

        private void Update()
        {
            if (GAME.MANAGER.CurrentState != State.gameplay)
            {
                footStepSource.volume = 0f;
                return;
            }
            else
            {
                footStepSource.volume = 1f;

            }
            if (move != null)
            {
                move.MovingOnValue(new Vector3(1, 0, 0));
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Instantiate(FX_jump, transform.position, Quaternion.identity, transform);

                isJumping = true;
                jumpStartTime = Time.time;
                jump.StartJumping(footStepSource, jumpAnim, runAnim);
                SetVFX(false);
            }

            Vector3 newPosition = transform.position;

            if (isJumping)
            {
                if (Time.time - jumpStartTime < jumpDuration)
                {
                    jump.DuringJumping(jumpStartTime, jumpDuration);
                }
                else
                {
                    isJumping = false;
                    Instantiate(FX_Endjump, transform.position, Quaternion.identity, transform);
                    jump.EndJumping(footStepSource, jumpAnim, runAnim);
                    SetVFX(true);
                }
            }
        }

        void SetVFX(bool isActive)
        {
            for (int i = 0; i >= vfxTrails.Count; i++)
            {
                if (isActive == true)
                {
                    vfxTrails[i].Play();
                }
                if (isActive == false)
                {
                    vfxTrails[i].Stop();
                }
            }
        }
    }
}
