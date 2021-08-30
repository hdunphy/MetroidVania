using UnityEngine;

public class AbilityPickup : CollectablePickup
{
    /*Component attached to an ingame ability pickup*/

    [SerializeField] private Ability Ability; //Ability given to player upon pickup

    public override void OnPickup(PlayerController controller)
    {
        controller.AddAbility(Ability);
    }
}
