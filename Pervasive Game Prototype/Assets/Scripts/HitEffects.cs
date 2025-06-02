using Unity.VisualScripting;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    [SerializeField] public GameObject hitEffect;

    void Start()
    {

    }

    public void PlayHitEffect()
    {
        GameObject effect = Instantiate(hitEffect, this.transform);
        effect.transform.Rotate(effect.transform.forward * Random.Range(0, 360));
    }
}


