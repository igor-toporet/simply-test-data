using System;
using System.Collections.Generic;

namespace SimplyTestData
{
    /// <summary>
    /// Stores customization actions for multiple types.
    /// </summary>
    public interface ICustomizationsContainer
    {
        /// <summary>
        /// Adds list of customization actions for type <typeparam name="T">T</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="customizations">
        /// Customization actions for type <typeparam name="T">T</typeparam>.
        /// </param>
        void AddForType<T>(params Action<T>[] customizations);

        /// <summary>
        /// Clears all stored customization actions for all types.
        /// </summary>
        void ClearAll();

        /// <summary>
        /// Clears all customization actions for type <typeparam name="T">T</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void ClearForType<T>();

        /// <summary>
        /// Gets all customization actions applicable to type <typeparam name="T">T</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>All customization actions applicable to type <typeparam name="T">T</typeparam>.</returns>
        /// <remarks>
        /// The returned customizations are not necessarily the only ones previously added
        /// but can contain reduced or excessive set of customizations.
        /// This is totally up to interface implementors.
        /// </remarks>
        IEnumerable<Action<T>> GetApplicableToType<T>();
    }
}
