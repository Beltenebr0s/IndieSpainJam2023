using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UnityEngine.UI.Image _image;
    [SerializeField] private Sprite unpressed, hover, pressed;

    private void Start()
    {
        _image = gameObject.GetComponent<Transform>().GetChild(1).GetComponent<UnityEngine.UI.Image>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonAnimCoroutine();
    }

    IEnumerator ButtonAnimCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _image.sprite = unpressed;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        _image.sprite = pressed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = unpressed;
        this.transform.GetChild(0).GetComponent<Scrolling>().x = 0;
        this.transform.GetChild(0).GetComponent<Scrolling>().y = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = hover;
        this.transform.GetChild(0).GetComponent<Scrolling>().x = -0.03f;
        this.transform.GetChild(0).GetComponent<Scrolling>().y = -0.01f;
    }
}
