using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Rigidbody myRB;
    public float forceMin;
    public float forceMax;

    float lifetime = 4;
    float fadeTime = 2;

    void Start()
    {
        float force = Random.Range(forceMin, forceMax);
        myRB.AddForce(transform.right * force);
        myRB.AddTorque(Random.insideUnitSphere * force);

        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifetime);

        float percent = 0;
        float fadeSpeed = 1 / fadeTime;

        Material mat = GetComponent<Renderer>().material;

        Color originalColor = mat.color;

        while(percent < 1)
        {
            percent += Time.deltaTime * fadeSpeed;
            mat.color = Color.Lerp(originalColor, Color.clear, percent);

            yield return null;
        }

        Destroy(gameObject);
    }
}
