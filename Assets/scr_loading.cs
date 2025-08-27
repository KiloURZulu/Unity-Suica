using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scr_loading : MonoBehaviour
{
    public Canvas CanvasUI;
    public Canvas Canvas_Pop;
    public Canvas Canvas_Loading;
    public GameObject GameArea;


    [SerializeField] private float loadingFill = 0f;
    public Image image__loadingFG; // UI Image for loading fill
    public TMP_Text loadingText; // TextMeshPro for loading text

    void Awake()
    {
        loadingFill = 0f;
        CanvasUI.enabled = false;
        Canvas_Pop.enabled = false;
        GameArea.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadGameArea());
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingFill < 1f)
        {
            // Update UI based on loading progress
            image__loadingFG.fillAmount = loadingFill;
            loadingText.text = "LOADING " + Mathf.Ceil(loadingFill * 100) + " %";
        }
        else
        {
            CanvasUI.enabled = true;
            //Canvas_Pop.enabled = true; // Uncomment if needed
            GameArea.SetActive(true);
            Canvas_Loading.enabled = false;
        }
    }

    IEnumerator LoadGameArea()
    {
        // Simulate loading process
        while (loadingFill < 1f)
        {
            // Simulating loading progress increment
            loadingFill += Time.deltaTime * 0.2f; // Adjust speed as necessary
            yield return null;
        }

        loadingFill = 1f;
    }
}