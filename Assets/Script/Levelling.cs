using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levelling : MonoBehaviour
{
    [SerializeField] private GameObject[] allCircuts;
    [SerializeField] private GameObject[] selectedCircut = new GameObject[6];
    [SerializeField] private GameObject winlocation;

    private GameObject normalcircut, winCircut;
    public int level = 1, circutsAddition = 12;
    [SerializeField] private float rotationSpeed = 10f;
    private float i = 0;
    public Material plateMaterial, baseMaterial;
    public MeshRenderer playerMesh;

    void Awake()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        plateMaterial.color = Random.ColorHSV(0, 1, .5f, 1, 1, 1);
        baseMaterial.color = plateMaterial.color + Color.gray;
        playerMesh.material.color = plateMaterial.color;

        level = PlayerPrefs.GetInt("Level", 1);
        //if (level > 20)
        //    circutsAddition = 0;

        CircutSelection();

        for (i = 0; i > -level - circutsAddition; i -= 1f)
        {
            if (level <= 5)
                normalcircut = Instantiate(selectedCircut[Random.Range(0, 3)]);
            if (level > 5 && level <= 10)
                normalcircut = Instantiate(selectedCircut[Random.Range(1, 4)]);
            if (level > 10 && level <= 15)
                normalcircut = Instantiate(selectedCircut[Random.Range(3, 5)]);
            if (level > 15)
                normalcircut = Instantiate(selectedCircut[Random.Range(4, 6)]);

            normalcircut.transform.position = new Vector3(0, i, 0);
            normalcircut.transform.eulerAngles = new Vector3(0, i * rotationSpeed, 0);

            if (Mathf.Abs(i) >= level * .4f && Mathf.Abs(i) <= level * .8f)
            {
                normalcircut.transform.eulerAngles += Vector3.up * 180;
            }

            normalcircut.transform.parent = FindObjectOfType<TowerRation>().transform;
        }

        winCircut = Instantiate(winlocation);
        winCircut.transform.position = new Vector3(0, i, 0);
    }

    void CircutSelection()
    {
        int randomModel = Random.Range(0, 5);
        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 6; i++)
                    selectedCircut[i] = allCircuts[i];
                break;
            case 1:
                for (int i = 0; i < 6; i++)
                    selectedCircut[i] = allCircuts[i + 6];
                break;
            case 2:
                for (int i = 0; i < 6; i++)
                    selectedCircut[i] = allCircuts[i + 12];
                break;
            case 3:
                for (int i = 0; i < 6; i++)
                    selectedCircut[i] = allCircuts[i + 18];
                break;
            case 4:
                for (int i = 0; i < 6; i++)
                    selectedCircut[i] = allCircuts[i + 24];
                break;
        }
    }
    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }
}
