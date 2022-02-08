using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] concreteSteps, metalSteps, woodSteps, dirtSteps, sandSteps;

    AudioSource audioSource;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void FootStep(AnimationEvent evt){
        AudioClip[] clips = GetCorrespondingAudioClips();
        int num = Random.Range(0, clips.Length);
        if(evt.animatorClipInfo.weight > .5f) audioSource.PlayOneShot(clips[num]);
    }

    private AudioClip[] GetCorrespondingAudioClips(){
        LayerMask mask = LayerMask.GetMask("Floor");
        RaycastHit hit;
        if(Physics.Raycast(transform.position + (transform.up * .2f), -transform.up, out hit, .5f, mask)){
            string matName = hit.transform.gameObject.GetComponent<Renderer>().sharedMaterial.name;
            if(matName == "Metal") return metalSteps;
            else if(matName == "Wood") return woodSteps;
            else if(matName == "Dirt") return dirtSteps;
            else if(matName == "Sand") return sandSteps;
            else return concreteSteps;
        }
        return null;
    }
}
