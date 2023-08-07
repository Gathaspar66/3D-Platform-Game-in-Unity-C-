using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public GameObject exitObject;
    public GameObject verticalLayout;

    void Start()
    {
        GameObject exitObject = GameObject.Find("EXIT");
        GameObject verticalLayout = GameObject.FindWithTag("VerticalLayout");
    }

    public void ExitEnabled(bool ifExitEnabled)
    {
        if (exitObject != null)
        {
            exitObject.SetActive(ifExitEnabled);
        }
    }

    public void ExitFromGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // wy³¹cz tryb gry
#endif
    }

    public void DisableLayoutInteractions(bool ifEnabled)
    {
        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("VerticalMenu");
        foreach (GameObject buttonObject in buttonObjects)
        {
            Button button = buttonObject.GetComponent<Button>();
            if (button != null)
            {
                button.enabled = ifEnabled;
            }
            else
            {
                Debug.LogWarning("Not found component with tag" + "VerticalMenu");
            }
        }
    }
}