using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource MyFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public void HoverSound()
    {
        MyFX.PlayOneShot(hoverFX);
    }
    public void ClickSound()
    {
        MyFX.PlayOneShot(clickFX);
    }
}
