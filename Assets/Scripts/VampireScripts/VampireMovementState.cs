using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using PGGE;

public class VampireMovementState : State
{
    float movementSpeed, rotationSpeed, runMultiplier, gravity, jumpForce, x, y, z;

    //FixedJoystick joystick;
    Transform camera, player;
    CharacterController cc;
    Animator anim;
    MonoBehaviour mb;

    TPCFollowTrackRotation tpc;
    RepositionCamera rc;

    Vector3 velocity = Vector3.zero;
    bool run = false;
    bool running = false;
    bool jump = false;
    bool crouch = false;

    public VampireMovementState(FSM _fsm, /*FixedJoystick _joystick,*/ Transform _camera, Transform _player, MonoBehaviour _mb, CharacterController _cc, Animator _anim, float _movementSpeed, float _rotationSpeed, float _runMultiplier, float _gravity, float _jumpForce, float _x, float _y, float _z) : base(_fsm){
        //joystick = _joystick;
        camera = _camera;
        player = _player;
        mb = _mb;
        cc = _cc;
        anim = _anim;
        movementSpeed = _movementSpeed;
        rotationSpeed = _rotationSpeed;
        runMultiplier = _runMultiplier;
        gravity = _gravity;
        jumpForce = _jumpForce;
        x = _x;
        y = _y;
        z = _z;
    }

    public override void Enter(){
        tpc = new TPCFollowTrackRotation(camera, player, x, y, z);
        rc = new RepositionCamera(camera, player, tpc);
    }

    public override void Exit(){
        tpc = null;
        rc = null;
    }

    public override void Update(){
        HandleInputs();
        Move();
        GameConstants.UpdateMagicTxt();
    }

    public override void FixedUpdate(){
        ApplyGravity();
    }

    public override void LateUpdate(){
        tpc.Update();
        rc.Update();
    }

    private void HandleInputs(){
        if(Input.GetKeyDown(KeyCode.LeftShift)) run = true;
        
        if(Input.GetKeyUp(KeyCode.LeftShift)) run = false;

        if(Input.GetKeyDown(KeyCode.Space)) jump = true;

        if(Input.GetKeyUp(KeyCode.Space)) jump = false;

        if(Input.GetKeyDown(KeyCode.Tab)){
            crouch = !crouch;
            Crouch();
        }  
    }

    private void Move(){
        // #if UNITY_STANDALONE
        //     float vert = Input.GetAxis("Vertical");
        //     float hori = Input.GetAxis("Horizontal");
        // #endif

        // #if UNITY_ANDROID
        //     float vert = joystick.Vertical;
        //     float hori = joystick.Horizontal;
        // #endif
        
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        if (run && !(vert < 0)) running = true;
        else running = false;

        Vector3 move = player.forward * vert * movementSpeed;
        if(running) move *= runMultiplier;

        cc.Move(move * Time.deltaTime);

        player.Rotate(0f, hori*rotationSpeed*.3f*Time.deltaTime, 0f);

        if(running) vert *= 2;
        anim.SetFloat("PosY", vert, 1f, Time.deltaTime * 10f);
        anim.SetFloat("PosX", hori, 1f, Time.deltaTime * 10f);

        if(jump && anim.GetCurrentAnimatorStateInfo(0).IsName("GroundMovement")) mb.StartCoroutine(Jump()); 
    }

    private void ApplyGravity(){
        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if(cc.isGrounded && velocity.y < 0f) velocity.y = 0f;
    }

    private IEnumerator Jump(){
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(.5f);
        velocity.y += jumpForce;
    }

    private void Crouch(){
        anim.SetBool("Crouch", crouch);
    }
}
