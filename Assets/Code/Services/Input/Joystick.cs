using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Services.Input
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Image _joyArea;
        [SerializeField] private Image _stick;

        public Vector2 Direction { get; private set; }

        private Vector2 _sizeJoyArea;

        private void Start() => 
            _sizeJoyArea = _joyArea.rectTransform.sizeDelta;

        public void OnPointerDown(PointerEventData eventData) => 
            OnDrag(eventData);

        public void OnPointerUp(PointerEventData eventData)
        {
            _stick.rectTransform.anchoredPosition = Vector2.zero;
            Direction = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joyArea.rectTransform, eventData.position, eventData.pressEventCamera, out position);

            _stick.rectTransform.anchoredPosition = Vector2.ClampMagnitude(new Vector2(position.x, position.y), _sizeJoyArea.x * 0.5f);

            Direction = position.normalized;
        }
    }
}