using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Planet", menuName = "Planet")]
public class Planet : ScriptableObject
{
    public Color skyColor;
    public float gravity;
}
