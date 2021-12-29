using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day21;

class Solution : SolutionBase
{
    Dictionary<string, int> Ingredients = new();
    Dictionary<string, HashSet<string>> ImplicatedIngredients = new();

    public Solution() : base(21, 2020, "Allergen Assessment") { }

    protected override string SolvePartOne()
    {
        Ingredients = new Dictionary<string, int>();
        ImplicatedIngredients = new Dictionary<string, HashSet<string>>();

        foreach (var (ingredients, allergens) in GetFoods())
        {
            foreach(var i in ingredients)
            {
                if (Ingredients.ContainsKey(i))
                {
                    Ingredients[i]++;
                }
                else
                {
                    Ingredients.Add(i, 1);
                }
            }

            foreach (var a in allergens)
            {
                if (!ImplicatedIngredients.ContainsKey(a))
                {
                    ImplicatedIngredients.Add(a, new HashSet<string>(ingredients));
                }
                else
                {
                    ImplicatedIngredients[a] = ImplicatedIngredients[a].Intersect(ingredients).ToHashSet();
                }
            }
        }

        var allImplicatedIngredients = ImplicatedIngredients.Values.SelectMany(s => s).ToHashSet();
        return Ingredients.Aggregate(0, (count, i) => allImplicatedIngredients.Contains(i.Key) ? count : count + i.Value).ToString();
    }

    protected override string SolvePartTwo()
    {
        Ingredients = new Dictionary<string, int>();
        ImplicatedIngredients = new Dictionary<string, HashSet<string>>();

        foreach (var (ingredients, allergens) in GetFoods())
        {
            foreach(var i in ingredients)
            {
                if (Ingredients.ContainsKey(i))
                {
                    Ingredients[i]++;
                }
                else
                {
                    Ingredients.Add(i, 1);
                }
            }

            foreach (var a in allergens)
            {
                if (!ImplicatedIngredients.ContainsKey(a))
                {
                    ImplicatedIngredients.Add(a, new HashSet<string>(ingredients));
                }
                else
                {
                    ImplicatedIngredients[a] = ImplicatedIngredients[a].Intersect(ingredients).ToHashSet();
                }
            }
        }

        while (ImplicatedIngredients.Values.Any(v => v.Count > 1))
        {
            foreach (var (a, ingredients) in ImplicatedIngredients)
            {
                if (ingredients.Count == 1)
                {
                    var i = ingredients.First();
                    foreach (var value in ImplicatedIngredients.Values)
                    {
                        if (value.Count == 1) continue;
                        value.Remove(i);
                    }
                }
            }
        }

        return ImplicatedIngredients.OrderBy(kv => kv.Key).Select(kv => kv.Value.First()).JoinAsStrings(",");
    }

    (string[] ingredients, string[] allergens)[] GetFoods()
        => Input.SplitByNewline().Select(l =>
        {
            var (ing, alg, _) = l.Split(" (contains ");
            return (ing.Split(" "), alg.TrimEnd(')').Split(", "));
        }).ToArray();
}
