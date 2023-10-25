using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNavigator : MonoBehaviour
{
    public List<Transform> transforms;

    private int currentIndex = -1;

    public void Init(int index = 0)
    {
        currentIndex = index;
    }

    public Vector2 GetNextLocation()
    {
        if (currentIndex >= transforms.Count)
            return Vector2.zero;

        Vector3 distance = transforms[currentIndex].position - transform.position;
        Vector3 dir = distance.normalized;

        if (Mathf.Abs(distance.x) < 0.5f && Mathf.Abs(distance.z) < 0.5f)
            currentIndex++;

        return new Vector2(dir.x, dir.z);
    }
}