using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        CreateCircle(new Vector2(200, 200));
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectransform = gameObject.GetComponent<RectTransform>();
        rectransform.anchoredPosition = anchoredPosition;
        rectransform.sizeDelta = new Vector2(11, 11);
        rectransform.anchorMin = new Vector2(0, 0);
        rectransform.anchorMax = new Vector2(0, 0);
    }

}
