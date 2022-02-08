using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Patterns;
using PGGE;
using Photon.Pun;

public class Vampire : MonoBehaviour
{
    [Header("Component references")]
    public Transform camera;
    public Transform player;
    public CharacterController cc;
    public Animator anim;
    public Text magicTxt;
    //public FixedJoystick joystick;

    [Header("Values for character movement")]
    [Space(10)]
    public float movementSpeed = 2f;
    public float rotationSpeed = 300f;
    public float runMultiplier = 2f;
    public float gravity = -20f;
    public float jumpForce = 8f;

    [Header("The X, Z and Z axis offsets for the third person camera")]
    public float xOffset;
    public float yOffset;
    public float zOffset;

    FSM fsm = new FSM();
    State moveState, attackState, reloadState;
    PhotonView photonView;

    private void Start(){
        moveState = new VampireMovementState(fsm, /*joystick,*/ camera, player, this, cc, anim, movementSpeed, rotationSpeed, runMultiplier, gravity, jumpForce, xOffset, yOffset, zOffset);

        attackState = new VampireAttackState(fsm, player, anim);

        reloadState = new VampireRechargeState(fsm, anim);

        fsm.Add(0, moveState);
        fsm.Add(1, attackState);
        fsm.Add(2, reloadState);

        fsm.SetCurrentState(moveState);

        photonView = GetComponent<PhotonView>();
    }

    private void Update(){
        if(photonView == null){
            Update_Personal();
            return;
        }
        else{
            if(photonView.IsMine){
                Update_Personal();
            }
        }
    }

    private void Update_Personal(){
        GameConstants.pForward = player.forward;

        if(fsm.GetCurrentState() != fsm.GetState(2)){
            if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.C)) fsm.SetCurrentState(fsm.GetState(1));
            else if(Input.GetKey(KeyCode.R)) fsm.SetCurrentState(fsm.GetState(2));
            else fsm.SetCurrentState(fsm.GetState(0));
        }

        fsm.Update();
        magicTxt.text = GameConstants.magicTxt;
    }

    private void FixedUpdate(){
        if(photonView == null){
            fsm.FixedUpdate();
            return;
        }
        else{
            if(photonView.IsMine){
                fsm.FixedUpdate();
            }
        }
    }

    private void LateUpdate(){
        if(photonView == null){
            fsm.LateUpdate();
            return;
        }
        else{
            if(photonView.IsMine){
                fsm.LateUpdate();
            }
        }
    }
}
