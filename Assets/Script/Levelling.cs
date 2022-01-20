using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelling : MonoBehaviour
{
    [SerializeField] private GameObject[] allCircut;
    [SerializeField] private GameObject[] selectedCircut = new GameObject[4];
    [SerializeField] private GameObject winlocation;

    private GameObject normalcircut, winCircut;
    public int level = 1, platformAddition = 7;
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
        if (level > 6)
            platformAddition = 0;

        CircutSelection();

        for (i = 0; i > -level - platformAddition; i -= 0.5f)
        {
            if (level <= 5)
                normalcircut = Instantiate(selectedCircut[Random.Range(0, 2)]);
            if (level > 5 && level <= 10)
                normalcircut = Instantiate(selectedCircut[Random.Range(1, 3)]);
            if (level > 10 && level <= 15)
                normalcircut = Instantiate(selectedCircut[Random.Range(2, 4)]);
            if (level > 15)
                normalcircut = Instantiate(selectedCircut[Random.Range(3, 4)]);

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
        int randomModel = Random.Range(0, 3);
        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                    selectedCircut[i] = allCircut[i];
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                    selectedCircut[i] = allCircut[i + 2];
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                    selectedCircut[i] = allCircut[i + 4];
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                    selectedCircut[i] = allCircut[i + 8];
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                    selectedCircut[i] = allCircut[i + 10];
                break;
        }
    }
}
