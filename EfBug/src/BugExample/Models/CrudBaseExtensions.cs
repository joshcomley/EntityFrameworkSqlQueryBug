using System;

namespace BugExample.Models
{
	public static class CrudBaseExtensions
	{
		public static void EnsureEntity<T, TKey>(this CrudBase<T, TKey> crud, TKey id, Action<T> update)
			where T : class
		{
			var entity = crud.Find(id);
			if (entity == null)
			{
				entity = Activator.CreateInstance<T>();
				crud.SetEntityId(entity, id);
				crud.Add(entity);
			}
			update(entity);
		}
	}
}