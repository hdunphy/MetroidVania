using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField, Tooltip("which object layers should be checked for a 'Ground' object")] 
    private LayerMask ground;

    [SerializeField, Tooltip("which object layers should be checked for a 'Portal' object")]
    private LayerMask portal;

    public static GameLayers Singleton;

    private void Awake()
    {
        //Singleton pattern On Awake set the singleton to this.
        //There should only be one GameLayer that can be accessed statically
        if(Singleton == null)
        {
            Singleton = this;
        }
        else
        { //if Gamelayer already exists then destory this. We don't want duplicates
            Destroy(this);
        }
    }

    public LayerMask GroundLayer { get => ground; }

    public LayerMask PortalLayer { get => portal; }
}
