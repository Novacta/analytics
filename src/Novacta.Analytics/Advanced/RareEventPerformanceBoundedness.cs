// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Contains constants for controlling if the rare event whose  
    /// probability must be evaluated by a Cross-Entropy estimator
    /// includes states of the system under study whose performances 
    /// are upper or lower bounded.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Events whose
    /// probabilities can be evaluated by a Cross-Entropy 
    /// estimator are characterized in terms of the performance of a system.
    /// Let <latex mode='inline'>\lambda</latex> be
    /// an extreme performance value. 
    /// Rare events are sets of states whose performances are lower or upper bounded 
    /// by <latex mode='inline'>\lambda</latex>, i.e. they can be defined as 
    /// <latex mode="display">E_L\!\left(\lambda\right)=\{x\in\mathcal{X}|\lambda\leq H\!\left(x\right)\}</latex> 
    /// or as
    /// <latex mode="display">E_U\!\left(\lambda\right)=\{x\in\mathcal{X}|H\!\left(x\right)\leq\lambda\},</latex> 
    /// where <latex mode='inline'>\mathcal{X}</latex> is the state space 
    /// of the system and 
    /// <latex mode='inline'>H\!\left(x\right)</latex> is the function 
    /// returning the system performance at state  
    /// <latex mode='inline'>x</latex>. 
    /// </para>
    /// <para>
    /// Constant <see cref="Lower"/> is intended to 
    /// signal 
    /// <latex mode='inline'>E_L</latex> events, while 
    /// constant <see cref="Upper"/> 
    /// corresponds to <latex mode='inline'>E_U</latex> events.
    /// </para>
    /// <para>
    /// For a thorough description of the intended use of <see cref="RareEventPerformanceBoundedness"/> 
    /// constants, see the remarks 
    /// about method <see cref="RareEventProbabilityEstimator.Estimate(
    /// RareEventProbabilityEstimationContext, double, int, int)">Estimate</see>.
    /// </para>
    /// </remarks>
    /// <seealso cref="RareEventProbabilityEstimator"/>
    public enum RareEventPerformanceBoundedness
    { 
        /// <summary>
        /// The rare event includes states whose performances are lower 
        /// bounded.
        /// </summary>
        Lower = 0,
        /// <summary>
        /// The rare event includes states whose performances are upper 
        /// bounded.
        /// </summary>
        Upper = 1
    }
}
