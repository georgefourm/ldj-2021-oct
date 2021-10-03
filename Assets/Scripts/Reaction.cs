using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction
{
    public Color[] input { get; private set; }
    public Color output { get; private set; }
    public Rule[] rules { get;  private set; }

    public Reaction(Color[] input, Color output, Rule[] rules)
    {
        this.input = input;
        this.output = output;
        this.rules = rules;
    }
}
