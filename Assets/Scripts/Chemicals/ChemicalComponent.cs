using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chemical", menuName = "Chemicals/Chemical Component", order = 1)]
public class ChemicalComponent : ScriptableObject
{
    public float amount;

    [SerializeField]
    public Rule[] rules;

}
