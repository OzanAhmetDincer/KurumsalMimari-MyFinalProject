using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }// Öncelik demek yani hangi özelliğin önce çalışmasını belirleriz. loglama mı cache mi gibi

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
