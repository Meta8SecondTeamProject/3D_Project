using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Frog_Interaction : MonoBehaviour
{
    private InputActionAsset controlDefine;
    private InputAction interaction;

    private bool input;
    private bool readyToInteraction;

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

    private void Update()
    {
        Debug.Log(readyToInteraction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NPC npc))
        {
            if (npc.isMessage)
            {
                npc.isInteraction = true;
                //readyToInteraction = true;
                npc.ambassadorWindow.SetActive(true);
                UIManager.Instance.OnOffInteractionText();
            }
            else
            {
                UIManager.Instance.OnOffInteractionText();
                npc.isMessage = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NPC npc))
        {
            if (npc.isInteraction)
            {
                npc.isInteraction = false;
                //readyToInteraction = false;
            }
            else
            {
                UIManager.Instance.OnOffInteractionText();
                npc.isMessage = false;
                npc.ambassadorWindow.SetActive(false);
                UIManager.Instance.OnOffInteractionText();
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
}
