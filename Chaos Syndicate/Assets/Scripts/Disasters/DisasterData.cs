using UnityEngine;

[CreateAssetMenu(fileName = "DisasterData", menuName = "Scriptable Objects/DisasterData")]
public class DisasterData : ScriptableObject
{
    public bool destroyed;
    public float expRadiusSmall,expRadiusBig,expForce;

    public void initialize(bool destroyed,float expRadiusSmall,float expRadiusBig,float expForce=0)
    {
        this.destroyed=destroyed;
        this.expRadiusSmall=expRadiusSmall;
        this.expRadiusBig=expRadiusBig;
        this.expForce=expForce;
    }
}
