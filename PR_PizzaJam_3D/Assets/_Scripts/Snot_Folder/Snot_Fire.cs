using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snot_Fire : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    private Transform startingPosition;
    public float timeLerpStart;
    public float lerpTime = 1;
    public bool isReflected;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target").transform;
        startingPosition = this.transform;
        timeLerpStart = Time.time;
        isReflected = false;
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.position = snotFired(startingPosition.position, target.position, timeLerpStart, lerpTime);
    }

    public Vector3 snotFired(Vector3 start, Vector3 end, float timeLerping, float lerpTiming)
    {
        float TimePassed = Time.time - timeLerping;
        float percentageLerped = TimePassed / lerpTiming;
        var result = Vector3.Lerp(start, end, percentageLerped);
        return result;
    }

    public void reflected()
    {
        if (isReflected == false)
        {
            isReflected = true;
            target.position = startingPosition.position;
            startingPosition = this.transform;
        }
    }
}
