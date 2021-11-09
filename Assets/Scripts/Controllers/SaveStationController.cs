using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveStationController : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Sprite OffSprite;
    [SerializeField] private Sprite OnSprite;
    [SerializeField] private Text SaveStationText;

    private const string OnEnterText = "Press Up to Save";
    private const string OnSaveText = "Saving...";
    private const string OnSavedText = "Saved";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController controller))
        {
            controller.SetTriggerObject(this);
            SpriteRenderer.sprite = OnSprite;
            SaveStationText.enabled = true;
            SaveStationText.text = OnEnterText;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController controller))
        {
            controller.SetTriggerObject(null);
            SpriteRenderer.sprite = OffSprite;
            SaveStationText.enabled = false;
        }
    }

    public void Interact(PlayerController controller)
    {
        SaveStationText.text = OnSaveText;
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
            SaveStationText.text = "Could not save Save Data";
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

        SaveStationText.text = OnSavedText;
    }
}
