using System.Reflection;

namespace Attributes
{
    internal enum SourceTag
    {
        Gold, 
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    // AllowMultiple = true 돼있으면 구독 대상을 중복으로 계속 찾을 수 있음 
    internal class BindAttribute : Attribute
    {
        public BindAttribute(string propertyName, SourceTag tag) 
        { 
            this.PropertyName = propertyName;
            this.Tag = tag;
        }

        public string PropertyName
        {
            get;
            private set;
        }
        
        public SourceTag Tag
        {
            get;
            private set;
        }
    }
}
