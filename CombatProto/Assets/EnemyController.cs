using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject combat;
    private CombatController combatController;

    public GameObject head;
    public GameObject torso;
    public GameObject legs;
    public GameObject finisher;

    public GameObject attackPoint;

    public float focus = 0;

    float countingTime = 10;

    bool isStart;

    private List<GameObject> pointsObject = new List<GameObject>();

    private List<int> selectedPoints = new List<int>();

    private void Start()
    {
        combatController = combat.GetComponent<CombatController>();
        finisher.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isStart)
        {
            if (focus > 0 && selectedPoints.Count < 3)
            {
                OnClick();
                focus -= Time.deltaTime * countingTime;
            }
            else
            {
                selectedPoints.ForEach(i => Destroy(pointsObject[i].gameObject));
                selectedPoints = new List<int>();
                isStart = false;
                combatController.Next();
            }
        }
    }

    public void StartAttack()
    {
        focus = 100;
        isStart = true;
    }

    private void OnClick()
    {
        bool isAdd = false;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == head.gameObject.transform)
            {
                selectedPoints.Add(0);
                pointsObject.Add(Instantiate(attackPoint, hit.collider.transform.position, Quaternion.identity));
                isAdd = true;
                Debug.Log("head");
            }
            if (hit.collider != null && hit.collider.transform == torso.gameObject.transform)
            {
                selectedPoints.Add(1);
                pointsObject.Add(Instantiate(attackPoint, hit.collider.transform.position, Quaternion.identity));
                isAdd = true;
                Debug.Log("torso");
            }
            if (hit.collider != null && hit.collider.transform == legs.gameObject.transform)
            {
                selectedPoints.Add(2);
                pointsObject.Add(Instantiate(attackPoint, hit.collider.transform.position, Quaternion.identity));
                isAdd = true;
                Debug.Log("legs");
            }
        }
        if (isAdd)
        {
            float step = 100 / 3;
            if (focus > step * 2)
            {
                focus = step * 2;
            }
            else if (focus > step)
            {
                focus = step;
            }
            else
            {
                focus = 0;
            }
        }
    }
}