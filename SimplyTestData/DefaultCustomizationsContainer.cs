using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyTestData
{
    public class DefaultCustomizationsContainer : ICustomizationsContainer
    {
        private readonly Dictionary<Type, Delegate> _customizations = new Dictionary<Type, Delegate>();

        public  void ClearAll()
        {
            _customizations.Clear();
        }

        public void ClearCustomizationsForType<T>()
        {
            _customizations.Remove(typeof(T));
        }

        public void AddCustomizationsForType<T>(params Action<T>[] customizations)
        {
            foreach (var customization in customizations)
            {
                StoreCustomizationAsPermanent(customization);
            }
        }

        public IEnumerable<Action<T>> GetCustomizationsApplicableForType<T>()
        {
            var type = typeof(T);

            var applicableCustomizations =
                from customizationMapping in _customizations
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

        private void StoreCustomizationAsPermanent<T>(Action<T> customization)
        {
            Type actualUnderlyingType = customization.GetType().GetGenericArguments()[0];

            Delegate storedPermanentCustomizations;
            if (_customizations.TryGetValue(actualUnderlyingType, out storedPermanentCustomizations))
            {
                var unique = storedPermanentCustomizations.GetInvocationList()
                    .Union(customization.GetInvocationList());
                var combined = Delegate.Combine(unique.ToArray());

                _customizations[actualUnderlyingType] = combined;
            }
            else
            {
                _customizations.Add(actualUnderlyingType, customization);
            }
        }
    }
}
