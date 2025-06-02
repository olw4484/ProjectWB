using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    //public Vector2 AimInput { get; private set; }
    public bool AimPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool DodgePressed { get; private set; }

    void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //AimInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        AimPressed = Input.GetButton("Fire2");     // 우클릭
        AttackPressed = Input.GetButtonDown("Fire1"); // 좌클릭
        DodgePressed = Input.GetKeyDown(KeyCode.Space);
    }

    public void ResetInputs()
    {
        AttackPressed = false;
        DodgePressed = false;
    }
}
