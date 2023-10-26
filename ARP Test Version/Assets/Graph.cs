﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using CodeMonkey.Utils;

public class Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        //CreateCircle(new Vector2(200, 200));
        List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 25, 37, 40, 36, 33 };
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectransform = gameObject.GetComponent<RectTransform>();
        rectransform.anchoredPosition = anchoredPosition;
        rectransform.sizeDelta = new Vector2(11, 11);
        rectransform.anchorMin = new Vector2(0, 0);
        rectransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    //Receive and Display the values what we want to graph.
    private void ShowGraph(List<int> valueList)
    {
        //Top limit of graph in the Y axis.
        float graphHeight = graphContainer.sizeDelta.y;
        //Maximun value to Y axis.
        float yMaximun = 50f;
        GameObject lastCircleGameObject = null;

        //Distance between each point on the X axis
        float xSize = 25f;
        for(int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximun) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectransform.anchorMin = new Vector2(0, 0);
        rectransform.anchorMax = new Vector2(0,0);
        rectransform.sizeDelta = new Vector2(distance, 3f);
        rectransform.anchoredPosition = dotPositionA + dir *distance * .5f;
        rectransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}
