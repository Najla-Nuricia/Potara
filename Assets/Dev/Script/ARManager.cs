using UnityEngine;
using Vuforia;

public class ARManager : MonoBehaviour
{
    [Header("Vuforia Target")]
    [SerializeField] private ObserverBehaviour imageTarget;

    [Header("AR Content")]
    [SerializeField] private GameObject arContent;

    [Header("UI")]
    [SerializeField] private GameObject scanPanel;
    [SerializeField] private GameObject informationPanel;

    [Header("Model")]
    [SerializeField] private ModelSwitcher modelSwitcher;

    private bool isTargetFound;

    private void Awake()
    {
        if (imageTarget != null)
        {
            imageTarget.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        SetTargetLostState();
    }

    private void OnDestroy()
    {
        if (imageTarget != null)
        {
            imageTarget.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(
        ObserverBehaviour behaviour,
        TargetStatus status)
    {
        bool targetFound = IsTargetFound(status);

        if (targetFound && !isTargetFound)
        {
            OnTargetFound();
        }
        else if (!targetFound && isTargetFound)
        {
            OnTargetLost();
        }
    }

    private bool IsTargetFound(TargetStatus status)
    {
        return status.Status == Status.TRACKED ||
               status.Status == Status.EXTENDED_TRACKED ||
               status.Status == Status.LIMITED;
    }

    private void OnTargetFound()
    {
        isTargetFound = true;

        // Tampilkan konten AR
        if (arContent != null)
            arContent.SetActive(true);

        // Sembunyikan "Silahkan Scan"
        if (scanPanel != null)
            scanPanel.SetActive(false);

        // Tampilkan informasi
        if (informationPanel != null)
            informationPanel.SetActive(true);

        // Mulai dari model pertama
        if (modelSwitcher != null)
            modelSwitcher.Initialize();
    }

    private void OnTargetLost()
    {
        isTargetFound = false;

        SetTargetLostState();
    }

    private void SetTargetLostState()
    {
        // Sembunyikan konten AR
        if (arContent != null)
            arContent.SetActive(false);

        // Tampilkan "Silahkan Scan"
        if (scanPanel != null)
            scanPanel.SetActive(true);

        // Sembunyikan informasi
        if (informationPanel != null)
            informationPanel.SetActive(false);
    }
}