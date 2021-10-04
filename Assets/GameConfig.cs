using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public float ReactionModifier = 1;
    private Dictionary<Color, Dictionary<Color, Reaction>> mappings { get; set; }

    private Reaction[] reactionPool;

    private void Awake()
    {
        var reactionPool = new List<Reaction>();
        for (float i = .1f; i <= .3f; i += .1f)
        {
            float amount = i * ReactionModifier;
            reactionPool.Add(new Reaction(ChemicalProperty.Smoke, amount, true));
            reactionPool.Add(new Reaction(ChemicalProperty.Smoke, amount, false));
            reactionPool.Add(new Reaction(ChemicalProperty.Wobble, amount, true));
            reactionPool.Add(new Reaction(ChemicalProperty.Wobble, amount, false));
        }
        this.reactionPool = reactionPool.ToArray();
    }

    public void GenerateConfig(Color[] colors)
    {
        mappings = new Dictionary<Color, Dictionary<Color, Reaction>>();
        Color[] resultColorPool = Shuffle(colors);

        int i = 0;
        foreach (var color in colors)
        {
            var innerDict = new Dictionary<Color, Reaction>();
            mappings.Add(color, innerDict);

            int j = 0;
            foreach (var otherColor in colors)
            {
                if (color.Equals(otherColor))
                {
                    // Skip Same color reactions
                    continue;
                }

                var resultColor = resultColorPool[j];
                if (resultColor.Equals(color))
                {
                    // Skip results that are the same as the color of the mix
                    j++;
                    resultColor = resultColorPool[j];
                }
                var resultReaction = reactionPool[i % reactionPool.Length];
                innerDict.Add(otherColor, new Reaction(resultReaction, resultColor));

                i++;
                j++;
            }
        }
    }

    private Color[] Shuffle(Color[] colors)
    {
        Color[] result = new Color[colors.Length];
        colors.CopyTo(result,0);
        for (int i = colors.Length - 1; i > 0; i--)
        {
            int j = Random.Range(i,colors.Length);
            var temp = result[i];
            result[i] = result[j];
            result[j] = temp;
        }
        return result;
    }

    public Reaction GetReaction(Color a, Color b)
    {
        return mappings[a][b];
    }
}

