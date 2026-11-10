using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private float _amount;
    private Transform _transform;
    public MoveLeftCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        //_transform.Translate(_amount * Vector3.left, Space.Self);
        _transform.Rotate(15 * _amount * Vector3.down, Space.Self);
    }
    public void Undo()
    {
        _transform.Translate(-_amount * Vector3.left, Space.World);
    }
}