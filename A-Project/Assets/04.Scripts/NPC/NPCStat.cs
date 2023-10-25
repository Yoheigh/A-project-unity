using UnityEngine;
using static Define;

public class NPCStat : MonoBehaviour
{
    public CharacterGlobalStatus GlobalStatus { get; private set; }

    public void Init()
    {
        ChangeGlobalStatus(CharacterGlobalStatus.Normal);
    }

    public void ChangeGlobalStatus(CharacterGlobalStatus status)
    {
        GlobalStatus = status;
    }
}