using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    public List<GameObject> points;

    public GameObject focus;

    public float countingTime = 10f;
    
    private float focusVal;
    private Slider focusSlider;

    private bool isPlayerAction;

    private int playerScore;
    private int enemyScore;

    private int selected;
    
    void Start()
    {
        focusSlider = focus.GetComponent<Slider>();
        Next();
    }

    private void Update()
    {
        if (focusVal < 0)
        {
            focusVal = 0;
            Next();
        }
        else
        {
            OnClick();
            focusVal -= Time.deltaTime * countingTime;
        }
        focusSlider.value = focusVal;
        
    }

    private void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.transform == points[0].gameObject.transform)
                {
                    Debug.Log("Gracz głowa");
                    playerScore++;
                    Next();
                }
                if (hit.collider.transform == points[1].gameObject.transform)
                {
                    Debug.Log("Gracz tors");
                    playerScore++;
                    Next();
                }
                if (hit.collider.transform == points[2].gameObject.transform)
                {
                    Debug.Log("Gracz nogi");
                    playerScore++;
                    Next();
                }
                if (hit.collider.transform == points[3].gameObject.transform)
                {
                    Debug.Log("Przeciwnik głowa");
                    enemyScore++;
                    Next();
                }
                if (hit.collider.transform == points[4].gameObject.transform)
                {
                    Debug.Log("Przeciwnik tors");
                    enemyScore++;
                    Next();
                }
                if (hit.collider.transform == points[5].gameObject.transform)
                {
                    Debug.Log("Przeciwnik nogi");
                    enemyScore++;
                    Next();
                }
            }
        }
    }

    private void Next()
    {
        points.ForEach(go => go.gameObject.SetActive(false));
        NewRandom();
        focusVal = 100;
        Debug.Log("Gracz: " + playerScore + " - Przeciwnik: " + enemyScore);
    }

    private void NewRandom()
    {
        bool loop = true;
        while (loop)
        {
            int rand = Random.Range(0, 6);
            if (selected != rand)
            {
                selected = rand;
                points[rand].gameObject.SetActive(true);
                loop = false;
            }
        }
    }
}