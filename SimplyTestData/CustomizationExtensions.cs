using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyTestData
{
    public static class CustomizationExtensions
    {
        public static T Customize<T>(this T obj, params Action<T>[] customizations)
        {
            return DoCustomize(obj, customizations);
        }

        public static T Customize<T>(this T obj, IEnumerable<Action<T>> customizations)
        {
            return DoCustomize(obj, customizations);
        }

        private static T DoCustomize<T>(T obj, IEnumerable<Action<T>> customizations)
        {
            var uniqueInvocations = customizations.SelectMany(c => c.GetInvocationList()).Distinct();

            foreach (Action<T> action in uniqueInvocations)
            {
                action(obj);
            }

            return obj;
        }
    }
}
