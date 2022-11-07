# Devinno.PLC [![NuGet stable version](https://badgen.net/nuget/v/devinno.plc)](https://nuget.org/packages/devinno.plc)


## 개요
  * PLC 기능을 수행할 수 있는 라이브러리
    <br />
    <br />  

## 참조
  * [Devinno](https://github.com/devinno-kr/Devinno) [1.1.0.3](https://www.nuget.org/packages/Devinno/1.1.0.3)
  * [Devinno.PLC.Interface](https://github.com/devinno-kr/Devinno.PLC) [1.0.0](https://www.nuget.org/packages/Devinno.PLC.Interface/1.0.0)
  * [Microsoft.CodeAnalysis.Common](https://github.com/dotnet/roslyn) [4.3.1](https://www.nuget.org/packages/Microsoft.CodeAnalysis.Common/4.3.1)
  * [Microsoft.CodeAnalysis.CSharp](https://github.com/dotnet/roslyn) [4.3.1](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp/4.3.1)
  * [Newtonsoft.Json](https://www.newtonsoft.com/json) [13.0.1](https://www.nuget.org/packages/Newtonsoft.Json/13.0.1)
    <br />
    <br />  

## 목차
  * [RpiPLC](#RpiPLC)
    <br />
    <br />  

## 예시
#### 1. RpiPLC
* **코드** 
```csharp
using Devinno.PLC.Ladder;
using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace RpiPLC
{
    class Program
    {
        static void Main(string[] args)
        {
            #region I/O
            Pi.Init<BootstrapWiringPi>();

            var Out1 = Pi.Gpio[P1.Pin12]; Out1.PinMode = GpioPinDriveMode.Output;
            var Out2 = Pi.Gpio[P1.Pin16]; Out2.PinMode = GpioPinDriveMode.Output;
            var Out3 = Pi.Gpio[P1.Pin18]; Out3.PinMode = GpioPinDriveMode.Output;
            var Out4 = Pi.Gpio[P1.Pin22]; Out4.PinMode = GpioPinDriveMode.Output;
            var Out5 = Pi.Gpio[P1.Pin32]; Out5.PinMode = GpioPinDriveMode.Output;
            var Out6 = Pi.Gpio[P1.Pin36]; Out6.PinMode = GpioPinDriveMode.Output;
            var Out7 = Pi.Gpio[P1.Pin38]; Out7.PinMode = GpioPinDriveMode.Output;
            var Out8 = Pi.Gpio[P1.Pin40]; Out8.PinMode = GpioPinDriveMode.Output;

            var In1 = Pi.Gpio[P1.Pin07]; In1.PinMode = GpioPinDriveMode.Input;
            var In2 = Pi.Gpio[P1.Pin11]; In2.PinMode = GpioPinDriveMode.Input;
            var In3 = Pi.Gpio[P1.Pin13]; In3.PinMode = GpioPinDriveMode.Input;
            var In4 = Pi.Gpio[P1.Pin15]; In4.PinMode = GpioPinDriveMode.Input;
            var In5 = Pi.Gpio[P1.Pin29]; In5.PinMode = GpioPinDriveMode.Input;
            var In6 = Pi.Gpio[P1.Pin31]; In6.PinMode = GpioPinDriveMode.Input;
            var In7 = Pi.Gpio[P1.Pin33]; In7.PinMode = GpioPinDriveMode.Input;
            var In8 = Pi.Gpio[P1.Pin35]; In8.PinMode = GpioPinDriveMode.Input;
            #endregion

            #region Engine
            var engine = new LadderEngine();
            engine.DeviceLoad += (o, s) =>
            {

                try
                {
                    s.Base.P[1] = In1.Read();
                    s.Base.P[2] = In2.Read();
                    s.Base.P[3] = In3.Read();
                    s.Base.P[4] = In4.Read();
                    s.Base.P[5] = In5.Read();
                    s.Base.P[6] = In6.Read();
                    s.Base.P[7] = In7.Read();
                    s.Base.P[8] = In8.Read();
                }
                catch (Exception ex) { }
            };
            engine.DeviceOuput += (o, s) =>
            {
                Out1.Write(s.Base.P[11]);
                Out2.Write(s.Base.P[12]);
                Out3.Write(s.Base.P[13]);
                Out4.Write(s.Base.P[14]);
                Out5.Write(s.Base.P[15]);
                Out6.Write(s.Base.P[16]);
                Out7.Write(s.Base.P[17]);
                Out8.Write(s.Base.P[18]);
            };
            engine.Start();

            Console.WriteLine("Ladder Engine Start");
            #endregion

            while (true)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
```

* **설명** 
``` 

```
<br />
