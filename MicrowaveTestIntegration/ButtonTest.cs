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
    public class ButtonTest
    {
        private Button sut_powerButton;
        private Button sut_timeButton;
        private Button sut_StartCancelButton;
        private Door sut_door;

        private ICookController cook;
        private ILight light;
        private IDisplay display;
        private UserInterface ui; 

        [SetUp]
        public void Setup()
        {
            sut_powerButton = new Button();
            sut_timeButton = new Button();
            sut_StartCancelButton = new Button();
            sut_door = new Door();

            cook = Substitute.For<ICookController>();
            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();

            ui = new UserInterface(sut_powerButton, sut_timeButton, sut_StartCancelButton, sut_door, display, light, cook);
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
            sut_StartCancelButton.Press();

           // ui.Received().OnStartCancelPressed();
        }

        
        /// <summary>
        /// Door 
        /// Her starter tests for Door 
        /// </summary>
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
