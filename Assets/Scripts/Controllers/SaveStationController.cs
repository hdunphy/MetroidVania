using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStationController : MonoBehaviour, IPlayerTrigger
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Sprite OffSprite;
    [SerializeField] private Sprite OnSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController controller))
        {
            controller.SetTriggerObject(this);
            SpriteRenderer.sprite = OnSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController controller))
        {
            controller.SetTriggerObject(null);
            SpriteRenderer.sprite = OffSprite;
        }
    }

    public void Interact(PlayerController controller)
    {
        SaveData.current.PlayerPosition = controller.transform.position;
        SaveData.current.PlayerSceneName = gameObject.scene.name;
        if (SerializationManager.Save(SaveData.current.SaveName, SaveData.current))
        {
            Debug.Log("Game Saved");
            StartCoroutine(SaveFlash());
        }
        else
        {
            Debug.LogError("Could not save Save Data");
        }
    }

    private IEnumerator SaveFlash()
    {
        SpriteRenderer.sprite = OffSprite;
        yield return new WaitForSeconds(.25f);
        SpriteRenderer.sprite = OnSprite;
        yield return new WaitForSeconds(.25f);
        SpriteRenderer.sprite = OffSprite;
        yield return new WaitForSeconds(.5f);
        SpriteRenderer.sprite = OnSprite;
    }
}
