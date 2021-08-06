namespace FrostByte
{
    class Buttons
    {
        public bool Enter;
        public bool Up, Down, Left, Right;
        public bool W, S, A, D;
        public int Pause;

        public Buttons()
        {
            Enter = false;
            Up = Down = Left = Right = false;
            W = A = S = D = false;
            Pause = 0;
        }
    }
}
