using UnityEngine;
using System.Collections;

// types of interaction for IInteractiveObject objects
public enum InteractType { None, Lootable, NPC, Metro, Bush, ES };

public interface IInteractiveObject
{
    int Interact(int param);
    InteractType GetInteractType();
}
