namespace TerraTyping.Core
{
    public struct Boost
    {
        private float multiplier;
        public string reason;

        public float Multiplier { get => multiplier + 1; set => multiplier = value - 1; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiplier">Automatically subtracts 1 for you.</param>
        /// <param name="reason"></param>
        public Boost(float multiplier, string reason)
        {
            this.multiplier = multiplier - 1;
            this.reason = reason;
        }
    }
}
