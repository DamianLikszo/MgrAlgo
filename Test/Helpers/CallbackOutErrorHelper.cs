namespace Test.Helpers
{
    public static class CallbackOutErrorHelper
    {
        public static string ErrorMessage => "Error From Helper";

        public delegate void Object1(object object1, out string error);
        
        public static Object1 DelegateForObject1 => (object string1, out string error) => error = ErrorMessage;
    }
}
