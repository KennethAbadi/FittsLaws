using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandlerScript : MonoBehaviour
{
    public ColorChangingScript ColorChangingScript;
    public DataLoggerScript DataLoggerScript;

    private Camera _mainCamera;

    void Awake()
    {
        _mainCamera = Camera.main;
    }
    void Update()
    {
        DataLoggerScript.time += Time.deltaTime;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        DataLoggerScript.finaltime = DataLoggerScript.time;
        DataLoggerScript.time = 0;
        ColorChangingScript.changeColor(rayHit.collider.gameObject);
        
        Debug.Log(rayHit.collider.gameObject.name);
    }
}
