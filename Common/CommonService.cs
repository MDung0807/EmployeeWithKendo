using System.Collections.Generic;

namespace KendoUIApp2.Common
{
    public interface CommonService<T>
    {
        bool create(T data);
        bool update(T data);
        bool delete(string id);
        T findById(string id);
        List<T> getAll();
    }
}
