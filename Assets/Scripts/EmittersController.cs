using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmittersController : MonoBehaviour
{

    public GameObject[] emitterObjects;

    private List <Emitter> emitters;

    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

        emitters = new List <Emitter>();

        foreach (var emitter in emitterObjects) 
        { 
            emitters.Add(emitter.GetComponent<Emitter>());
        }
    }

    public void StartEmitter()
    {
        if (emitters.Count == 0)
            return;

        audioPlayer.Play();

        foreach (var emitter in emitters) 
        {
            emitter.Emit();
        }
    }
}
