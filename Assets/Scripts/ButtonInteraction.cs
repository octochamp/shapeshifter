using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject targetScript;
    public string methodName;

    private XRBaseInteractable interactable;
    private Renderer buttonRenderer;
    private Color originalColor;

    private void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        buttonRenderer = GetComponent<Renderer>();
        originalColor = buttonRenderer.material.color;

        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("OnSelect Entered");
        buttonRenderer.material.color = Color.green;
        if (targetScript != null && !string.IsNullOrEmpty(methodName))
        {
            targetScript.SendMessage(methodName);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("OnSelect Exited");
        buttonRenderer.material.color = originalColor;
    }
}