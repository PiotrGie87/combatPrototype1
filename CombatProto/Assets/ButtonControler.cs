using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ButtonControler : MonoBehaviour
{
    [SerializeField] float countingTime = 1;

    public List <GameObject> points;

    public GameObject head;
    public GameObject torso;
    public GameObject legs;

    float focus = 0;

    private List<int> randomPoints = new List<int>();
    private List<int> selectedPoints = new List<int>();

    private bool isStart;





    // Start is called before the first frame update
    void Start()
    {
        

        for (int i = 0; i < 3; i++)
        {
            randomPoints.Add(Random.Range(0, 6));

        }


        StartCoroutine(ShowRandomPoints());
        
    }

    private IEnumerator ShowRandomPoints()
    {
        for (int i = 0; i < 3; i++)
        {
            int value = randomPoints[i];
            points[value].GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log(value);
            yield return new WaitForSeconds(1f);
            points[value].GetComponent<SpriteRenderer>().color = Color.green; // trudniejsza wersja :)

        }

        focus = 100;
        isStart = true;
    }

    
    

    // Update is called once per frame
    void Update()
    {

        if(randomPoints.Count == selectedPoints.Count)
        {
            focus = 0;
        }


        if (focus > 0)
        {
            OnClick();
            focus -= Time.deltaTime * countingTime;
            //Debug.Log(focus);

        }
        else if (isStart)
        {
            randomPoints.ForEach(i => Debug.Log(i));
            
            selectedPoints.ForEach(i => Debug.Log(i));
            

            if (randomPoints.SequenceEqual(selectedPoints))
            {
                Debug.Log("Wygra�e�");
            }
            else
            {
                Debug.Log("Przegra�e�");
            }

            isStart = false;
        }

    }

    private void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == head.gameObject.transform)
            {
                selectedPoints.Add(0);

         
                Debug.Log("000");
            }
            if(hit.collider != null && hit.collider.transform == torso.gameObject.transform)
            {
                selectedPoints.Add(2);
                Debug.Log("222");
            }
            if(hit.collider != null && hit.collider.transform == legs.gameObject.transform)
            {
                selectedPoints.Add(4);
                Debug.Log("444");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == head.gameObject.transform)
            {
                selectedPoints.Add(1);
                Debug.Log("111");
            }
            if (hit.collider != null && hit.collider.transform == torso.gameObject.transform)
            {
                selectedPoints.Add(3);
                Debug.Log("333");
            }
            if (hit.collider != null && hit.collider.transform == legs.gameObject.transform)
            {
                selectedPoints.Add(5);
                Debug.Log("555");
            }




        }
    }
}
