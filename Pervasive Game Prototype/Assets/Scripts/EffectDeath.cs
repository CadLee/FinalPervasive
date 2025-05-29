using UnityEngine;

public class EffectDeath : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] public float rotationSpeed;

    void Start()
    {
        Invoke("Death", lifetime);
    }
    
    void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime * 360));
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
