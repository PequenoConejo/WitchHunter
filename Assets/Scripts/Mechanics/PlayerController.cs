using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Photon.Pun;
using Platformer.Mechanics;
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        public PhotonView view;
        public Joystick joystick;
        public GameController gameController;
        public ButtonScript ButtonJump;
        bool isButtonJumpPressed;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            ButtonJump = GameObject.FindGameObjectWithTag("ButtonJump").GetComponent<ButtonScript>();
            // gameController.SetParameters(this);
        }

        void Start()
        {
            view = GetComponent<PhotonView>();
            joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
            // gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            if (view.Owner.IsLocal)
            {
                gameController.SetParameters(this);
            }
        }


        protected override void Update()
        {
            move.x = joystick.Horizontal;
            isButtonJumpPressed = ButtonJump.buttonPressed;
            if (view.IsMine) { 
            if (controlEnabled)
            {
                    if (joystick.Horizontal == 0)
                        move.x = Input.GetAxis("Horizontal");
                    // if (joystick.Horizontal != 0)
                    // move.x = joystick.Horizontal;
                    // if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    if (jumpState == JumpState.Grounded && (isButtonJumpPressed || Input.GetButtonDown("Jump")))
                        jumpState = JumpState.PrepareToJump;
                    // else if (Input.GetButtonUp("Jump"))
                    else if (!isButtonJumpPressed || Input.GetButtonUp("Jump"))
                    {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
            }
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}