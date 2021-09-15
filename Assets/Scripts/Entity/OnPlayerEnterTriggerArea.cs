using UnityEngine;
using UnityEngine.Events;

public class OnPlayerEnterTriggerArea : MonoBehaviour
{
    public UnityEvent<PlayerController> SetPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            SetPlayer.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            SetPlayer.Invoke(null);
        }
    }
}
