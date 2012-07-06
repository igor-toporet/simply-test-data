using System;
using System.Collections.Generic;

namespace SimplyTestData
{
    public interface ICustomizationsContainer
    {
        void AddCustomizationsForType<T>(params Action<T>[] customizations);

        void ClearAll();

        void ClearCustomizationsForType<T>();

        IEnumerable<Action<T>> GetCustomizationsApplicableForType<T>();
    }
}
