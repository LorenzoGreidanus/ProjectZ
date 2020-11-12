using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffects : MonoBehaviour
{
    public GameObject muzzleEffect;

    public Sprite[] muzzleEffects;
    public SpriteRenderer[] spriteRenderers;

    public float flashTime;

    public void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        muzzleEffect.SetActive(true);

        int flashSpriteIndex = Random.Range(0, muzzleEffects.Length);
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = muzzleEffects[flashSpriteIndex];
        }

        Invoke("Deactivate", flashTime);
    }

    public void Deactivate()
    {
        muzzleEffect.SetActive(false);
    }
}
