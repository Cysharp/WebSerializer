﻿// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY `Serializers/TupleSerializers.tt`. DO NOT CHANGE IT.
// </auto-generated>
#nullable enable
namespace Cysharp.Web.Serializers
{
    public sealed class TupleWebSerializer<T1> : IWebSerializer<Tuple<T1>>
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1> : IWebSerializer<ValueTuple<T1>>
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.Exit();
        }
    }

    public sealed class TupleWebSerializer<T1, T2> : IWebSerializer<Tuple<T1, T2>>
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1, T2> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1, T2> : IWebSerializer<ValueTuple<T1, T2>>
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1, T2> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.Exit();
        }
    }

    public sealed class TupleWebSerializer<T1, T2, T3> : IWebSerializer<Tuple<T1, T2, T3>>
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1, T2, T3> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1, T2, T3> : IWebSerializer<ValueTuple<T1, T2, T3>>
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1, T2, T3> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.Exit();
        }
    }

    public sealed class TupleWebSerializer<T1, T2, T3, T4> : IWebSerializer<Tuple<T1, T2, T3, T4>>
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1, T2, T3, T4> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1, T2, T3, T4> : IWebSerializer<ValueTuple<T1, T2, T3, T4>>
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1, T2, T3, T4> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);

            writer.Exit();
        }
    }

    public sealed class TupleWebSerializer<T1, T2, T3, T4, T5> : IWebSerializer<Tuple<T1, T2, T3, T4, T5>>
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1, T2, T3, T4, T5> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1, T2, T3, T4, T5> : IWebSerializer<ValueTuple<T1, T2, T3, T4, T5>>
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);

            writer.Exit();
        }
    }

    public sealed class TupleWebSerializer<T1, T2, T3, T4, T5, T6> : IWebSerializer<Tuple<T1, T2, T3, T4, T5, T6>>
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1, T2, T3, T4, T5, T6> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1, T2, T3, T4, T5, T6> : IWebSerializer<ValueTuple<T1, T2, T3, T4, T5, T6>>
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);

            writer.Exit();
        }
    }

    public sealed class TupleWebSerializer<T1, T2, T3, T4, T5, T6, T7> : IWebSerializer<Tuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1, T2, T3, T4, T5, T6, T7> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1, T2, T3, T4, T5, T6, T7> : IWebSerializer<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);

            writer.Exit();
        }
    }

    public sealed class TupleWebSerializer<T1, T2, T3, T4, T5, T6, T7, TRest> : IWebSerializer<Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>>
        where TRest : notnull
    {
        public void Serialize(ref WebSerializerWriter writer, Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> value, WebSerializerOptions options)
        {
            if (value == null) return;

            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<TRest>().Serialize(ref writer, value.Rest, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleWebSerializer<T1, T2, T3, T4, T5, T6, T7, TRest> : IWebSerializer<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>
        where TRest : struct
    {
        public void Serialize(ref WebSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);
            var latestName = (options.CollectionSeparator == null) ? CollectionHelper.GetLatestName(writer.GetStringBuilder()) : null;

            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);
            if (options.CollectionSeparator != null)
            {
                writer.AppendRaw(options.CollectionSeparator);
            }
            else if (latestName != null)
            {
                writer.AppendConcatenate();
                writer.AppendRaw(latestName);
            }
            else
            {
                writer.AppendConcatenate();
            }
            options.GetRequiredSerializer<TRest>().Serialize(ref writer, value.Rest, options);

            writer.Exit();
        }
    }


    internal static class TupleWebSerializer
    {
        internal static Type GetTupleWebSerializerType(int i)
        {
            switch (i)
            {
                case 1:
                    return typeof(TupleWebSerializer<>);
                case 2:
                    return typeof(TupleWebSerializer<,>);
                case 3:
                    return typeof(TupleWebSerializer<,,>);
                case 4:
                    return typeof(TupleWebSerializer<,,,>);
                case 5:
                    return typeof(TupleWebSerializer<,,,,>);
                case 6:
                    return typeof(TupleWebSerializer<,,,,,>);
                case 7:
                    return typeof(TupleWebSerializer<,,,,,,>);
                case 8:
                    return typeof(TupleWebSerializer<,,,,,,,>);
                default:
                    break;
            }

            throw new InvalidOperationException($"TupleWebSerializer<T1...T{i}> is not found.");
        }

        internal static Type GetValueTupleWebSerializerType(int i)
        {
            switch (i)
            {
                case 1:
                    return typeof(ValueTupleWebSerializer<>);
                case 2:
                    return typeof(ValueTupleWebSerializer<,>);
                case 3:
                    return typeof(ValueTupleWebSerializer<,,>);
                case 4:
                    return typeof(ValueTupleWebSerializer<,,,>);
                case 5:
                    return typeof(ValueTupleWebSerializer<,,,,>);
                case 6:
                    return typeof(ValueTupleWebSerializer<,,,,,>);
                case 7:
                    return typeof(ValueTupleWebSerializer<,,,,,,>);
                case 8:
                    return typeof(ValueTupleWebSerializer<,,,,,,,>);
                default:
                    break;
            }

            throw new InvalidOperationException($"ValueTupleWebSerializer<T1...T{i}> is not found.");
        }
    }
}