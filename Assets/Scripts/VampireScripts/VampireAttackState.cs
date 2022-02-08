using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using PGGE;

public class VampireAttackState : State
{
    Transform player;
    Animator anim;

    float elapTime = 0f;
    float shootRate = .2f;
    float shootAmt = 1;

    public VampireAttackState(FSM _fsm, Transform _player, Animator _anim) : base(_fsm){
        player = _player;
        anim = _anim;
    }

    public override void Enter(){}

    public override void Exit(){
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
    }

    public override void Update(){
        if(GameConstants.currentMagicCount <= 0){
            fsm.SetCurrentState(fsm.GetState(2));
            return;
        }

        if(Input.GetKey(KeyCode.Z)){
            anim.SetBool("Attack1", true);
            shootAmt = 1f;
            shootAmt = 0;
        }
        else if(Input.GetKey(KeyCode.X)){
            anim.SetBool("Attack2", true);
            shootRate = .2f;
            shootAmt = 1;
        }
        else if(Input.GetKey(KeyCode.C) && GameConstants.currentMagicCount >= 20){
            anim.SetBool("Attack3", true);
            shootRate = 1.6f;
            shootAmt = 20;
        }
        else{
            anim.SetFloat("Posx", 0f, 1f, Time.deltaTime * 10f);
            anim.SetFloat("PosY", 0f, 1f, Time.deltaTime * 10f);
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", false);
            anim.SetBool("Attack3", false);
            return;
        }

        if(Input.GetKey(KeyCode.R)){
            fsm.SetCurrentState(fsm.GetState(2));
            return;
        }

        if(elapTime >= shootRate){
            GameConstants.currentMagicCount -= shootAmt;
            elapTime = 0f;
        }
        else elapTime += Time.deltaTime;

        GameConstants.UpdateMagicTxt();
    }

    public override void FixedUpdate(){}

    public override void LateUpdate(){}
}
