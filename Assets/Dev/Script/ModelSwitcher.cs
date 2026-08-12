using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ModelSwitcher : MonoBehaviour
{
    [Header("Model Container")]
    [SerializeField] private Transform modelContainer;

    [Header("UI")]
    [SerializeField] private TMP_Text modelNameText;
    [SerializeField] private TMP_Text nutritionText;
    [SerializeField] private TMP_Text funFactText;

    [Header("Navigation")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Models")]
    [SerializeField] private List<ModelData> models;

    private int currentIndex;
    private GameObject currentModel;

    private void Awake()
    {
        previousButton.onClick.AddListener(PreviousModel);
        nextButton.onClick.AddListener(NextModel);
    }

    public void Initialize()
    {
        if (models == null || models.Count == 0)
        {
            Debug.LogWarning("ModelSwitcher: ModelData kosong.");
            return;
        }

        currentIndex = 0;
        ShowCurrentModel();
    }

    public void NextModel()
    {
        if (models == null || models.Count == 0)
            return;

        currentIndex++;

        if (currentIndex >= models.Count)
            currentIndex = 0;

        ShowCurrentModel();
    }

    public void PreviousModel()
    {
        if (models == null || models.Count == 0)
            return;

        currentIndex--;

        if (currentIndex < 0)
            currentIndex = models.Count - 1;

        ShowCurrentModel();
    }

    private void ShowCurrentModel()
    {
        ModelData data = models[currentIndex];

        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        if (data.modelPrefab != null)
        {
            currentModel = Instantiate(
                data.modelPrefab,
                modelContainer
            );

            currentModel.transform.localPosition = Vector3.zero;
            currentModel.transform.localRotation = Quaternion.identity;
            currentModel.transform.localScale = Vector3.one;
        }

        if (modelNameText != null)
            modelNameText.text = data.modelName;

        if (nutritionText != null)
            nutritionText.text = data.nutritionInfo;

        if (funFactText != null)
            funFactText.text = data.funFact;

        UpdateNavigationButton();
    }

    private void UpdateNavigationButton()
    {
        bool hasMultipleModels = models.Count > 1;

        previousButton.gameObject.SetActive(hasMultipleModels);
        nextButton.gameObject.SetActive(hasMultipleModels);
    }
}