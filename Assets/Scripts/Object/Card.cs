using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int originNum;
    public bool isSelected = false;
    public bool isMine;
    public string cName;
    public CardShape shape;
    public int number;
    public bool front;
    public Transform parent;
    [SerializeField]
    private Image frontImg;
    [SerializeField]
    private Image backImg;
    [SerializeField]
    private Vector2 velocity;
    private bool isMove = false;
    public float speed;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
		if (parent != null && isMove)
		{
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0.0f, 0.0f, transform.localPosition.z), fractionOfJourney);

			if (transform.localPosition.x <= 0.001f && transform.localPosition.y <= 0.001f)
			{
                transform.localPosition = new Vector3(0.0f, 0.0f, transform.localPosition.z);
                isMove = false;
			}
		}
    }

    public void CardSelect()
	{
		if (!backImg.enabled)
		{
            transform.Translate(Vector3.up * 50.0f);
            isSelected = true;
		}
	}

    public void CardDeselect()
	{
		if (!backImg.enabled)
		{
            transform.Translate(Vector3.down * 50.0f);
            isSelected = false;
		}
	}
    public void SetParent(Transform parent, float speed)
	{
        this.parent = parent;

        transform.SetParent(parent);
        this.speed = speed;
        startTime = Time.time;
        isMove = true;
        journeyLength = Vector3.Distance(transform.localPosition, new Vector3(0, 0, transform.localPosition.z));
	}

    public GameObject CardSelected()
	{
		if (isMine)
		{
            return gameObject;
		}
		else
		{
            return null;
		}
	}

    public void SetFrontImage(Sprite img)
	{
        frontImg.sprite = img;
	}
    public void SetBackImage(Sprite img)
	{
        backImg.sprite = img;
	}

    public void ShowCard()
	{
        backImg.enabled = false;
	}
    public void HideCard()
	{
        backImg.enabled = true;
	}

    public bool IsShowed()
	{
        return backImg.enabled;
	}
}
