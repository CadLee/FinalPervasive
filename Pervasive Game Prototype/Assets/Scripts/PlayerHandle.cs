using UnityEngine;

public class PlayerHandle : MonoBehaviour
{
    [SerializeField] float MaxHP;
    [SerializeField] float Attack;
    
    private float healthPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthPoints = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
