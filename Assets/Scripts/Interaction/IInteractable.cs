using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public string InteractionPrompt2 { get; }
    public string InteractionName { get; }

    public bool Interact(PlayerInteraction playerInteraction);
}
