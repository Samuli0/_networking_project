using UnityEngine;

public interface ICommand
{
    // All classes that inherit must impliment methods
    void Execute();
    void Undo();

}
