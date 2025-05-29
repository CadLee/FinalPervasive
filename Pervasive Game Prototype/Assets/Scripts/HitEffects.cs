using Unity.VisualScripting;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    [SerializeField] public GameObject hitEffect;

    void Start()
    {
        Instantiate(hitEffect, this.transform);
    }
}


