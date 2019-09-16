namespace Prophet
{
    public static class Prelude
    {
        public static Maybe<A> Maybe<A>(A value)
        {
            if (value != null)
            {
                return Just(value);
            }

            return Nothing<A>();
        }

        public static Just<A> Just<A>(A value)
        {
            return new Just<A>(value);
        }

        public static Nothing<A> Nothing<A>()
        {
            return new Nothing<A>();
        }
    }
}
