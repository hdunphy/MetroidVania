using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private float HealthBarFillSpeed;

    private float currentPercent = 1f;

    public static HUDController Singleton;

    private void Awake()
    {
        //Singleton pattern On Awake set the singleton to this.
        //There should only be one HUDController that can be accessed statically
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        { //if HUDController already exists then destory this. We don't want duplicates
            Destroy(this);
        }
    }

    public void SetCurrentPercent(float percent)
    {
        currentPercent = percent;

        HealthBar.transform.localScale = new Vector2(currentPercent, 1);
    }

    public void SetHealthBarPercent(float percent)
    {
        StartCoroutine(ChangeHealthPercent(percent));
    }

    private IEnumerator ChangeHealthPercent(float percent)
    {
        currentPercent = percent;
        while (Mathf.Abs(HealthBar.transform.localScale.x - currentPercent) > HealthBarFillSpeed)
        {
            HealthBar.transform.localScale = Vector2.MoveTowards(HealthBar.transform.localScale, new Vector2(currentPercent, 1), HealthBarFillSpeed);
            yield return null;
        }
    }
}
