using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Class
// If you want to call this script, directly call 
// SoundEffectController.instance.FUNCTION
// Please do not touch this script without notification

public class SoundEffectController : MonoBehaviour
{

    AudioSource audioSource;

    [SerializeField] AudioClip movingStart;
    [SerializeField] AudioClip moving;
    [SerializeField] AudioClip movingEnd;
    [SerializeField] AudioClip uiPopUp;
    [SerializeField] AudioClip uiClose;
    public static SoundEffectController instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMovingSound()
    {
        audioSource.Stop();
        StartCoroutine("MovingSound");
    }

    public void PlayMovingEndSound()
    {
        StopCoroutine("MovingSound");
        audioSource.Stop();
        audioSource.clip = movingEnd;
        audioSource.loop = false;
        audioSource.Play();
    }
    
    public void PlayUIPopUpSound()
    {
        audioSource.Stop();
        audioSource.clip = uiPopUp;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayUICloseSound()
    {
        audioSource.Stop();
        audioSource.clip = uiClose;
        audioSource.Play();
    }

    IEnumerator MovingSound()
    {
        audioSource.clip = movingStart;
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        audioSource.Stop();
        audioSource.clip = moving;
        audioSource.loop = true;
        audioSource.Play();
    }
}
