using System.Collections.Generic;

namespace Code.Scripts.Entity
{
    public class Muni : Player
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
                {Control.Horizontal, "Horizontal"},
                {Control.Jump, "JumpMuni"},
                {Control.Strike, "StrikeMuni"}
            };
        }
    }
}