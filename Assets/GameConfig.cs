using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public Material[] materials;

    public Rule[] rules;

    public Dictionary<Color, Dictionary<Color, Reaction>> mapping { get; private set; }
    
    public void GenerateConfig()
    {
        GenerateRules();
        mapping = new Dictionary<Color, Dictionary<Color, Reaction>>();

        var i = 0;
        Shuffle(rules);

        foreach (var material in materials)
        {
            var color = material.color;
            var colorDict = new Dictionary<Color, Reaction>();
            mapping.Add(color, colorDict);
            foreach (var otherMaterial in materials)
            {
                i++;
                colorDict.Add(otherMaterial.color, new Reaction(
                    new Color[] { color, otherMaterial.color },
                    GetRandomColor(),
                    new Rule[] { rules[i%rules.Length] }    
                ));
            }
        }
    }

    void Shuffle<T>(T[] a)
    {
        // Loops through array
        for (var i = a.Length - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            var temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }

        // Print
        for (int i = 0; i < a.Length; i++)
        {
            Debug.Log(a[i]);
        }
    }

    ModifyRule GenerateRandomRule()
    {
        var amounts = new float[] { 0, 0.2f, 0.3f, 0.4f };
        

        var rule = new ModifyRule();
        rule.Amount = amounts[Random.Range(0, 4)];
        rule.AffectedProperty = (ChemicalProperty) Random.Range(1, 3);
        rule.Remove = Random.Range(0, 1) > 0.5;
        return rule;
    }

    void GenerateRules()
    {
        var ruleList = new List<Rule>();
        var amounts = new float[] { 0.2f, 0.3f, 0.4f };
        foreach (var amount in amounts)
        {
            
            for (var j = 1; j <= 2; j++)
            {
                ruleList.Add(GenerateRule(amount, j, true));
                ruleList.Add(GenerateRule(amount, j, false));
            }
        }

        rules = ruleList.ToArray();
    }

    Rule GenerateRule(float amount, int prop, bool remove)
    {
        var rule = new ModifyRule();
        rule.Amount = amount;
        rule.AffectedProperty = (ChemicalProperty) prop;
        rule.Remove = remove;
        return rule;
    }

    Color GetRandomColor()
    {
        return materials[Random.Range(0, materials.Length)].color;
    }

    public Reaction getReaction(Color a, Color b)
    {
        return mapping[a][b];
    }
}

