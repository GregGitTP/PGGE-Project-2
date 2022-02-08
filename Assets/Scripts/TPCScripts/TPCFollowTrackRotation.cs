using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE;

public class TPCFollowTrackRotation : TPCFollow
{
    public Vector3 camPos;
    

    public TPCFollowTrackRotation(Transform _camera, Transform _player, float x, float y, float z) : base(_camera, _player, x, y, z){}

    public override void Start(){
        camera.rotation = Quaternion.Euler(GameConstants.cameraAngleOffset, 0f, 0f);
    }

    public override void Update(){
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition(){
        camPos = player.TransformPoint(new Vector3(x, y, z));
        if(!GameConstants.blocked){
            camera.position = Vector3.Lerp(camera.position, camPos, Time.deltaTime * GameConstants.damping);
        }
        else{
            camera.position = Vector3.Lerp(camera.position, GameConstants.blockedCamPos, Time.deltaTime * GameConstants.damping);
        }
    }

    private void UpdateRotation(){
        Vector3 playerAngle = new Vector3(GameConstants.cameraAngleOffset, player.eulerAngles.y, player.eulerAngles.z);

        camera.rotation = Quaternion.Slerp(camera.rotation, Quaternion.Euler(playerAngle), Time.deltaTime * GameConstants.damping);
    }
}
