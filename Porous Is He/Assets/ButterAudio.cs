using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource slideAudio;
    [SerializeField] private AudioSource plopAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlop()
    {
        plopAudio.Play();
    }
    public void PlaySlide()
    {
        slideAudio.Play();
    }
}
