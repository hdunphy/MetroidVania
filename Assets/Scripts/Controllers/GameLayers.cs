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

    /// <summary>
    /// Determins if layer is in the mask
    /// </summary>
    /// <param name="layer">layer that is being tested</param>
    /// <param name="mask">mask the layer is being tested against</param>
    /// <returns>true if layer is in the layerMask else returns false</returns>
    public bool IsLayerInLayerMask(int layer, LayerMask mask)
    {
        //Layers are stored as bits so found this answer on the internet
        bool isInDamageableLayer = ((1 << layer) & mask) != 0;

        return isInDamageableLayer;
    }

    public LayerMask GroundLayer { get => ground; }

    public LayerMask PortalLayer { get => portal; }
}
