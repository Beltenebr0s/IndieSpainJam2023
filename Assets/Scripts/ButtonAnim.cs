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
        _image = gameObject.GetComponent<Transform>().GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = unpressed;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = pressed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = unpressed;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = hover;
    }
}
