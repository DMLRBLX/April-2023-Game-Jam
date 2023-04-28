using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] RandomAudio[] clips;
    [SerializeField] Vector2 maxMinWait;

    bool ready = true;

    private void Update()
    {
        if (ready)
        {
            ready = false;
            StartCoroutine(PlayClip());
        }
    }

    IEnumerator PlayClip()
    {
        yield return new WaitForSeconds(Random.Range(maxMinWait.x, maxMinWait.y));
        int randomClip = Random.Range(0, clips.Length);
        RandomAudio audioData = clips[randomClip];


        int randomEffect = Random.Range(0, audioData.effects.Length);
        audioData.clip.Play();
        audioData.effects[randomEffect].SetActive(true);
        audioData.effects[randomEffect].GetComponent<Animator>().SetTrigger("Effect");

        ready = true;
    }
}

[System.Serializable]
public class RandomAudio
{
    public AudioSource clip;
    public GameObject[] effects;
}