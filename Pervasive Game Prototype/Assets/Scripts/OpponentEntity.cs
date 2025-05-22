using UnityEngine;

public class OpponentEntity : GameEntity
{
    public PlayerEntity player;
    public PlayerControl playerControl;

    private enum OpponentType { Still }
    [SerializeField] OpponentType opponentType;
    
    private ArmState leftArmState = ArmState.Neutral;
    private ArmState rightArmState = ArmState.Neutral;

    private BodyState bodyState = BodyState.Neutral;
    private AimState aimState = AimState.Neutral;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerEntity>();
        
    }

    void Update()
    {
        
    }

    public void ProcessHit(string hit)
    {
        if hit
    }

    public override void death()
    {
        throw new System.NotImplementedException();
    }
}