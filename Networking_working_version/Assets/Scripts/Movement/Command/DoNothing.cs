using UnityEngine;

public class DonNothing : ICommand
{
    private float _amount;
    private Transform _transform;
    public DonNothing(Transform transform, float amount)
    {

    }

    public void Execute()
    {

    }
    public void Undo()
    {

    }
}
