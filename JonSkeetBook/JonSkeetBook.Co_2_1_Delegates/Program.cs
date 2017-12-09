using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Threading;

namespace JonSkeetBook.Co_2_1_Delegates
{
    /*
    Делегаты обеспечивают уровень косвенного обращения: вместо того чтобы определить действие,
    которое будет выполнено непосредственно, его можно упаковать в объект. Затем этот объект
    может быть использован как и любой другой, а одной из операций, которую вы можете осуществить
    c ним - это выполнение инкапсулируемого действия.
    * 
    Можно считать тип делегата - интерфейсом одного метода, а экземпляр делегата - объектом,
    реализующим этот интерфейс.
    * 
    Следует знать, что экземпляр делегата будет препятствовать удалению своей цели 
    при сборе "мусора", если сам экземпляр делегата не может быть собран. Это может  
    привести к утечке памяти, особенно когда "недолговечный" объект подписался на событие 
    "долговечного" объекта, используя себя как цель. Долговечный объект косвенно  
    удерживает ссылку на недолговечный объект, продлевая его существование. 
     
    Зачем нужны делегаты?
    Для того чтобы у нас была возможность разрешить ситуацию, когда код должен выполнить действия, не зная
    точно, каковы эти действия должны быть. Например, единчтвенная причина, по которой класс Thread значет,
    что при запуске нужно работать в новом потоке, - это прелоставленный вами конструктор с экземпляром
    делегата ThreadStart или ParametrizedThreadStart.
     
    Рецепт простых делегатов:
    1 Тип делегата должен быть объявлен.
    2 Должен существовать метод, содержащий код для выполнения.
    3 Экземпляр делегата должен быть создан.
    4 Экземпляр делегата должен быть вызван.

    Любой создаваемый тип делегата неявно наследуется от System.MulticastDelegate, который, в свою очередь, наследуется
    от System.Delegate.
                            [делегат] -> System.MulticastDelegate -> System.Delegate
    
    Понятия:
        - Тип делегата
        - Экземпляр типа делегата
    
    Следует знать, что экземпляр делегата будет препятствовать удалению своей цели 
    при сборе "мусора", если сам экземпляр делегата не может быть собран. Это может  
    привести к утечке памяти, особенно когда "недолговечный" объект подписался на событие 
    "долговечного" объекта, используя себя как цель. Долговечный объект косвенно  
    удерживает ссылку на недолговечный объект, продлевая его существование.

    */

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                MethodGroups();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadLine();
        }

        #region First example
        private delegate void StringProcessor(string s);

        private static void FirstExample()
        {
            StringProcessor sp1 = new StringProcessor(PrintString1);
            StringProcessor sp2 = new StringProcessor(PrintString2);

            var sp = (StringProcessor)Delegate.Combine(sp1, sp2);
            sp.Invoke("s");
        }

        private static void PrintString1(string s)
        {
            Console.WriteLine($"string: {s}");
        }

        private static void PrintString2(object s)
        {
            Console.WriteLine($"object: {s}");
        }
        #endregion

        #region Second region
        public delegate string DoCalculation(EngCalc clac);

        private static void SecondExample()
        {
            var calc1 = new DoCalculation(Show1);
            var calc2 = new DoCalculation(Show2);

            // Как очеред, первым скомбинирован  - первым выполнится
            // Если возвращается значение, это значение будет того, кто выполнился последним
            var combine = (DoCalculation)Delegate.Combine(calc1, calc2);
            
            //DoCalculation combine = null;
            //combine += calc1; //(DoCalculation)Delegate.Combine(combine, calc1);
            //combine += calc2; //(DoCalculation)Delegate.Combine(combine, calc2);
            
            //combine -= calc2; //(DoCalculation)Delegate.Remove(combine, calc1);

            //// Если результатом оказывается пустой список вызова, возвращается null.
            //// Тут как раз это и происходит => combine is null !!!!!!
            //combine -= calc1; //(DoCalculation)Delegate.Remove(combine, calc1);
            
            Calc instance = new EngCalc(10);

            string s = combine((EngCalc)instance);
            Console.WriteLine(s);
        }

        private static string Show1(Calc c)
        {
            return c.SumInt(1, 2).ToString(CultureInfo.InvariantCulture);
        }

        private static string Show2(EngCalc c)
        {
            return c.SumDouble(1, 2).ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        #region Third
        private static void Event()
        {
            var eventContainer = new EventContainer();

            //eventContainer.Event = (s, e) => { }; // Ощибка при компиляции, нельзя напрямую изменять, необходимо пользоваться акцессорами события
            eventContainer.Event += (s, e) => { Console.WriteLine(e.Message); };

            eventContainer.Go();
        } 
        #endregion

        private static void MethodGroups()
        {
            // Преобразование групп методов
            // Компилятор следит за тем, можем ли мы преобразовать любую из
            // перегрузок данного метода (из-за этого такое название) в ожидаемый
            // делегат.
            ThreadStart ts = Start;

            Action a = () => { };
            Action<int> b = i => { };
            Type t = a.Target.GetType();

            //IEnumerable ie = null;
            //ICollection ic = null;
            //IList il = null;
            //Array ar = null;
        }

        private static void Start()
        {
            
        }

        private static void Start(object sender, EventArgs e)
        {
            
        }
    }

    class Calc
    {
        public int X { get; }

        public Calc(int x)
        {
            X = x;
        }

        public int SumInt(int x, int y)
        {
            return x + y;
        }
    }

    class EngCalc : Calc
    {
        public EngCalc(int x)
            : base(x)
        {
            
        }

        public double SumDouble(double x, double y)
        {
            return x + y + X;
        }
    }

    class EventContainer
    {
        public event EventHandler<MyEventArgs> Event;

        protected virtual void OnEvent()
        {
            Event?.Invoke(this, new MyEventArgs($"Hello from {GetType().Name}!"));
        }

        public void Go()
        {
            OnEvent();
        }
    }

    class MyEventArgs : EventArgs
    {
        public string Message { get; }

        public MyEventArgs(string message)
        {
            Message = message;
        }
    }
}
