using UnityEngine;

public class UndoCommand
{
    private float _amount;
    private Transform _transform;
    public UndoCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = -amount;
    }

    public void Execute()
    {
        _transform.Translate(_amount * Vector3.right, Space.World);
    }
}
