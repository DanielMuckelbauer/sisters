using System.Collections.Generic;

namespace Code.Scripts.Entity
{
    public class Pollin : Player
    {
        protected override void Start()
        {
            base.Start();
            InitializeControls();
        }

        private void InitializeControls()
        {
            Controls = new Dictionary<Control, string>
            {
                {Control.Horizonal, "HorizontalPollin"},
                {Control.Jump, "JumpPollin"},
                {Control.Strike, "StrikePollin"}
            };
        }
    }
}