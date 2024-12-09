using UnityEngine;

public class TriggerOn : MonoBehaviour
{
    [SerializeField]
    private Prop_On prop;
    private void OnEnable()
    {
        DataManager.Instance.triggerOn += 1;
        if (DataManager.Instance.triggerOn >= 3 && DataManager.Instance.triggerOn % 3 == 0)
        {
            prop.StartCoroutine(prop.PropOn());
        }
    }
}
