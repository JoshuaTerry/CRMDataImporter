using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CRMDataImport
{
    public class ThreadActionContext : IDisposable
    {
        protected Project _project;
         
        static object indexLock = new object();
        static Dictionary<int, ActionContext> _threadContexts = new Dictionary<int, ActionContext>();

        public ThreadActionContext(Project prj)
        {
            _project = prj; 
        }
         
        public ActionContext Current
        {
            get
            {
                //see if this thread already currently has a context open
                lock (indexLock)
                {
                    if (_threadContexts.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                    {
                        var context = _threadContexts[Thread.CurrentThread.ManagedThreadId];
                        context.UseCount++;

                        //this is to recycle the context after so many uses
                        if (context.UseCount > 1000)
                        {
                            DestroyContext();
                            return CreateNewContext();
                        }

                        return context;
                    }
                    else
                    {
                        return CreateNewContext();
                    }
                }
            }
        }

        private void DestroyContext()
        {
            if (_threadContexts.ContainsKey(Thread.CurrentThread.ManagedThreadId))
            {
                _threadContexts[Thread.CurrentThread.ManagedThreadId].Dispose();
                _threadContexts.Remove(Thread.CurrentThread.ManagedThreadId);
            }
        }

        private ActionContext CreateNewContext()
        {
            var service = _project.AuthHelper.GetOrganizationProxy();
            var context = new CrmContext(service);
            var ac = new ActionContext(service, context);
            _threadContexts[Thread.CurrentThread.ManagedThreadId] = ac;
            return ac;
        }

        public void InvalidateContext()
        {
            lock (indexLock)
            {
                DestroyContext();
            }
        }
         
        public void Dispose()
        {
            lock (indexLock)
            {
                foreach (ActionContext c in _threadContexts.Values)
                {
                    c.Dispose();
                }
            }
        }
    }
}
