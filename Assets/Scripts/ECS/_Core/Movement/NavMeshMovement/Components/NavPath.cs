namespace Client
{
    internal struct HasPath
    {
        public PathProvider Path;
        public int CurrentPathPointIndex;
        public float MoveSpeed;
        public float CompleteRadius;
    }
    
    internal struct TimerForPathIdle
    {
        public float Value;
    }
}