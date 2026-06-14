using UnityEngine;

/// <summary>
/// entity의 monoBehaviour
/// </summary>
public class EntityMono : MonoBehaviour
{

    public Vector3 GetCurrPos()
    {
        return transform.position;
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
