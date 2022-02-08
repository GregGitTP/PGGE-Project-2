using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE;

public class RepositionCamera
{
    Transform camera, player;
    TPCFollowTrackRotation tpc;

    public RepositionCamera(Transform _camera, Transform _player, TPCFollowTrackRotation _tpc){
        camera = _camera;
        player = _player;
        tpc = _tpc;
    }

    public void Update(){
        Vector3 offsetWall = player.forward;
        Vector3 offsetRoof = -player.up;
        
        Vector3 cameraPosition = tpc.camPos - offsetWall;

        Vector3 playerPosition = new Vector3(player.position.x, player.position.y + GameConstants.playerHeight, player.position.z);

        LayerMask mask = LayerMask.GetMask("Obstacle");
        RaycastHit hit;
        if(Physics.Raycast(playerPosition, (cameraPosition - playerPosition), out hit, Vector3.Distance(cameraPosition, playerPosition), mask)){
            GameConstants.blocked = true;
            if(hit.transform.gameObject.tag == "Wall") GameConstants.blockedCamPos = hit.point + (offsetWall * 1f);
            else if(hit.transform.gameObject.tag == "Roof") GameConstants.blockedCamPos = hit.point + (offsetRoof * .2f);
        }
        else{
            GameConstants.blocked = false;
        }
    }
}
