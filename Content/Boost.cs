namespace TerraTyping
{
    public struct Boost
    {
        private float multiplier;
        public string reason;

        public float Multiplier { get => multiplier + 1; set => multiplier = value - 1; }

        public Boost(float multiplier, string reason)
        {
            this.multiplier = multiplier - 1;
            this.reason = reason;
        }
    }
}
