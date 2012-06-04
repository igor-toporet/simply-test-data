using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyTestData
{
    public static class TestData
    {
        private static readonly Dictionary<Type, Delegate> PermanentCustomizations = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Clears all previously stored permanent customizations for objects of all types.
        /// </summary>
        public static void ClearAllPermanentCustomizations()
        {
            PermanentCustomizations.Clear();
        }

        /// <summary>
        /// Clears all permanent customizations for objects of type <typeparam name="T">T</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <remarks>Permament customizations for derived types remain intact.</remarks>
        public static void ClearPermanentCustomizations<T>()
        {
            PermanentCustomizations.Remove(typeof(T));
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
            var obj = new T();

            obj.Customize(GetApplicablePermanentCustomizations<T>());
            obj.Customize(customizations);

            return obj;
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
            var result = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                result.Add(Create<T>());
            }
            return result;
        }

        /// <summary>
        /// Customizes object by applying all the type-applicable permanent customizations
        /// as well as passed ad-hoc ones.
        /// </summary>
        /// <typeparam name="T">Type of object beig customized.</typeparam>
        /// <param name="obj">Object to be customized.</param>
        /// <param name="customizations">Ad-hoc customizations to be applied to passed object</param>
        /// <returns>
        /// Object with all the type-applicable permanent customizations
        /// applied as well as passed ad-hoc ones.
        /// </returns>
        public static T Customize<T>(this T obj, params Action<T>[] customizations)
        {
            var uniqueInvocations = customizations.SelectMany(c => c.GetInvocationList()).Distinct();

            foreach (Action<T> action in uniqueInvocations)
            {
                action(obj);
            }

            return obj;
        }

        /// <summary>
        /// Stores customizations permanently and then applies them
        /// to every created object of type <typeparam name="T">T</typeparam> or of derived type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="customizations"></param>
        public static void SetPermanentCustomizations<T>(params Action<T>[] customizations) where T : class
        {
            foreach (var customization in customizations)
            {
                StoreCustomizationAsPermanent(customization);
            }
        }

        private static bool CanInterpretTypeAsCustomized(Type type, Type customizedType)
        {
            return customizedType.IsAssignableFrom(type) || type.GetInterfaces().Contains(customizedType);
        }

        private static Action<T>[] GetApplicablePermanentCustomizations<T>()
        {
            var type = typeof(T);

            var applicableCustomizations =
                from customizationMapping in PermanentCustomizations
                let customizedType = customizationMapping.Key
                let customization = customizationMapping.Value
                where CanInterpretTypeAsCustomized(type, customizedType)
                select customization;

            return applicableCustomizations.Cast<Action<T>>().ToArray();
        }

        private static void StoreCustomizationAsPermanent<T>(Action<T> customization) where T : class
        {
            Type actualUnderlyingType = customization.GetType().GetGenericArguments()[0];

            Delegate storedPermanentCustomizations;
            if (PermanentCustomizations.TryGetValue(actualUnderlyingType, out storedPermanentCustomizations))
            {
                var unique = storedPermanentCustomizations.GetInvocationList()
                    .Union(customization.GetInvocationList());
                var combined = Delegate.Combine(unique.ToArray());

                PermanentCustomizations[actualUnderlyingType] = combined;
            }
            else
            {
                PermanentCustomizations.Add(actualUnderlyingType, customization);
            }
        }
    }
}
