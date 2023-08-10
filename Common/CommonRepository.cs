using System.Collections.Generic;

namespace KendoUIApp2.Common
{
    public interface CommonRepository<T>
    {
        List<T> getAll();
        T findById(string id);
        bool create(T entity);
        bool update(T entity);
        bool delete(string id);
    }
}
