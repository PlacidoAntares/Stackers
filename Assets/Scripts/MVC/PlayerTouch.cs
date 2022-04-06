
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerTouch : Singleton<PlayerTouch>
{
    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position);
    public event EndTouchEvent OnEndTouch;

    private PlayerTouchControls touchControls;

    private void Awake()
    {
        touchControls = new PlayerTouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Started:" + touchControls.Touch.TouchPosition.ReadValue<Vector2>()) ;
        if (OnStartTouch != null)
        {
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Ended");
        if (OnEndTouch != null)
        {
            OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }
}
