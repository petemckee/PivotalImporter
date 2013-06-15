using System;
using System.Runtime.Caching;

namespace PivotalImporter2.Domain.Cacheing
{
	public class DefaultCacheProvider : ICacheProvider
	{
		private ObjectCache Cache { get { return MemoryCache.Default; } }

		public object Get(string key)
		{
			return Cache[key];
		}

		public void Set(string key, object data, int cacheTime)
		{
			var policy = new CacheItemPolicy();
			policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

			Cache.Add(new CacheItem(key, data), policy);
		}

		public bool IsSet(string key)
		{
			return (Cache[key] != null);
		}

		public void Invalidate(string key)
		{
			Cache.Remove(key);
		}

		public int CacheTime()
		{
			// minutes
			return 30;
		}
	}

}
