using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<summary>
 * SpecialFood is showing in random time stamps for given peroid.
 * When its lifetime is coming to over it starts blinking, then disappears.
 * </summary>
 */
public class SpecialFood : Food
{
    public float timeToDestroy = .5f;
    public float blinkingInterval = .05f;

    private float timeToBlink;
    private bool isPlaying;

    // Update is called once per frame

    void Start()
    {
        timeToBlink = blinkingInterval;
        isPlaying = false;
    }

    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if(timeToDestroy <= 0)
        {
            AudioManager.instance.StopPlayingSound("TimeOut");
            Destroy(gameObject);
        }

        if (timeToDestroy <= 1f) // if one last secont left, it starts blinking and play appropriate sound
        {
            if (!isPlaying)
            {
                AudioManager.instance.PlaySound("TimeOut");
                isPlaying = true;
            }


            timeToBlink -= Time.deltaTime;
            if (timeToBlink < 0) // Blinks when it's time to blink ;)
            { 
                Blink();
                timeToBlink = blinkingInterval;
            }
        }

        //adding some rotation for a nice effect
        gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed), Space.Self);

    }

    void Blink()
    {
        //blinking turn on and off MeshRenderer for a nice effect
        gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent<MeshRenderer>().enabled;
    }

    private void OnDestroy()
    {
            AudioManager.instance.StopPlayingSound("TimeOut");
    }
}
