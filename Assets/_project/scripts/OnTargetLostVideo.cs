using easyar;
using UnityEngine;

public class OnTargetLostVideo : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private int countMinLost;
    [SerializeField] private GameObject[] gameObjectsForDisable;
    [SerializeField] private GameObject[] gameObjectsForEnable;

    private int controllerCountLost;
    public int ControllerCountLost
    {
        get
        {
            return controllerCountLost;
        }
        set
        {
            controllerCountLost = value;

            if (controllerCountLost == countMinLost)
            {
                OnCountMinLost();
            }
        }
    }

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Start()
    {
        ImageTargetController[] imageTargetControllers = FindObjectsOfType<ImageTargetController>();

        foreach (var imageTargetController in imageTargetControllers)
        {
            imageTargetController.OnTargetFound += OnTargetFound;
            imageTargetController.OnTargetLost += OnTargetLost;
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void OnTargetFound(Target target)
    {
        controllerCountLost++;
    }

    private void OnTargetLost(Target target)
    {
        controllerCountLost--;
    }

    private void OnCountMinLost()
    {
        foreach (var go in gameObjectsForDisable)
        {
            go.SetActive(false);
        }

        //foreach (var go in gameObjectsForEnable)
        //{
        //    go.SetActive(true);
        //}
    }

    #endregion
}