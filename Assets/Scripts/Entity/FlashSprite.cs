using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer; //Render to change the material
    [SerializeField] private Material FlashMaterial; //Material to momentarily switch to
    [SerializeField] private float Duration; //How long to switch material

    private Material currentMaterial; //Variable to save original material
    private Coroutine flashCoroutine; //Store coroutine so don't run it twice

    private void Start()
    {
        currentMaterial = SpriteRenderer.material;
    }

    /// <summary>
    /// Set Flash duration and then call flash
    /// </summary>
    /// <param name="seconds">How long to flash for</param>
    public void FlashForDuration(float seconds)
    {
        Duration = seconds;
        Flash();
    }

    /// <summary>
    /// Stop flash coroutine if currently running.
    ///     Run FlashCoroutine
    /// </summary>
    public void Flash()
    {
        if(flashCoroutine != null)
            StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// Change Sprite Renderer's material for {Duration} number of seconds and then revert back
    /// </summary>
    private IEnumerator FlashCoroutine()
    {
        SpriteRenderer.material = FlashMaterial;

        yield return new WaitForSeconds(Duration);

        SpriteRenderer.material = currentMaterial;
    }
}
