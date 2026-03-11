using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public class KeyboardBinding : IInputBinding
{
    [SerializeField] KeyCode _key;

    public KeyboardBinding(KeyCode key)
    {
        this._key = key;
    }

    public bool GetKeyDown() => Input.GetKeyDown(_key);
    public bool GetKey() => Input.GetKey(_key);
    public bool GetKeyUp() => Input.GetKeyUp(_key);
}
