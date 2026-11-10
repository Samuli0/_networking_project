using UnityEngine;

public class MoveRightCommand : ICommand
{
    private float _amount;
    private Transform _transform;
    public MoveRightCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        //_transform.Translate(_amount * Vector3.right, Space.Self);
        _transform.Rotate(15 * _amount * Vector3.up, Space.Self);
    }
    public void Undo()
    {
        _transform.Translate(-_amount * Vector3.right, Space.World);
    }
}