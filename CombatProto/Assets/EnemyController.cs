using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject combat;
    private CombatController combatController;

    public GameObject head;
    public GameObject torso;
    public GameObject legs;

    public GameObject attackPoint;

    float focus = 0;

    float countingTime = 10;

    bool isStart;

    private List<int> selectedPoints = new List<int>();

    private void Start()
    {
        combatController = combat.GetComponent<CombatController>();
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == head.gameObject.transform)
            {
                selectedPoints.Add(0);
                Instantiate(attackPoint, hit.collider.transform);
                Debug.Log("head");
            }
            if (hit.collider != null && hit.collider.transform == torso.gameObject.transform)
            {
                selectedPoints.Add(1);
                Instantiate(attackPoint, hit.collider.transform);
                Debug.Log("torso");
            }
            if (hit.collider != null && hit.collider.transform == legs.gameObject.transform)
            {
                selectedPoints.Add(2);
                Instantiate(attackPoint, hit.collider.transform);
                Debug.Log("legs");
            }
        }
    }
}