using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE;

public class TPCTrack : TPCBase
{
    public TPCTrack(Transform _camera, Transform _player) : base(_camera, _player){}

    public override void Start(){
        camera.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public override void Update(){
        camera.LookAt(new Vector3(player.position.x, player.position.y + GameConstants.playerHeight, player.position.z));
    }
}
