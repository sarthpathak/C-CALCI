using System;

namespace EVENTING
{
    
   

    //Generics with interface 
    public interface ICalculator<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
    }

    //Generic class of type T
    public class Calculator<T> : ICalculator<T> where T : IConvertible
    {
        public event EventHandler<CalculationEventArgs<T>> CalculationPerformed;

        protected  void OnCalculationPerformed(T result)
        {
            CalculationPerformed?.Invoke(this, new CalculationEventArgs<T> { Result = result });
        }

        //Add function
        public T Add(T a, T b)
        {
            dynamic result = PerformOperation(a, b, (x, y) => x + y);
            return result;
        }

        //Substract function
        public T Subtract(T a, T b)
        {
            dynamic result = PerformOperation(a, b, (x, y) => x - y);
            return result;
        }
       
        //
        public T Multiply(T a, T b)
        {
            dynamic result = PerformOperation(a, b, (x, y) => x * y);
            return result;
        }

        public T Divide(T a, T b)
        {
            if (b.ToDouble(null) == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            dynamic result = PerformOperation(a, b, (x, y) => x / y);
            return result;
        }

        //passing  functions in delegates
        private T PerformOperation(T a, T b, Func<double, double, double> operation)
        {
            double result = operation(a.ToDouble(null), b.ToDouble(null));
            OnCalculationPerformed((T)Convert.ChangeType(result, typeof(T)));
            return (T)Convert.ChangeType(result, typeof(T));
        }
    
}


    //  Event getting and setting
    public class CalculationEventArgs<T> : EventArgs
    {
        public T Result { get; set; }
    }
}