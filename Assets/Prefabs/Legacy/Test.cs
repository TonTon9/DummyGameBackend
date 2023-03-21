using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _gameObjects;

    [ContextMenu("222")]
    private void Test2()
    {
        foreach (var gameObject1 in _gameObjects)
        {
            gameObject1.name = $"mixamorig:{gameObject1.name}";
        }
    }
}
