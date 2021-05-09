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
    float countingFinisherTime = 30;

    bool isStart;
    bool isFinisher;
    bool isFinish;

    private Vector3 finisherScale;

    private List<GameObject> pointsObject = new List<GameObject>();

    private List<int> randomPoints = new List<int>();
    private List<int> selectedPoints = new List<int>();

    private void Start()
    {
        combatController = combat.GetComponent<CombatController>();
        finisher.gameObject.SetActive(false);
        finisherScale = finisher.transform.localScale;
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
                if (CheckIsFinisher())
                {
                    Debug.Log("Finisher");
                    finisher.gameObject.SetActive(true);
                    focus = 100;
                    isFinisher = true;
                }
                else
                {
                    pointsObject.ForEach(o => Destroy(o));
                    selectedPoints = new List<int>();
                    combatController.Next();
                }
                isStart = false;
            }
        }
        if (isFinisher)
        {
            if (focus > 0 && !isFinish)
            {
                OnClick();
                focus -= Time.deltaTime * countingFinisherTime;
                finisher.transform.localScale = new Vector2(finisherScale.x * (focus / 100) + 1f, finisherScale.y * (focus / 100) + 1f);
            }
            else if (!isFinish)
            {
                selectedPoints.ForEach(i => Destroy(pointsObject[i]));
                selectedPoints = new List<int>();
                combatController.Next();
                isFinisher = false;
            }
        }
    }

    bool CheckIsFinisher()
    {
        bool isCheck = false;
        if (selectedPoints.Count == randomPoints.Count)
        {
            isCheck = true;
            selectedPoints.ForEach(i => isCheck = !isCheck || randomPoints[i] != selectedPoints[i]);
        }
        return isCheck;
    }

    public void StartAttack()
    {
        focus = 100;
        isStart = true;
        Debug.Log("Wylosowana kolejność obrony przeciwnika:");
        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, 3);
            randomPoints.Add(random);
            Debug.Log(random);
        }
    }

    private void OnClick()
    {
        bool isAdd = false;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (isStart)
            {
                if (hit.collider != null && hit.collider.transform == head.gameObject.transform)
                {
                    selectedPoints.Add(0);
                    pointsObject.Add(Instantiate(attackPoint, hit.collider.transform.position, Quaternion.identity));
                    isAdd = true;
                    Debug.Log("head [0]");
                }
                if (hit.collider != null && hit.collider.transform == torso.gameObject.transform)
                {
                    selectedPoints.Add(1);
                    pointsObject.Add(Instantiate(attackPoint, hit.collider.transform.position, Quaternion.identity));
                    isAdd = true;
                    Debug.Log("torso [1]");
                }
                if (hit.collider != null && hit.collider.transform == legs.gameObject.transform)
                {
                    selectedPoints.Add(2);
                    pointsObject.Add(Instantiate(attackPoint, hit.collider.transform.position, Quaternion.identity));
                    isAdd = true;
                    Debug.Log("legs [2]");
                }
            }
            if (isFinisher)
            {
                if (hit.collider != null && hit.collider.transform == finisher.gameObject.transform)
                {
                    isFinish = true;
                    Debug.LogWarning("Wygrałeś starcie!");
                }
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