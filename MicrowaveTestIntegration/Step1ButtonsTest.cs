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

namespace MicrowaveTestIntegration
{
    [TestFixture]
    public class Step1ButtonsTest
    {
        private Button sut_powerButton;
        private Button sut_timeButton;
        private Button sut_startCancelButton;
        private Door door;

        private ICookController cook;
        private ILight light;
        private IDisplay display;
        private UserInterface ui; 

        [SetUp]
        public void Setup()
        {
            sut_powerButton = new Button();
            sut_timeButton = new Button();
            sut_startCancelButton = new Button();
            door = new Door();

            cook = Substitute.For<ICookController>();
            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();

            ui = new UserInterface(sut_powerButton, sut_timeButton, sut_startCancelButton, door, display, light, cook);
        }

        [Test]
        public void PowerButton_Pressed_UI_Received_OnPowerPressed_StateREADY()
        {
            sut_powerButton.Press();

            display.Received().ShowPower(50);
        }

        [Test]
        public void PowerButton_Pressed_UI_Received_OnPowerPressed_StateSETPOWER()
        {
            sut_powerButton.Press();

            sut_powerButton.Press();

            display.Received().ShowPower(100);
        }

        [Test]
        public void TimeButton_Pressed_UI_Received_OnTimePressed()
        {
            sut_timeButton.Press();

           // ui.Received().OnTimePressed();
        }

        [Test]
        public void StartCancelButton_Pressed_UI_Received_OnStartCancelPressed()
        {
            sut_startCancelButton.Press();

           // ui.Received().OnStartCancelPressed();
        }
        
    }
}
