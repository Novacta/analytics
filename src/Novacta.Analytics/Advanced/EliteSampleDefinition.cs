namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Contains constants for controlling if the elite sample points of a 
    /// Cross-Entropy context must be defined as those corresponding 
    /// to the lowest or highest performances of the system under study.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An <see cref="EliteSampleDefinition"/> constant can be used to 
    /// specify 
    /// if the <see cref="CrossEntropyProgram.Run(
    /// CrossEntropyContext, int, double)">Run</see> 
    /// of a given Cross-Entropy program must define its elite sample 
    /// points 
    /// as those corresponding 
    /// to the lowest or highest performances of the system under study. 
    /// </para>
    /// <para>
    /// For a thorough description of the intended use 
    /// of <see cref="EliteSampleDefinition"/> 
    /// constants, see the remarks 
    /// about the <see cref="CrossEntropyProgram"/> class.
    /// </para>
    /// </remarks>
    /// <seealso cref="CrossEntropyProgram"/>
    public enum EliteSampleDefinition 
    {
        /// <summary>
        /// The elite sample points of a 
        /// Cross-Entropy context correspond
        /// to the highest performances of the system under study.
        /// </summary>
        HigherThanLevel,
        /// <summary>
        /// The elite sample points of a 
        /// Cross-Entropy context correspond
        /// to the lowest performances of the system under study.
        /// </summary>
        LowerThanLevel
    }
}
