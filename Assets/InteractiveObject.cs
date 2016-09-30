using UnityEngine;
using System.Collections;

public enum InteractType { None, Lootable, NPC, Metro, Bush, ES };

public interface IInteractiveObject
{
    int Interact(int param);
    InteractType GetInteractType();
}
