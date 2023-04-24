using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snot_Fire : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 target;
    private Vector3 startingPosition;
    public float timeLerpStart;
    public float lerpTime = 10f;
    public bool isReflected;
    public ParticleSystem explosion;
    public GameObject snot;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target").transform.position;
        startingPosition = this.transform.position;
        timeLerpStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.position = snotFired(startingPosition, target, timeLerpStart, lerpTime);
    }

    public Vector3 snotFired(Vector3 start, Vector3 end, float timeLerping, float lerpTiming)
    {
        float TimePassed = Time.time - timeLerping;
        float percentageLerped = TimePassed / lerpTiming;
        var result = Vector3.Lerp(start, end, percentageLerped);
        return result;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "target")
        {
            explosion.Play();
            Destroy(snot);
            StartCoroutine(destroyObject());
        }
    }
    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    public void reflected()
    {
        if (isReflected == false)
        {
            isReflected = true;
            timeLerpStart = Time.time;
            Vector3 relativePos = startingPosition - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            this.transform.rotation = rotation;
            target = startingPosition;
            startingPosition = this.transform.position;
        }
    }
}
