namespace EnigmaLibrary.Models.Classic.Components
{
    public class RotorStepMessage
    {
        public RotorStepMessage(int step)
        {
            Steps = step;
        }

        public int Steps { get; }
    }
}