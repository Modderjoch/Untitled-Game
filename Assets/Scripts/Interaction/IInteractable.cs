using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public string InteractionPrompt2 { get; }
    public Sprite PromptImage { get; }
    public Sprite PromptImage2 { get; }
    public string InteractionName { get; }


    public bool Interact(PlayerInteraction playerInteraction);
    public bool ExtraInteract(PlayerInteraction playerInteraction);
}
