namespace YojigenPoint.VaultPrime.Helpers
{
    /// <summary>
    /// Provides functionality to generate sequential GUIDs (COMB GUIDs).
    /// </summary>
    public static class GuidGenerator
    {
        /// <summary>
        /// Generates a new COMB GUID, which combines a random GUID with a timestamp.
        /// This ensures that GUIDs are generated in a sequential order, which can
        /// significantly improve database index performance.
        /// </summary>
        /// <returns>A new, sequential Guid.</returns>
        public static Guid GenerateCombGuid()
        {
            // Start with a standard random GUID.
            Span<byte> guidBytes = stackalloc byte[16];
            Guid.NewGuid().TryWriteBytes(guidBytes);

            // Get the current UTC time and its ticks. Ticks are a 64-bit (8-byte) value.
            long timestampTicks = DateTime.UtcNow.Ticks;

            // Convert the ticks to a byte span.
            Span<byte> timestampBytes = stackalloc byte[8];
            BitConverter.TryWriteBytes(timestampBytes, timestampTicks);

            // The last 6 bytes of a GUID are the "node" and are random.
            // We will replace these with the last 6 bytes of the timestamp.
            // This ensures the GUIDs are sequential while keeping the first parts random.
            // We use the last 6 bytes because the first 2 bytes of a timestamp change very rarely.
            const int timestampByteOffset = 2;
            const int guidByteOffset = 10;
            const int bytesToCopy = 6;

            timestampBytes.Slice(timestampByteOffset, bytesToCopy).CopyTo(guidBytes.Slice(guidByteOffset, bytesToCopy));

            // Create and return the new GUID from the modified byte array.
            return new Guid(guidBytes);
        }
    }
}