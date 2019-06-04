namespace Test.Helpers
{
    public static class CallbackOutErrorHelper
    {
        public static string ErrorMessage => "Error From Helper";

        public delegate void Object1(object object1, out string error);

        public delegate void Object2(object object1, object object2, out string error);

        public static Object1 DelegateForObject1 => (object object1, out string error) => error = ErrorMessage;

        public static Object2 DelegateForObject2 =>
            (object object1, object object2, out string error) => error = ErrorMessage;
    }
}
