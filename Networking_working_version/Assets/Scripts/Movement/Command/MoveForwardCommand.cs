using UnityEngine;

public class MoveForwardCommand : ICommand
{
    private float _amount;
    private Transform _transform;
    public MoveForwardCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        _transform.Translate(_amount*Vector3.forward, Space.Self);
    }
    public void Undo()
    {
        _transform.Translate(-_amount * Vector3.forward, Space.World);
    }
}
