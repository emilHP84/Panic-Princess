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
        ParticleSystem.EmissionModule[] emissions;
        [SerializeField] private GameObject FX_jump;
        [SerializeField] private GameObject FX_Endjump;

        private void OnEnable()
        {
            // EVENTS.OnJump += Jumping;
            EVENTS.OnDeath += DeathAnim;
        }

        private void OnDisable()
        {
            // EVENTS.OnJump -= Jumping;
            EVENTS.OnDeath -= DeathAnim;
        }

        private void Start()
        {
            AssignComponent();
            emissions = new ParticleSystem.EmissionModule[vfxTrails.Count];
            for (int i = 0; i < emissions.Length; i++)
            {
                emissions[i] = vfxTrails[i].emission;
            }
            Debug.Log("emission " + emissions[0].rateOverTime.constant);
        }

        private void AssignComponent()
        {
            move = GetComponent<Move>();
            jump = GetComponent<Jump>();
        }

        private void StartJump()
        {
            if (FX_jump) Instantiate(FX_jump, transform.position, Quaternion.identity, transform);
            isJumping = true;
            jumpStartTime = Time.time;
            jump.StartJumping(footStepSource, jumpAnim, runAnim);
            SetVFX(false); 
        }

        private void EndJump()
        {
            isJumping = false;
            if (FX_Endjump) Instantiate(FX_Endjump, transform.position, Quaternion.identity, transform);
            jump.EndJumping(footStepSource, jumpAnim, runAnim);
            SetVFX(true);
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

            if (!isJumping && Input.GetKeyDown(KeyCode.Space))
            {
                StartJump();
            }

            if (!isJumping && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    StartJump();
                }
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
                    EndJump();
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
            for (int i = 0; i >= emissions.Length; i++)
            {
                emissions[i].rateOverTime = isActive ? 5f : 0f;
            }
        }
    }
}
