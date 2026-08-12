using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(
    fileName = "NewModelData",
    menuName = "Potara/Model Data"
)]
public class ModelData : ScriptableObject
{
    [Header("Basic Information")]
    public string id;
    public string modelName;
    public string category;

    [Header("3D Model")]
    public GameObject modelPrefab;

    [Header("Nutrition Information")]
    [TextArea(8, 20)]
    public string nutritionInfo;

    [Header("Fun Fact")]
    [TextArea(3, 10)]
    public string funFact;

    [Header("Recipes")]
    public List<RecipeData> recipes = new List<RecipeData>();
}