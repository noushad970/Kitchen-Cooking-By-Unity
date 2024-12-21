using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform PlateVisualPrefab;
    private List<GameObject> plateVisualGameObjectList;
    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemove += PlatesCounter_OnPlateRemove;
    }

    private void PlatesCounter_OnPlateRemove(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(PlateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0,plateOffsetY*plateVisualGameObjectList.Count,0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

}
