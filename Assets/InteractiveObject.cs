using UnityEngine;
using System.Collections;

public interface IInteractiveObject
{
    int Interact(int param);
    bool CanTalk();
    bool CanLoot();
}
