using System;
using Maxime.Script.Inputs;
using UnityEngine;

namespace Maxime.Script.Controllers
{
    [RequireComponent(typeof(MaximePlayerInput))]
    [RequireComponent(typeof(MaximeMovement))]
    [RequireComponent(typeof(CharacterController))]
    public class MaximePlayerController : MonoBehaviour
    {
        private MaximePlayerInput _playerInput;

        private bool _isJumping;
        private float _verticalSpeed;
        private bool _isGrounded;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float gravity = -20f;
        [SerializeField] private LayerMask groundLayer;

        private CharacterController _characterController;

        private void Awake()
        {
            _playerInput = GetComponent<MaximePlayerInput>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();
            ApplyGravity();
            CheckGroundStatus();
        }

        private void OnEnable()
        {
            _playerInput.OnJump += PlayerInput_OnJump;
        }

        private void OnDisable()
        {
            _playerInput.OnJump -= PlayerInput_OnJump;
        }

        /// <summary>
        /// Handles player input for jumping.
        /// </summary>
        private void PlayerInput_OnJump(object sender, System.EventArgs e)
        {
            if (_isGrounded)
            {
                _isJumping = true;
                _verticalSpeed = jumpForce;
                Debug.Log("Jump");
            }
        }

        /// <summary>
        /// Handles movement, including forward and jump physics.
        /// </summary>
        private void HandleMovement()
        {
            Vector3 move = Vector3.right * moveSpeed;
            move.y = _verticalSpeed;
            _characterController.Move(move * Time.deltaTime);
        }

        /// <summary>
        /// Applies gravity to the player.
        /// </summary>
        private void ApplyGravity()
        {
            if (!_isGrounded)
            {
                _verticalSpeed -= gravity * Time.deltaTime;
            }
            else if (_isGrounded)
            {
                _verticalSpeed = 0f;
            }
        }

        /// <summary>
        /// Checks if the player is on the ground using the CharacterController.
        /// </summary>
        private void CheckGroundStatus()
        {
            _isGrounded = _characterController.isGrounded;
            if (_isGrounded)
            {
                _isJumping = false;
            }
        }
    }
}