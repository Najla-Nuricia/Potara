using UnityEngine;

[System.Serializable]
public class RecipeData
{
    [Header("Recipe Information")]
    public string recipeName;

    [Header("Recipe Image")]
    public Sprite recipeImage;

    [TextArea(5, 15)]
    public string ingredients;

    [TextArea(5, 15)]
    public string cookingMethod;
}