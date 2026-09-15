using UnityEngine;

[System.Serializable]
public class PlayerInput
{
    public int deviceId;
    public Device device;
    public bool[] fred;
    public bool strumPressed;
    public bool startPressed;
    public bool starPressed;
    public float tilt, whammy;

    public enum Device
    {
        Keyboard,
        Xinput
    }

    public PlayerInput(Device _device, int _deviceId)
    {
        device = _device;
        deviceId = _deviceId;
        fred = new bool[5];
    }

    public void Update()
    {
        // Cinco notas del teclado: verde, rojo, amarillo, azul y naranja.
        bool keyGreen = Input.GetKey(KeyCode.A);
        bool keyRed = Input.GetKey(KeyCode.S);
        bool keyYellow = Input.GetKey(KeyCode.D);
        bool keyBlue = Input.GetKey(KeyCode.F);
        bool keyOrange = Input.GetKey(KeyCode.G);

        // Conserva también el soporte para gamepad.
        fred[0] = keyGreen || XInput.GetButton(deviceId, XInput.Button.X);
        fred[1] = keyRed || XInput.GetButton(deviceId, XInput.Button.A);
        fred[2] = keyYellow || XInput.GetButton(deviceId, XInput.Button.B);
        fred[3] = keyBlue || XInput.GetButton(deviceId, XInput.Button.Y);
        fred[4] = keyOrange || XInput.GetButton(deviceId, XInput.Button.LB);

        // En teclado, presionar una nota también realiza el rasgueo.
        bool keyboardStrum =
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.F) ||
            Input.GetKeyDown(KeyCode.G);

        bool gamepadStrum =
            XInput.GetButtonDown(deviceId, XInput.Button.DPadDown) ||
            XInput.GetButtonDown(deviceId, XInput.Button.DPadUp);

        strumPressed = keyboardStrum || gamepadStrum;
        startPressed = Input.GetKeyDown(KeyCode.Return) ||
                       XInput.GetButtonDown(deviceId, XInput.Button.Start);
        starPressed = Input.GetKeyDown(KeyCode.Space) ||
                      XInput.GetButtonDown(deviceId, XInput.Button.RB);

        tilt = XInput.GetAxis(deviceId, XInput.Axis.RY);
        whammy = XInput.GetAxis(deviceId, XInput.Axis.RX);
    }
}
