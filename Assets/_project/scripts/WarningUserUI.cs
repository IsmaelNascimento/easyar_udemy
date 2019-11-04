using easyar;
using UnityEngine;

public class WarningUserUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] ImageTargetController[] imageTargetControllers;
    
    private void Start()
    {
        foreach (var imageTargetController in imageTargetControllers)
        {
            imageTargetController.OnTargetFound += OnTargetFound;
            imageTargetController.OnTargetLost += OnTargetLost;
        }
    }

    private void OnTargetFound(Target target)
    {
        panel.SetActive(false);
    }

    private void OnTargetLost(Target target)
    {
        panel.SetActive(true);
    }
}