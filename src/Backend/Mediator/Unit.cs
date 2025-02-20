namespace Backend.Mediator
{
    /// <summary>
    /// Represents a placeholder for empty return values in Mediator requests.
    /// </summary>
    public sealed class Unit
    {
        /// <summary>
        /// Gets the singleton instance of the <see cref="Unit"/> class.
        /// </summary>
        public static Unit Value { get; } = new Unit();

        // Private constructor to prevent instantiation.
        private Unit() { }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Unit;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "()";
        }
    }
}
