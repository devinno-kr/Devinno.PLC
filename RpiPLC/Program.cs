using Devinno.Measure;
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
            #region I/O - Ideantree
            /*
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
            engine.DeviceLoad += (o, s) => {

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
            */
            #endregion
            #region I/O - Uno
            /*  
              Pi.Init<BootstrapWiringPi>();
              var pin = new BcmPin[] 
              {
                  BcmPin.Gpio16, BcmPin.Gpio20, BcmPin.Gpio21, BcmPin.Gpio26,
                  BcmPin.Gpio18, BcmPin.Gpio23, BcmPin.Gpio24, BcmPin.Gpio25,
                  BcmPin.Gpio08, BcmPin.Gpio07, BcmPin.Gpio01, BcmPin.Gpio12
              };

              var pout = new BcmPin[]
              {
                  BcmPin.Gpio27, BcmPin.Gpio17, BcmPin.Gpio04, BcmPin.Gpio03,
                  BcmPin.Gpio11, BcmPin.Gpio09, BcmPin.Gpio10, BcmPin.Gpio22,
                  BcmPin.Gpio13, BcmPin.Gpio06, BcmPin.Gpio05, BcmPin.Gpio00
              };

              var Outs = new IGpioPin[12];
              var Ins = new IGpioPin[12];
              var chr = new Chattering[12];

              for (int i = 0; i < 12; i++)
              {
                  Outs[i] = Pi.Gpio[pout[i]]; Outs[i].PinMode = GpioPinDriveMode.Output;
                  Ins[i] = Pi.Gpio[pin[i]]; Ins[i].PinMode = GpioPinDriveMode.Input;
                  chr[i] = new Chattering() { ChatteringTime = 50 };
              }

              var engine = new LadderEngine();
              engine.DeviceLoad += (o, s) =>
              {
                  try
                  {
                      for (int i = 0; i < 12; i++)
                      {
                          chr[i].Set(!Ins[i].Read());
                          s.Base.P[1 + i] = chr[i].State;
                      }
                  }
                  catch (Exception ex) { }
              };
              engine.DeviceOuput += (o, s) =>
              {
                  for (int i = 0; i < 12; i++) Outs[i].Write(s.Base.P[13 + i]);
              };

              engine.Connected += (o, s) => Console.WriteLine("Connected");
              engine.Disconnected += (o, s) => Console.WriteLine("Disconnected");

              engine.Start();
              Console.WriteLine("Ladder Engine Start");
          */
            #endregion
            var engine = new LadderEngine();
            engine.Start();
            while (true)
            {
                System.Threading.Thread.Sleep(100);
            }

        }
    }
}
