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

        [Test]
        public void Door()
        {
            sut_door.Open();

            //  ui.Received().OnDoorOpened();
        }

        [Test]
        public void Door_Close()
        {
            sut_door.Close();

            //  ui.Received().OnDoorClosed();
        }
    }
}
