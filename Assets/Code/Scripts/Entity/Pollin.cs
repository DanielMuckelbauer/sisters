using System.Collections.Generic;

namespace Code.Scripts.Entity
{
    public class Pollin : Player
    {
        protected override void Awake()
        {
            base.Awake();
            InitializeControls();
        }

        private void InitializeControls()
        {
            Controls = new Dictionary<Control, string>
            {
                {Control.Horizontal, "HorizontalPollin"},
                {Control.Jump, "JumpPollin"},
                {Control.Strike, "StrikePollin"}
            };
        }
    }
}