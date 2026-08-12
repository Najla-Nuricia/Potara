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
    [SerializeField] private List<ModelData> models = new List<ModelData>();

    private int currentIndex = 0;
    private GameObject currentModel;

    private void Awake()
    {
        if (previousButton != null)
            previousButton.onClick.AddListener(PreviousModel);

        if (nextButton != null)
            nextButton.onClick.AddListener(NextModel);
    }

    private void OnDestroy()
    {
        if (previousButton != null)
            previousButton.onClick.RemoveListener(PreviousModel);

        if (nextButton != null)
            nextButton.onClick.RemoveListener(NextModel);
    }

    public void Initialize()
    {
        if (models == null || models.Count == 0)
        {
            Debug.LogWarning("ModelSwitcher: Tidak ada ModelData.");
            return;
        }

        // Selalu mulai dari model pertama
        currentIndex = 0;

        Debug.Log(
            $"ModelSwitcher Initialize: {models[0].modelName}"
        );

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
        if (models == null || models.Count == 0)
            return;

        ModelData data = models[currentIndex];

        if (data == null)
        {
            Debug.LogWarning(
                $"ModelSwitcher: ModelData pada index {currentIndex} kosong."
            );
            return;
        }

        // Hapus model sebelumnya
        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

        // Spawn model baru
        if (data.modelPrefab != null && modelContainer != null)
        {
            currentModel = Instantiate(
                data.modelPrefab,
                modelContainer
            );

            currentModel.transform.localPosition = Vector3.zero;
            currentModel.transform.localRotation = Quaternion.identity;
            currentModel.transform.localScale = Vector3.one;

            Debug.Log(
                $"Model spawned: {data.modelName}"
            );
        }
        else
        {
            Debug.LogWarning(
                $"ModelSwitcher: Prefab atau ModelContainer kosong untuk {data.modelName}"
            );
        }

        // Update informasi
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
        bool hasMultipleModels = models != null && models.Count > 1;

        if (previousButton != null)
            previousButton.gameObject.SetActive(hasMultipleModels);

        if (nextButton != null)
            nextButton.gameObject.SetActive(hasMultipleModels);
    }
}