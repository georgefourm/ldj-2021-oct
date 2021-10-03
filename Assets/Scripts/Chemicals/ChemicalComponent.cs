using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chemical", menuName = "Chemicals/Chemical Component", order = 1)]
public class ChemicalComponent : ScriptableObject
{
    [SerializeField]
    public Rule[] rules;

    public Material chemicalColor;
}
