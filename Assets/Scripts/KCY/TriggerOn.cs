using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOn : MonoBehaviour
{
	private void OnEnable()
	{
		DataManager.Instance.triggerOn += 1;
	}
}
