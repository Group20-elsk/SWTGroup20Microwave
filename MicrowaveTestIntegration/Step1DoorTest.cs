using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace MicrowaveTestIntegration //Der mangler en .dll fil til Jenkins serveren?? 
{
    [TestFixture]
    public class Step1DoorTest
    {
        private Button powerButton;
        private Button timeButton;
        private Button startCancelButton;
        private Door sut_door;

        private ICookController cook;
        private ILight light;
        private IDisplay display;
        private UserInterface ui;

        [SetUp]
        public void Setup()
        {
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();
            sut_door = new Door();

            cook = Substitute.For<ICookController>();
            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();

            ui = new UserInterface(powerButton, timeButton, startCancelButton, sut_door, display, light, cook);
        }

        //Test at Door open
        //READY
        //SETPOWER
        //SETTIME
        //COOKING
        [Test]
        public void Door_OnDoorOpened_Display_Received_Clear_StateReady()
        {
            sut_door.Open();

            light.Received().TurnOn();
        }

        [Test]
        public void Door_OnDoorOpened_Light_Received_TurnOn_StateSetPower()
        {
            powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_door.Open();

            display.Received().Clear();
        }

        [Test]
        public void Door_OnDoorOpened_Light_Received_TurnOn_State_SetTime()
        {
            powerButton.Press(); //Her bliver state sat til SETPOWER
            timeButton.Press();  //Her bliver state sat til SETTIME
            sut_door.Open();

            display.Received().Clear();
        }

        [Test]
        public void Door_OnDoorOpened_CookController_Received_Stop_StateCooking()
        {
            powerButton.Press(); //Her bliver state sat til SETPOWER
            timeButton.Press();  //Her bliver state sat til SETTIME
            startCancelButton.Press();  //Her bliver state sat til COOKING
            sut_door.Open();

            cook.Received().Stop();
        }


        //Test at Door close
        //DoorOpen
        [Test]
        public void Door_OnDoorClosed_Light_Received_TurnOff_StateDoorOpen()
        {
            sut_door.Open(); //Her bliver state sat til DoorOpen
            sut_door.Close();

            light.Received().TurnOff();
        }
    }
}
