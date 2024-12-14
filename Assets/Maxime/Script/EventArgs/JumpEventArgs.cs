using System;

namespace Maxime.Script.EventArgs
{
    public class JumpEventArgs : System.EventArgs
    {
        public bool IsJump { get; private set; }

        public JumpEventArgs(bool isJump)
        {
            this.IsJump = isJump;
        }
    }
}