using System.Collections.Generic;

namespace KendoUIApp2.Common
{
    public interface CommonService<T1, T2>
    {
        bool create(T1 data);
        bool update(T2 data);
        bool delete(string id);
        T1 findById(string id);
        List<T1> getAll();
    }
}
