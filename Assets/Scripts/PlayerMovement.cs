using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_JoystickSize = new Vector2(300, 300);
    [SerializeField]
    private FloatingJoystick m_Joystick;
    [SerializeField]
    private PlayerController m_playerController;

    private Finger MovementFinger;
    private Vector2 MovementAmount;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }
    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = m_JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    m_Joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - m_Joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - m_Joystick.RectTransform.anchoredPosition;
            }

            m_Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            m_Joystick.Knob.anchoredPosition = Vector2.zero;
            m_Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }


    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width)
        {
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            m_Joystick.gameObject.SetActive(true);
            m_Joystick.RectTransform.sizeDelta = m_JoystickSize;
            m_Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < m_JoystickSize.x / 2)
        {
            StartPosition.x = m_JoystickSize.x / 2;
        }

        if (StartPosition.y < m_JoystickSize.y / 2)
        {
            StartPosition.y = m_JoystickSize.y / 2;
        }
        else if (StartPosition.y > Screen.height - m_JoystickSize.y / 2)
        {
            StartPosition.y = Screen.height - m_JoystickSize.y / 2;
        }

        return StartPosition;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        m_playerController.Move(MovementAmount);
    }
}
