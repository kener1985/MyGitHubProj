using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    sealed public class CacheManager
    {
        static private Dictionary<string,object> _cache;
        static CacheManager()
        {
            _cache = new Dictionary<string, object>();
        }
        static public object GetCache(string pageid, string cacheid)
        {
            string id = pageid + cacheid;
            object cache = null;
            if (_cache.ContainsKey(id))
            {
                cache = _cache[id];
                
            }

            return cache;
        }

        static public void AddCache(string pageid, string cacheid, object ini)
        {
            //if (_cache.ContainsKey(cacheid))
            //    throw new Exception("cache exist!");
            string id = pageid + cacheid;
            GlobalVar.Log.LogDebug("增加缓存！", id);
            _cache[id] = ini;
        }
        static public bool HasCache(string pageid,string cacheid)
        {
            string id = pageid + cacheid;
            bool bHint = _cache.ContainsKey(id);
            if(bHint)
                GlobalVar.Log.LogDebug("命中缓存!", id);
            else
                GlobalVar.Log.LogDebug("没有命中缓存!", id);
            return bHint;
        }
        static public void ClearPageCache(string pageid)
        {
            if (Size() == 0)
                return;
            string[] keys = _cache.Keys.ToArray();
            for (int i = 0; i < keys.Length;i++ )
            {
                string key = keys[i];
                if (key.StartsWith(pageid))
                    _cache.Remove(key);
            }
            GlobalVar.Log.LogDebug("删除缓存!", pageid);
        }
        static public int Size()
        {
            return _cache.Count;
        }
    }
}
