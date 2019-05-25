using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu, System.Serializable]
public class VectorValue : ScriptableObject
{
    [Header("Runtime value")]
    public Vector2 InitialValue;
    [Header("Default starting value")]
    public Vector2 DefaultValue;


}
