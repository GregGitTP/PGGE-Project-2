using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE;

public class TPCTopDown : TPCTrack
{
    protected float distanceFromPlayer;

    public TPCTopDown(Transform _camera, Transform _player, float _distanceFromPlayer) : base(_camera, _player){
        distanceFromPlayer =  _distanceFromPlayer;
    }

    public override void Start(){
        camera.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    public override void Update(){
        Vector3 targetPos = new Vector3(player.position.x, player.position.y + GameConstants.playerHeight + distanceFromPlayer, player.position.z);
        camera.position = Vector3.Lerp(camera.position, targetPos, Time.deltaTime * GameConstants.damping);
    }
}
