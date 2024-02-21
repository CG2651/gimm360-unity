using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    public AudioSource source;
    public BoxCollider soundTrigger;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
       source.Play();
    }
}
