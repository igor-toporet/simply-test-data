using System;
using System.Collections.Generic;
using System.Linq;

namespace TestData
{
    public static class TestData
    {
        private static readonly Dictionary<Type, Delegate> PermanentCustomizations = new Dictionary<Type, Delegate>();

        public static void ApplyPermanently<T>(params Action<T>[] customizations)
        {
            foreach (var customization in customizations)
            {
                StoreCustomizationAsPermanent(customization);
            }
        }

        public static void ClearPermanentCustomizations()
        {
            PermanentCustomizations.Clear();
        }

        public static void ClearPermanentCustomizations<T>()
        {
            PermanentCustomizations.Remove(typeof(T));
        }

        public static T Create<T>(params Action<T>[] customizations) where T : class
        {
            var obj = (T) Activator.CreateInstance(typeof(T));

            obj.Customize(GetApplicablePermanentCustomizations<T>());
            obj.Customize(customizations);

            return obj;
        }

        public static IList<T> CreateListOf<T>(ushort size) where T : class
        {
            var result = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                result.Add(Create<T>());
            }
            return result;
        }

        public static T Customize<T>(this T obj, params Action<T>[] customizations)
        {
            var uniqueInvocations = customizations.SelectMany(c => c.GetInvocationList()).Distinct();

            foreach (Action<T> action in uniqueInvocations)
            {
                action(obj);
            }

            return obj;
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

        private static bool CanInterpretTypeAsCustomized(Type type, Type customizedType)
        {
            return customizedType.IsAssignableFrom(type) || type.GetInterfaces().Contains(customizedType);
        }

        private static void StoreCustomizationAsPermanent<T>(Action<T> customization)
        {
            Type actualUnderlyingType = customization.GetType().GetGenericArguments()[0];

            Delegate storedPermanentCustomizations;
            if (PermanentCustomizations.TryGetValue(actualUnderlyingType, out storedPermanentCustomizations))
            {
                var unique = storedPermanentCustomizations.GetInvocationList().Union(customization.GetInvocationList());
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
