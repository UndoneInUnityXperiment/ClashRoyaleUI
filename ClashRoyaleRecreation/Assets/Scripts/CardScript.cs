using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI costShown;
    public int cost;
    public bool inPosition;
    public Animator anim;
    public float speed = 0.05f;
    public Image whiteOver;

    private void Awake()
    {
        anim = gameObject.GetComponentInParent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        costShown.text = cost.ToString();
        inPosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPosition)
        {
            transform.parent.position = Vector3.Lerp(gameObject.transform.position,
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().emptyPosition, speed);

            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().emptyPosition) < 0.1)
            {
                inPosition = true;
            }
        }
        CheckCost();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cost < GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentElixir)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentElixir -= cost;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().emptyPosition = gameObject.transform.position;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().newCard.GetComponentInChildren<CardScript>().MoveNewCard();
            Destroy(gameObject);
        }
    }

    public void MoveNewCard()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SpawnCard();
        anim.Play("ScaleAnim");
        inPosition = false;
    }

    public void SetSmall()
    {
        anim.Play("DefaultAnim");
    }

    public void CheckCost()
    {
        if (cost < GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentElixir)
        {
            whiteOver.GetComponent<Image>().enabled = false;
        }
        else
        {
            whiteOver.GetComponent<Image>().enabled = true;
            whiteOver.fillAmount = 1 - (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentElixir / cost);
        }
    }
}