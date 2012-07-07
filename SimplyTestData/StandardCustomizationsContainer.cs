using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyTestData
{
    public class StandardCustomizationsContainer : ICustomizationsContainer
    {
        protected readonly Dictionary<Type, Delegate> Customizations = new Dictionary<Type, Delegate>();

        public void AddForType<T>(params Action<T>[] customizations)
        {
            foreach (var customization in customizations)
            {
                StoreCustomization(customization);
            }
        }

        public void ClearAll()
        {
            Customizations.Clear();
        }

        public void ClearForType<T>()
        {
            Customizations.Remove(typeof(T));
        }

        public IEnumerable<Action<T>> GetApplicableToType<T>()
        {
            var applicableCustomizations =
                from customizationMapping in Customizations
                let customizedType = customizationMapping.Key
                let customization = customizationMapping.Value
                where CanInterpretTypeAsCustomized(typeof(T), customizedType)
                select customization;

            return applicableCustomizations.Cast<Action<T>>().ToArray();
        }

        /// <summary>
        /// Limits customizations to be considered as applicable to the specified type itself.
        /// </summary>
        /// <remarks>
        /// In other words, customizations defined for base/inherited types
        /// as well as for implemented interfaces will be not included
        /// into results of GetApplicableToType&lt;T&gt;() call.
        /// </remarks>
        public bool ScopeApplicableCustomizationsToRequestedTypeOnly { get; set; }

        private bool CanInterpretTypeAsCustomized(Type type, Type customizedType)
        {
            if (ScopeApplicableCustomizationsToRequestedTypeOnly)
            {
                return type == customizedType;
            }

            return customizedType.IsAssignableFrom(type);
        }

        protected void StoreCustomization<T>(Action<T> customization)
        {
            Type customizedType = customization.GetType().GetGenericArguments()[0];

            Delegate storedCustomizations;
            if (Customizations.TryGetValue(customizedType, out storedCustomizations))
            {
                var uniqueInvocations =
                    storedCustomizations.GetInvocationList()
                    .Union(customization.GetInvocationList());

                var combinedIntoDelegate = Delegate.Combine(uniqueInvocations.ToArray());

                Customizations[customizedType] = combinedIntoDelegate;
            }
            else
            {
                Customizations.Add(customizedType, customization);
            }
        }
    }
}
