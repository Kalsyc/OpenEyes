using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles the environment screen in meditation menu
/// </summary>
public class EnvironmentLoader : MonoBehaviour
{
    private Color unselectColorFlower;
    private Color unselectColorGrass;
    private int currentLoadedEnvIndex = -1;
    private int flowerEnvIndex = (int)SceneIndexes.MEDITATION_FLOWERS;
    private int grassEnvIndex = (int)SceneIndexes.MEDITATION_GRASS;
    private bool isSelected = false;

    public Color selectColor;
    public GameObject unselectedWarning;
    public GameObject envPanel;
    public GameObject nextPanel;
    public Graphic flowerPanel;
    public Graphic grassPanel;

    private void Start()
    {
        unselectColorFlower = flowerPanel.color;
        unselectColorGrass = grassPanel.color;
    }
    public void SelectFlowerEnv()
    {
        if (isSelected && currentLoadedEnvIndex != flowerEnvIndex)
        {
            currentLoadedEnvIndex = flowerEnvIndex;
            grassPanel.color = unselectColorGrass;
            flowerPanel.color = selectColor;
            return;
        }
        else if (!isSelected)
        {
            currentLoadedEnvIndex = flowerEnvIndex;
            flowerPanel.color = selectColor;
            isSelected = true;
            return;
        }
    }

    public void SelectGrassEnv()
    {
        if (isSelected && currentLoadedEnvIndex != grassEnvIndex)
        {
            currentLoadedEnvIndex = grassEnvIndex;
            flowerPanel.color = unselectColorFlower;
            grassPanel.color = selectColor;
            return;
        }
        else if (!isSelected)
        {
            currentLoadedEnvIndex = grassEnvIndex;
            grassPanel.color = selectColor;
            isSelected = true;
            return;
        }
    }

    public void LoadFlowerEnv()
    {
        unselectedWarning.SetActive(false);

        if (currentLoadedEnvIndex != flowerEnvIndex)
        {
            if (currentLoadedEnvIndex != -1)
            {
                SceneManager.UnloadSceneAsync(currentLoadedEnvIndex);
            }
            SceneManager.LoadSceneAsync(flowerEnvIndex, LoadSceneMode.Additive);
            currentLoadedEnvIndex = flowerEnvIndex;
        }
    }

    public void LoadGrassEnv()
    {
        unselectedWarning.SetActive(false);

        if (currentLoadedEnvIndex != grassEnvIndex)
        {
            if (currentLoadedEnvIndex != -1)
            {
                SceneManager.UnloadSceneAsync(currentLoadedEnvIndex);
            }
            SceneManager.LoadSceneAsync(grassEnvIndex, LoadSceneMode.Additive);
            currentLoadedEnvIndex = grassEnvIndex;
        }
    }

    public void LoadNextIfSelected()
    {
        if (currentLoadedEnvIndex == -1)
        {
            unselectedWarning.SetActive(true);
            return;
        }
        SceneManager.LoadSceneAsync(currentLoadedEnvIndex, LoadSceneMode.Additive);
        SetToInactive();
        nextPanel.SetActive(true);
    }

    private void SetToInactive()
    {
        envPanel.SetActive(false);
    }

}
