using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCFollowTrackIndependantRotation
{
    // NOT WORKING
    // public float sensitivity;

    // Vector3 camDir;

    // protected override void OnEnable(){
    //     camera.LookAt(new Vector3(player.position.x, player.position.y + playerHeight, player.position.z));
    // }
    
    // protected override void LateUpdate(){
    //     Vector3 cameraForward = player.InverseTransformDirection(camera.forward);

    //     camera.position = Vector3.Lerp(camera.position, player.TransformPoint(new Vector3(x, y, z)), Time.deltaTime * damping);

    //     float vert = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    //     float hori = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

    //     camDir = cameraForward + new Vector3(hori, vert, 0f);
    
    //     camera.rotation = Quaternion.LookRotation(camDir);
    // }
}
