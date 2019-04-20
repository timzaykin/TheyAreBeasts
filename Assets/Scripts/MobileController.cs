using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TAB { 

    public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public bool isFireController = false;

        //private Image JoystickBG;
        //[SerializeField]
        //private Image Joystick;
        //private Vector2 inputVector;

        public void Start()
        {
            //JoystickBG = GetComponent<Image>();
            //Joystick = transform.GetChild(0).GetComponent<Image>();
        }


        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);

            if (isFireController) {
                InputController.Instance.StartFire();
            }

        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            //inputVector = Vector2.zero;
            //Joystick.rectTransform.anchoredPosition = Vector2.zero;

            if (isFireController)
            {
                InputController.Instance.StopFire();
            }
        }


        public virtual void OnDrag(PointerEventData eventData)
        {
            //Vector2 pos;
            //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos)) {
            //    pos.x = (pos.x / JoystickBG.rectTransform.sizeDelta.x);
            //    pos.y = (pos.y / JoystickBG.rectTransform.sizeDelta.y);

            //    inputVector = new Vector2(pos.x, pos.y);
            //    inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //    Joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (JoystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (JoystickBG.rectTransform.sizeDelta.y / 2));
            //}
        }

        public float Horisontal() {
            return Input.GetAxis("Horizontal");
        }

        public float Vertial() {
            return Input.GetAxis("Vertical");
        }

        //public Vector3 getDirection() {
        //    if (inputVector != Vector2.zero) return new Vector3(inputVector.y*-1f,0, inputVector.x);
        //    else return Vector3.zero;
        //}
    }
}
