using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyTestData
{
    public class CustomizationsContainer
    {
        private readonly Dictionary<Type, Delegate> _customizations = new Dictionary<Type, Delegate>();


        /// <summary>
        /// Clears all previously stored permanent customizations for objects of all types.
        /// </summary>
        public  void ClearAllPermanentCustomizations()
        {
            _customizations.Clear();
        }

        /// <summary>
        /// Clears all permanent customizations for objects of type <typeparam name="T">T</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <remarks>Permament customizations for derived types remain intact.</remarks>
        public  void ClearPermanentCustomizations<T>()
        {
            _customizations.Remove(typeof(T));
        }

        /// <summary>
        /// Stores customizations permanently and then applies them
        /// to every created object of type <typeparam name="T">T</typeparam> or of derived type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="customizations"></param>
        public  void SetPermanentCustomizations<T>(params Action<T>[] customizations) where T : class
        {
            foreach (var customization in customizations)
            {
                StoreCustomizationAsPermanent(customization);
            }
        }

        private  void StoreCustomizationAsPermanent<T>(Action<T> customization) where T : class
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
