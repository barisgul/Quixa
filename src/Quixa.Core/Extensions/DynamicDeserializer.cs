using System.Dynamic;

namespace Quixa.Core.Extensions
{
    public static class DynamicDeserializer
    {
        public static dynamic Deserialize(object jsonObject)
        {
            dynamic deserializedObject = new ExpandoObject();

            return deserializedObject;
        }
    }
}
