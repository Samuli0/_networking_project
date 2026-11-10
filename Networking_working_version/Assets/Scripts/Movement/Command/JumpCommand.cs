using UnityEngine;
using UnityEngine.UIElements;

public class JumpCommand : ICommand
{
   
        private float _JumpForce;
        private Rigidbody _rb;
        public JumpCommand(Rigidbody rb, float JumpForce)
        {
            _rb = rb;
            _JumpForce = JumpForce;
        }
        

        public void Execute()
        {
            _rb.AddForce(Vector3.up * _JumpForce, ForceMode.Impulse); // jump
        }
    public void Undo()
    {

    }

}
