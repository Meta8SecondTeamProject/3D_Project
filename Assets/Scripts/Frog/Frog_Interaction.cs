using UnityEngine;
using UnityEngine.InputSystem;

public class Frog_Interaction : MonoBehaviour
{
    private InputAction interaction;

    public bool readyToInteraction;

    private void Awake()
    {
        interaction = GetComponent<PlayerInput>().actions.FindAction("Interaction");
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

    //NPC 대사창, 상호작용 On
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interaction npc) && transform.root.CompareTag("Player"))
        {
            if (npc.isMessage)
            {
                npc.isInteraction = true;
            }
            else
            {
                if (npc.isNonInteractive == false)
                {
                    UIManager.Instance.OnOffInteractionText(true);
                }
                npc.interactionEffectObject.SetActive(true);
                npc.isMessage = true;
            }
        }
    }

    //NPC 대사창, 상호작용 Off
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interaction npc) && transform.root.CompareTag("Player"))
        {
            if (npc.isInteraction)
            {
                npc.isInteraction = false;
            }
            else
            {
                if (npc.isNonInteractive == false)
                {
                    UIManager.Instance.OnOffInteractionText(false);
                }
                npc.interactionEffectObject.SetActive(false);
                npc.isMessage = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (readyToInteraction && other.TryGetComponent(out Interaction npc))
        {
            npc.InteractionEvent();
            readyToInteraction = false;
            return;
        }
    }
}
