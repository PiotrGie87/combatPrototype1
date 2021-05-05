using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonControler : MonoBehaviour
{
    [SerializeField] float countingTime = 1;

    public List <GameObject> points;

    public GameObject head;
    public GameObject torso;
    public GameObject legs;

    float focus = 0;

    private List<int> randomPoints = new List<int>();




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
    }

    
    

    // Update is called once per frame
    void Update()
    {
        

        

        if (focus > 0)
        {
            OnClick();
            focus -= Time.deltaTime * countingTime;
            Debug.Log(focus);
            
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
                //points[0]

         
                Debug.Log("yyy");
            }
            if(hit.collider != null && hit.collider.transform == torso.gameObject.transform)
            {
                Debug.Log("xxx");
            }
            if(hit.collider != null && hit.collider.transform == legs.gameObject.transform)
            {
                Debug.Log("zzz");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == head.gameObject.transform)
            {
                Debug.Log("aaa");
            }
            if (hit.collider != null && hit.collider.transform == torso.gameObject.transform)
            {
                Debug.Log("bbb");
            }
            if (hit.collider != null && hit.collider.transform == legs.gameObject.transform)
            {
                Debug.Log("ccc");
            }




        }
    }
}
