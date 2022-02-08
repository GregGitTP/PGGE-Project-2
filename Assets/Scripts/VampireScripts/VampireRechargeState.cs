using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using PGGE;

public class VampireRechargeState : State
{
    Animator anim;

    float elapTime = 0f;

    public VampireRechargeState(FSM _fsm, Animator _anim) : base(_fsm){
        anim = _anim;
    }

    public override void Enter(){
        GameConstants.ReloadMagicTxt();

        elapTime = 0f;

        anim.SetTrigger("Reload");
    }

    public override void Exit(){
        GameConstants.currentMagicCount = GameConstants.maxMagicCount;
    }

    public override void Update(){}

    public override void FixedUpdate(){}

    public override void LateUpdate(){
        if(elapTime >= anim.GetCurrentAnimatorClipInfo(0)[0].clip.length) fsm.SetCurrentState(fsm.GetState(0));
        else elapTime += Time.deltaTime;
    }
}
