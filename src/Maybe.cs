using System;

namespace Prophet
{
    public interface Maybe<A>
    {
        Maybe<B> Select<B>(Func<A, Maybe<B>> map);
        Maybe<B> Select<B>(Func<A, B> map);
        Maybe<B> SelectMany<B>(Func<A, Maybe<B>> map);
        Maybe<C> SelectMany<B, C>(Func<A, Maybe<B>> map, Func<A, B, C> proj);
    }

    public readonly struct Just<A> : Maybe<A>
    {
        public A Value { get; }

        public static implicit operator A(Just<A> just) => just.Value;

        public Just(A value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        public Maybe<B> Select<B>(Func<A, Maybe<B>> map)
        {
            return map(Value);
        }

        public Maybe<B> Select<B>(Func<A, B> map)
        {
            return new Just<B>(map(Value));
        }

        public Maybe<B> SelectMany<B>(Func<A, Maybe<B>> map)
        {
            return map(Value);
        }

        public Maybe<C> SelectMany<B, C>(Func<A, Maybe<B>> map, Func<A, B, C> proj)
        {
            return SelectMany(value => map(value).SelectMany(y => new Just<C>(proj(value, y))));
        }
    }

    public readonly struct Nothing<A> : Maybe<A>
    {
        public Maybe<B> Select<B>(Func<A, Maybe<B>> map)
        {
            return new Nothing<B>();
        }

        public Maybe<B> Select<B>(Func<A, B> map)
        {
            return new Nothing<B>();
        }

        public Maybe<B> SelectMany<B>(Func<A, Maybe<B>> map)
        {
            return new Nothing<B>();
        }

        public Maybe<C> SelectMany<B, C>(Func<A, Maybe<B>> map, Func<A, B, C> proj)
        {
            return new Nothing<C>();
        }
    }
}
