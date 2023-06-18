using System.Collections.Generic;

namespace BluToolbox
{
    public class HardReloadManager : IHardReloadManager
    {
        private HashSet<IHardReload> _objs = new();

        public void HardReload()
        {
            foreach (IHardReload obj in _objs)
            {
                obj.OnHardReload();                
            }
            _objs.Clear();
        }

        public void AddOnHardReload(IHardReload obj)
        {
            _objs.Add(obj);
        }

        public void RemoveOnHardReload(IHardReload obj)
        {
            _objs.Remove(obj);
        }
    }
}