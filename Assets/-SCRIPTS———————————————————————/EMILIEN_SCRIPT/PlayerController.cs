using DG.Tweening;
using NUnit.Framework;
using System.Collections;
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
            EVENTS.OnDeath += DeathAnim;
        }

        private void OnDisable()
        {
            EVENTS.OnJump -= Jumping;
            EVENTS.OnDeath -= DeathAnim;
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

            if (!isJumping && Input.GetKeyDown(KeyCode.Space)  || !isJumping && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    Instantiate(FX_jump, transform.position, Quaternion.identity, transform);

                    isJumping = true;
                    jumpStartTime = Time.time;
                    jump.StartJumping(footStepSource, jumpAnim, runAnim);
                    SetVFX(false);
                }

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

        void DeathAnim()
        {
            StartCoroutine("EnumDeath");
        }

        private IEnumerator EnumDeath()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.transform.DOMoveY(3, 0.6f);
            yield return new WaitForSeconds(0.6f);
            gameObject.transform.DOMoveY(-10, 1f);
        }


        void SetVFX(bool isActive)
        {  
            for (int i = 0; i >= vfxTrails.Count; i++)
            {
                if (isActive == true)
                {
                    //vfxTrails[i].main.simulationSpeed = 0;
                }
                if (isActive == false)
                {
                    //vfxTrails[i].main.simulationSpeed = 1;
                }
            }
        }
    }
}
