using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private string inputName;

    private void Update()
    {
        Debug.Log(inputName);
    }

    public bool GetButton(string axes)
    {
        return Input.GetButton($"{inputName}{axes}");
    }

    public float GetAxis(string axes)
    {
        return Input.GetAxis($"{inputName}{axes}");
    }
}
