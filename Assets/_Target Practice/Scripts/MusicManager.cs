using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicTracks;
    [SerializeField] AudioSource audioSource;
   

   private void Update() {
         if(!audioSource.isPlaying) {
              audioSource.clip = musicTracks[Random.Range(0, musicTracks.Count)];
              audioSource.Play();
         }
   }
}
