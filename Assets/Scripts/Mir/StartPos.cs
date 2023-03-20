using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _pos;

    public static StartPos GetInstance;

    private void Awake()
    {
        GetInstance = this;
    }

    public Vector3 GetRandomPos()
    {
        return _pos[Random.Range(0, _pos.Count)].position;
    }
}