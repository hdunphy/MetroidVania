using UnityEngine;

public class OtherSceneObjectStateController : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private SceneObjectData SceneObjectData;

    public void SetSceneObjectFromOtherScene(bool isOn)
    {
        SceneObjectData.isOn = isOn;
        SaveData.current.SaveSceneObject(SceneName, SceneObjectData);
    }
}

