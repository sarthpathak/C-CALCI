using System;

namespace EVENTING
{
    
    using System.Security.Policy;

    public interface ICalculator<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
    }

    public class Calculator<T> : ICalculator<T> where T : IConvertible
    {
        public event EventHandler<CalculationEventArgs<T>> CalculationPerformed;

        protected virtual void OnCalculationPerformed(T result)
        {
            CalculationPerformed?.Invoke(this, new CalculationEventArgs<T> { Result = result });
        }

        public T Add(T a, T b)
        {
            dynamic result = a.ToDouble(null) + b.ToDouble(null);
            OnCalculationPerformed((T)Convert.ChangeType(result, typeof(T)));
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public T Subtract(T a, T b)
        {
            dynamic result = a.ToDouble(null) - b.ToDouble(null);
            OnCalculationPerformed((T)Convert.ChangeType(result, typeof(T)));
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public T Multiply(T a, T b)
        {
            dynamic result = a.ToDouble(null) * b.ToDouble(null);
            OnCalculationPerformed((T)Convert.ChangeType(result, typeof(T)));
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public T Divide(T a, T b)
        {
            if (b.ToDouble(null) == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            dynamic result = a.ToDouble(null) / b.ToDouble(null);
            OnCalculationPerformed((T)Convert.ChangeType(result, typeof(T)));
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }

    public class CalculationEventArgs<T> : EventArgs
    {
        public T Result { get; set; }
    }
}