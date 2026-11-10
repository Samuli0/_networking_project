using UnityEngine;

public class MoveBackCommand : ICommand
{
    private float _amount;
    private Transform _transform;
    public MoveBackCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        _transform.Translate(_amount * Vector3.back, Space.Self);
    }
    public void Undo()
    {
        _transform.Translate(-_amount * Vector3.back, Space.World);
    }
}
