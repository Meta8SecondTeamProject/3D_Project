using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Frog_Interaction : MonoBehaviour/*, IBuiable*/
{
	private InputActionAsset controlDefine;
	private InputAction interaction;

	private bool input;
	public bool readyToInteraction;

	private void Awake()
	{
		controlDefine = GetComponent<PlayerInput>().actions;
		interaction = controlDefine.FindAction("Interaction");
	}

	private void OnEnable()
	{
		interaction.started += OnPressEvent;
	}

	private void OnDisable()
	{
		interaction.started -= OnPressEvent;
	}

	private void OnPressEvent(InputAction.CallbackContext context)
	{
		if (context.ReadValue<float>() > 0)
		{
			readyToInteraction = true;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out NPC npc) && transform.root.CompareTag("Player"))
		{
			if (npc.isMessage)
			{
				npc.isInteraction = true;
				//UIManager.Instance.OnOffInteractionText();
			}
			else
			{
				UIManager.Instance.OnOffInteractionText(true);
				npc.chatWindow.SetActive(true);
				npc.isMessage = true;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out NPC npc) && transform.root.CompareTag("Player"))
		{
			if (npc.isInteraction)
			{
				npc.isInteraction = false;
			}
			else
			{
				UIManager.Instance.OnOffInteractionText(false);
				npc.chatWindow.SetActive(false);
				npc.isMessage = false;
				//UIManager.Instance.OnOffInteractionText();
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (readyToInteraction && other.TryGetComponent(out NPC npc))
		{
			npc.Interaction();
			readyToInteraction = false;
			return;
		}
	}


	//int IBuiable.BuySometing()
	//{
	//	return DataManager.Instance.data.money;
	//}
}
