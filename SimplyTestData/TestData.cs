using System;
using System.Collections.Generic;

namespace SimplyTestData
{
    public static class TestData
    {
        static TestData()
        {
            DefaultSession = new TestDataSession {Customizations = new StandardCustomizationsContainer()};
        }

        public static TestDataSession DefaultSession { get; set; }

        /// <summary>
        /// Clears all previously stored permanent customizations for objects of all types.
        /// </summary>
        public static void ClearAllPermanentCustomizations()
        {
            DefaultSession.Customizations.ClearAll();
        }

        /// <summary>
        /// Clears all permanent customizations for objects of type <typeparam name="T">T</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <remarks>Permament customizations for derived types remain intact.</remarks>
        public static void ClearPermanentCustomizations<T>()
        {
            DefaultSession.Customizations.ClearForType<T>();
        }

        /// <summary>
        /// Creates object of specified type <typeparam name="T">T</typeparam>
        /// with all the type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </summary>
        /// <typeparam name="T">Type of object to be created.</typeparam>
        /// <param name="customizations">Ad-hoc customizations to be applied to resulting object.</param>
        /// <returns>
        /// Object of type <typeparam name="T">T</typeparam>
        /// with all type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </returns>
        public static T Create<T>(params Action<T>[] customizations) where T : class, new()
        {
            return DefaultSession.Create(customizations);
        }

        /// <summary>
        /// Creates list of objects of specified type <typeparam name="T">T</typeparam>
        /// with all the type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </summary>
        /// <typeparam name="T">Type of object to be created.</typeparam>
        /// <param name="size">List capacity (i.e. number of objects in list)</param>
        /// <returns>
        /// List of objects of specified type <typeparam name="T">T</typeparam>
        /// with all the type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </returns>
        public static IList<T> CreateListOf<T>(ushort size) where T : class, new()
        {
            return DefaultSession.CreateListOf<T>(size);
        }

        /// <summary>
        /// Stores customizations permanently and then applies them
        /// to every created object of type <typeparam name="T">T</typeparam> or of derived type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="customizations"></param>
        public static void SetPermanentCustomizations<T>(params Action<T>[] customizations) where T : class
        {
            DefaultSession.Customizations.AddForType(customizations);
        }
    }
}
