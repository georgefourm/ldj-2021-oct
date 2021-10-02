using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public abstract class Rule : ScriptableObject
{
    public abstract void apply(Mix mix);
}
